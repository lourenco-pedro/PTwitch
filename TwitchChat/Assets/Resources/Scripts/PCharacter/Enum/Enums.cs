using System;

namespace PCharacter
{

    public enum PCharacterType { CHARACTER, SOLID }
    public enum PAnimationType { NORMAL, PING_PONG };
    public enum PControllerActionType { NONE, TRANSLATE_LEFT, TRANSLATE_RIGHT, JUMP }

#if UNITY_EDITOR

    public enum GUIStyleType { NONE, NORMAL_HEADER, MEDIUM_ITALIC_LABEL }
#endif
}