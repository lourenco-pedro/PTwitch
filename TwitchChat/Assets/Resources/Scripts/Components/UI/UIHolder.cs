using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchChat_frntEnd
{
    public class UIHolder : MonoBehaviour
    {

        public Canvas Canvas;

        public Camera Camera;
        public Camera UICamera;

        [Space(10f)]
        public List<UIPanel> OpennedPanels;
        public List<PanelType> InstantiatedPanels;

        [Space(10f)]
        [ReadOnly]
        public LoginPanelComponent LoginPanelComponent;
        [ReadOnly]
        public LoginSettingsPanelComponent LoginSettingsPanelComponent;
        [ReadOnly]
        public UIPanel ConnectingPanelComponent;
        [ReadOnly]
        public HubPanelComponent HubPanelComponent;
        [ReadOnly]
        public InChatPanelComponent InChatPanelComponent;

        [Space(10f)]
        public PanelType PanelToOpen;
        public PanelType PanelToClose;
    }
}
