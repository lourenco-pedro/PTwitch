using UnityEngine;
using System.Collections.Generic;

namespace TwitchChat_frntEnd
{
    public static class SimulationUtil
    {

        public static Simulation GetSimulation()
        {
            return SingletonUtil.GetMain().Simulation;
        }

        public static void InitializeSimulation()
        {
            Simulation simulation = GetSimulation();
            simulation.FollowersToAdd = new Queue<Follower>();
        }

        public static void UpdateTick()
        {
            Simulation simulation = GetSimulation();

            if (simulation.Tick)
                simulation.Tick = false;

            if (Time.time >= simulation.CurrentTick)
            {
                simulation.CurrentTick += simulation.TickInSeconds;
                simulation.Tick = true;
            }
        }

        public static bool Tick()
        {
            Simulation simulation = GetSimulation();
            return simulation.Tick;
        }

        public static void ChangeState(State state, ChangeType addType)
        {
            switch (addType)
            {
                case ChangeType.SET:
                    SetState(state);
                    break;
                case ChangeType.PUSH:
                    AddState(state);
                    break;
                case ChangeType.POP:
                    PopState();
                    break;
            }
        }

        public static void SetState(State state)
        {
            Simulation simulation = GetSimulation();
            simulation.ChangeType = ChangeType.SET;
            simulation.ToState = state;
        }

        public static void AddState(State state)
        {
            Simulation simulation = GetSimulation();
            simulation.ChangeType = ChangeType.PUSH;
            simulation.ToState = state;
        }

        public static void PopState()
        {
            Simulation simulation = GetSimulation();
            simulation.ChangeType = ChangeType.POP;
        }

        public static void ManageStateChanging()
        {
            Simulation simulation = GetSimulation();

            if (simulation.ChangeType != ChangeType.NONE)
            {

                switch (simulation.ChangeType)
                {
                    case ChangeType.SET:
                        simulation.States[simulation.States.Count - 1] = simulation.ToState;
                        break;
                    case ChangeType.PUSH:
                        simulation.States.Add(simulation.ToState);
                        break;
                    case ChangeType.POP:
                        simulation.States.RemoveAt(simulation.States.Count - 1);
                        break;
                }

                simulation.ChangeType = ChangeType.NONE;

                InitializeState();
            }
        }

        public static void InitializeState()
        {
            State state = GetCurrentState();

            switch (state)
            {
                case State.IN_CHAT:
                    ChatUtil.InitializeChat();
                    break;
            }
        }

        public static State GetCurrentState()
        {
            Simulation simulation = GetSimulation();
            return simulation.States[simulation.States.Count - 1];
        }

        public static State GetLastState()
        {
            Simulation simulation = GetSimulation();
            return (simulation.States.Count > 1) ? simulation.States[simulation.States.Count - 2] : State.NONE;
        }


        public static State GetStateByPanelType(PanelType panelType)
        {
            switch (panelType)
            {
                case PanelType.LOGIN:
                    return State.LOGIN;
                case PanelType.LOGIN_SETTINGS:
                    return State.LOGIN_SETTINGS;
                case PanelType.CONNECTING:
                    return State.CONNECTING;
                case PanelType.HUB:
                    return State.HUB;
                case PanelType.IN_CHAT:
                    return State.IN_CHAT;
                default:
                    return State.NONE;
            }
        }
    }
}