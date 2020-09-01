using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwitchChat_frntEnd
{
    public static class HubUIUtil
    {
        public static void SetupPanel()
        {
            HubPanelComponent hubPanelComponent = (HubPanelComponent)UIPanelUtil.GetUIPanel(PanelType.HUB);
        }
    }
}