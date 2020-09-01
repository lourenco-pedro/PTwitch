using UnityEngine;

namespace PCharacter
{
    public interface IPCharacterInstance
    {
        Character Base { get; }
        Transform Transform { get; }
        string Name { get; set; }
        SpriteRenderer Render { get; }
        GameObject RenderObject { get; }
        Rigidbody2D Rig { get; set; }
        PCharacterInstanceController Controller { get; set; }
        PCharacterInstanceAnimation CurrentAnimation { get; set; }
    }
}