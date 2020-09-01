using System;

namespace TwitchChat_frntEnd
{
    public enum State
    {
        NONE,
        INTIALIZE,
        LOGIN,
        LOGIN_SETTINGS,
        CONNECTING,
        IN_CHAT,
        HUB,
    }

    public enum CharacterState
    {
        IDLE,
        WALK
    }

    public enum ChangeType
    {
        NONE,
        SET,
        PUSH,
        POP
    }

    public enum PanelType
    {
        NONE,
        LOGIN,
        LOGIN_SETTINGS,
        CONNECTING,
        HUB,
        IN_CHAT
    }
}