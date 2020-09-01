using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchChat_frntEnd
{
    public static class UIPanelUtil
    {

        public static void ManagePanels()
        {
            UIHolder uiHolder = UIUtil.GetUIHolder();
            Simulation simulation = SimulationUtil.GetSimulation();

            State currentState = SimulationUtil.GetCurrentState();
            State lastState = SimulationUtil.GetLastState();

            PanelType currentPanel = GetPanelTypeByState(currentState);
            PanelType lastPanel = GetPanelTypeByState(lastState);

            if (!IsPanelOpenned(currentPanel) && uiHolder.PanelToOpen == PanelType.NONE)
            {
                uiHolder.PanelToOpen = currentPanel;
            }

            if (uiHolder.PanelToClose == PanelType.NONE)
            {
                for (int i = 0; i < uiHolder.InstantiatedPanels.Count; i++)
                {
                    PanelType instantiatedPanelType = uiHolder.InstantiatedPanels[i];
                    UIPanel instantiatedPanel = GetUIPanel(instantiatedPanelType);
                    State desiredState = SimulationUtil.GetStateByPanelType(instantiatedPanelType);

                    if (instantiatedPanel.gameObject.activeSelf && !simulation.States.Contains(desiredState))
                    {
                        uiHolder.PanelToClose = instantiatedPanelType;
                        break;
                    }
                }
            }

            ManageOpenningPanel();
            ManageClosingPanels();
            UpdateOppenedPanels();
        }

        public static UIPanel GetUIPanel(PanelType panelType)
        {

            UIHolder uiHolder = UIUtil.GetUIHolder();
            UIPanel uiPanel = null;

            Prefabs prefabs = SingletonUtil.GetMain().Prefabs;

            switch (panelType)
            {
                case PanelType.LOGIN:
                    {
                        if (uiHolder.LoginPanelComponent == null)
                        {
                            uiHolder.LoginPanelComponent = GameObject.Instantiate(prefabs.LoginPanelComponent, uiHolder.Canvas.transform);
                            uiHolder.InstantiatedPanels.Add(uiHolder.LoginPanelComponent.PanelType);
                        }

                        uiPanel = uiHolder.LoginPanelComponent;
                    }
                    break;
                case PanelType.LOGIN_SETTINGS:
                    {
                        if (uiHolder.LoginSettingsPanelComponent == null)
                        {
                            uiHolder.LoginSettingsPanelComponent = GameObject.Instantiate(prefabs.LoginSettingsPanelComponent, uiHolder.Canvas.transform);
                            uiHolder.InstantiatedPanels.Add(uiHolder.LoginSettingsPanelComponent.PanelType);
                        }
                        uiPanel = uiHolder.LoginSettingsPanelComponent;
                    }
                    break;
                case PanelType.CONNECTING:
                    {
                        if (uiHolder.ConnectingPanelComponent == null)
                        {
                            uiHolder.ConnectingPanelComponent = GameObject.Instantiate(prefabs.ConnectingPanelComponent, uiHolder.Canvas.transform);
                            uiHolder.InstantiatedPanels.Add(uiHolder.ConnectingPanelComponent.PanelType);
                        }
                        uiPanel = uiHolder.ConnectingPanelComponent;
                    }
                    break;
                case PanelType.HUB:
                    {
                        if (uiHolder.HubPanelComponent == null)
                        {
                            uiHolder.HubPanelComponent = GameObject.Instantiate(prefabs.HubPanelComponent, uiHolder.Canvas.transform);
                            uiHolder.InstantiatedPanels.Add(uiHolder.HubPanelComponent.PanelType);
                        }
                        uiPanel = uiHolder.HubPanelComponent;
                    }
                    break;
                case PanelType.IN_CHAT:
                    {
                        if (uiHolder.InChatPanelComponent == null)
                        {
                            uiHolder.InChatPanelComponent = GameObject.Instantiate(prefabs.InChatPanelComponent, uiHolder.Canvas.transform);
                            uiHolder.InstantiatedPanels.Add(uiHolder.InChatPanelComponent.PanelType);
                        }

                        uiPanel = uiHolder.InChatPanelComponent;
                    }
                    break;
            }

            uiPanel.transform.position = uiHolder.Camera.WorldToScreenPoint(Vector3.zero);

            return uiPanel;
        }

        private static void UpdateOppenedPanels()
        {
            UIHolder uiHolder = UIUtil.GetUIHolder();
            for (int i = 0; i < uiHolder.OpennedPanels.Count; i++)
            {
                UIPanel uiPanel = uiHolder.OpennedPanels[i];

                switch (uiPanel.PanelType)
                {
                    case PanelType.LOGIN:
                        LoginUIUtil.UpdatePanel();
                        break;
                    case PanelType.LOGIN_SETTINGS:
                        break;
                    case PanelType.IN_CHAT:
                        InChatUIUtil.UpdatePanel();
                        break;
                }
            }
        }

        private static void ManageOpenningPanel()
        {
            UIHolder uiHolder = UIUtil.GetUIHolder();

            if (uiHolder.PanelToOpen == PanelType.NONE)
                return;

            OpenPanel(uiHolder.PanelToOpen);

            uiHolder.PanelToOpen = PanelType.NONE;
        }

        private static void ManageClosingPanels()
        {
            UIHolder uiHolder = UIUtil.GetUIHolder();

            if (uiHolder.PanelToClose == PanelType.NONE)
                return;

            UIPanel panelToClose = GetUIPanel(uiHolder.PanelToClose);

            panelToClose.gameObject.SetActive(false);
            uiHolder.OpennedPanels.Remove(panelToClose);
            uiHolder.PanelToClose = PanelType.NONE;
        }

        private static void OpenPanel(PanelType panelType)
        {
            UIHolder uiHolder = UIUtil.GetUIHolder();
            UIPanel uiPanel = GetUIPanel(panelType);

            switch (panelType)
            {
                case PanelType.LOGIN:
                    LoginUIUtil.SetupPanel();
                    break;
                case PanelType.LOGIN_SETTINGS:
                    LoginSettingsUIUtil.SetupPanel();
                    break;
                case PanelType.HUB:
                    HubUIUtil.SetupPanel();
                    break;
                case PanelType.IN_CHAT:
                    break;
            }

            uiPanel.gameObject.SetActive(true);
            uiHolder.OpennedPanels.Add(uiPanel);
        }

        private static bool IsPanelOpenned(PanelType panelType)
        {
            bool isOpenned = false;
            UIHolder uiHolder = UIUtil.GetUIHolder();

            for (int i = 0; i < uiHolder.OpennedPanels.Count; i++)
            {
                UIPanel uiPanel = uiHolder.OpennedPanels[i];
                if (uiPanel.PanelType == panelType)
                {
                    isOpenned = true;
                    break;
                }
            }

            return isOpenned;
        }

        private static PanelType GetPanelTypeByState(State state)
        {
            switch (state)
            {
                case State.LOGIN:
                    return PanelType.LOGIN;
                case State.LOGIN_SETTINGS:
                    return PanelType.LOGIN_SETTINGS;
                case State.CONNECTING:
                    return PanelType.CONNECTING;
                case State.HUB:
                    return PanelType.HUB;
                case State.IN_CHAT:
                    return PanelType.IN_CHAT;
                default:
                    return PanelType.NONE;
            }
        }
    }
}