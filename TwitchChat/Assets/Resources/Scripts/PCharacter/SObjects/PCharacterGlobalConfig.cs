using UnityEngine;

namespace PCharacter
{
    [CreateAssetMenu(fileName = "PCharacterGlobalConfig", menuName = "PCharacter/GlobalConfig")]
    public class PCharacterGlobalConfig : ScriptableObject
    {
        [Header("Animation Config")]
        public float DelayToNextFrame = 1f;
        public float FrameSpeedMultiplier = 1f;

        [Space(10f)]
        [Header("Physics Config")]
        public float GravityScale = 1;

        [HideInInspector]
        public RigidbodyConstraints2D Constraints2D;
    }
}