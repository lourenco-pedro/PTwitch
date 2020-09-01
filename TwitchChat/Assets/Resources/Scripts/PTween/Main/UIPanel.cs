/**********************************************************************
* Project           : PTween
*
* Author            : Pedro Pereira Lourenço
*
* Last Update      :  2020‑04‑18
*
* Purpose           : Animate UI elements without Aniamtion clip
**********************************************************************/
 
 
using UnityEngine;
using PTween;

namespace TwitchChat_frntEnd
{
    public class UIPanel : MonoBehaviour
    {
        public PanelType PanelType;
        public PTweenPlayerComponent PanelTransition;
    }
}