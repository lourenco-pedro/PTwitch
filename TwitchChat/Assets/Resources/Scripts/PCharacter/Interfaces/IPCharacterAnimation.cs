using UnityEngine;

namespace PCharacter
{
    public interface IPCharacterAnimation
    {
        float Time { get; set; }
        int CurrentFrame { get; set; }
        int FrameCount { get; }
        PCharacterAnimation Clip { get; set; }
    }
}