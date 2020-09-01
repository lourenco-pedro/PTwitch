using UnityEngine;
using System.Collections.Generic;

namespace PCharacter
{
    public interface IPCharacterController
    {
        bool isWallFacing { get; set; }
        PControllerActionType CurrentAction { get; set; }
        List<PControllerActionType> Actions { get; set; }
    }
}