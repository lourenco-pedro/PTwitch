/**********************************************************************
* Project           : PTween
*
* Author            : Pedro Pereira Lourenço
*
* Last Update      :  2020‑04‑18
*
* Purpose           : Animate UI elements without Aniamtion clip
**********************************************************************/
 
 
using System;
using UnityEngine;

namespace PTween
{
    [System.Serializable]
    public class PTweenInstance
    {
        public PTweenComponent Target;

        [Space(10f)]
        public PTweenTransformClip Position;
        public PTweenTransformClip Rotation;
        public PTweenTransformClip Scale;
        public PtweenAlphaClip Alpha;

        [Space(10f)]
        public PTweenAnimationDirection AnimationDirection;
        public bool Finished;
    }
}