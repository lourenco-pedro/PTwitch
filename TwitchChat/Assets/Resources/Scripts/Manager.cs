using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PCharacter;
using TMPro;
using System.Diagnostics;

namespace TwitchChat_frntEnd
{

    [System.Serializable]
    public class Access
    {
        public string BackEndProcessPath;
        public string JsonPath;
#if UNITY_EDITOR    
        public string DevBackEndProcessPath;
        public string DevJsonPath;
#endif
    }

    [System.Serializable]
    public class Client
    {
        [ReadOnly]
        public string UserName;
        [ReadOnly]
        public string AccessToken;
        [ReadOnly]
        public bool Connected;
        [ReadOnly]
        public bool ReadMessages;
        [ReadOnly]
        public List<Follower> Followers;
    }

    [System.Serializable]
    public class Simulation
    {
        [ReadOnly]
        public List<State> States;

        public float TickInSeconds;

        [Header("Follower Section")]
        [Space(10f)]

        [ReadOnly]
        public Follower NewFollower;
        public float TimeDisplayFollower;

        public Queue<Follower> FollowersToAdd;

        [HideInInspector]
        public float CurrentTick;
        [HideInInspector]
        public bool Tick;
        [HideInInspector]
        public State ToState;
        [HideInInspector]
        public ChangeType ChangeType;
        [HideInInspector]
        public float CurrentTimeDisplayFollower;
        [HideInInspector]
        public Process Backend;
    }

    [System.Serializable]
    public class Prefabs
    {
        [Header("MAIN")]
        public MainCameraComponent MainCameraComponent;

        [Space(10f)]
        [Header("Chat")]
        public Transform Ground;
        public CharacterBaseCollection[] CharacterCollection;

        [Space(10f)]
        [Header("UI")]
        public UIHolder UIHolder;
        public LoginPanelComponent LoginPanelComponent;
        public LoginSettingsPanelComponent LoginSettingsPanelComponent;
        public UIPanel ConnectingPanelComponent;
        public HubPanelComponent HubPanelComponent;
        public InChatPanelComponent InChatPanelComponent;

        [Space(10f)]
        public UIText UiText;
    }

    [System.Serializable]
    public class Chat
    {

        [Header("Layers")]
        public string CharacterLayer;
        public string GroundLayer;

        [Space(10f)]

        [Header("Ground")]
        [ReadOnly]
        public Transform Ground;
        public float GroundYOfset;

        [Space(10f)]
        [Header("Characters")]
        public CharacterConfiguration CharacterConfiguration;
        public List<Character> Characters;
    }

    public class Manager : MonoBehaviour
    {
        public static Manager main;

        [SerializedObjectTag]
        public Access Access;

        [SerializedObjectTag]
        public Client Client;

        [SerializedObjectTag]
        public Simulation Simulation;

        [SerializedObjectTag]
        public Prefabs Prefabs;

        [Space(10f)]
        [ReadOnly]
        public MainCameraComponent MainCameraComponent;
        [ReadOnly]
        public UIHolder UI;
        [SerializedObjectTag]
        public Chat Chat;

        void Awake()
        {
            SingletonUtil.InitializeSingleton(this);
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            Simulation.CurrentTick += Simulation.TickInSeconds;
            SimulationUtil.AddState(State.INTIALIZE);
        }

        void Update()
        {
            SimulationUtil.ManageStateChanging();

            State currentState = SimulationUtil.GetCurrentState();

            SimulationUtil.UpdateTick();

            switch (currentState)
            {
                case State.INTIALIZE:
                    AccessUtil.LoadAccess();
                    SimulationUtil.InitializeSimulation();
                    CameraUtil.InitializeMainCameraComponent();
                    UIUtil.InitializeUI();
                    SimulationUtil.SetState(State.LOGIN);
                    break;
                case State.LOGIN:
                    {
                        if (Simulation.Backend != null)
                        {
                            SimulationUtil.SetState(State.CONNECTING);
                        }
                    }
                    break;
                case State.IN_CHAT:
                    {

                        if (!ClientUtil.IsConnected())
                        {
                            UnityEngine.Debug.Log("Disconnected");

                            if (Simulation.Backend != null && Simulation.Backend.HasExited)
                            {
                                Simulation.Backend = null;
                            }

                            SimulationUtil.SetState(State.LOGIN);
                            break;
                        }

                        if (SimulationUtil.Tick())
                        {
                            FollowerUtil.UpdateFollowers();
                        }

                        ChatUtil.UpdateChat();
                    }
                    break;
                case State.LOGIN_SETTINGS:
                    break;
                case State.CONNECTING:
                    {
                        if (!SimulationUtil.Tick())
                            break;

                        UnityEngine.Debug.Log("Connecting");

                        if (ClientUtil.IsConnected())
                        {
                            SimulationUtil.SetState(State.HUB);
                            UnityEngine.Debug.Log("Connection complete !");
                        }
                    }
                    break;
                case State.HUB:
                    {

                        Client.ReadMessages = true;

                        if (SimulationUtil.Tick())
                        {
                            FollowerUtil.UpdateFollowers();
                        }
                    }
                    break;
            }

            UIUtil.UpdateUI();
        }

        void OnApplicationQuit()
        {
            if (Simulation.Backend == null)
                return;

            Client.Connected = false;
            Client.Followers.Clear();
            ClientUtil.SetClient();

            Simulation.Backend.CloseMainWindow();
        }

#if UNITY_EDITOR
        void OnGUI()
        {
            GameDebug.DebugGUI();
        }
#endif
    }
}