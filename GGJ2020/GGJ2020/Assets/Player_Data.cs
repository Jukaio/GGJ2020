using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Data
{
    public enum LEVEL
    {
        ZERO,
        ELSE,
        PASSABLE
    }

    public enum ERROR_STATE
    {
        NO_ERROR,
        ERROR,
    }

    public enum AREA_STATE
    {
        AIR,
        GROUND
    }

    public enum AIR_STATE
    {
        BUG = -1,
        RISE,
        MOVE_LEFT_AIR,
        MOVE_RIGHT_AIR,
        FALL,
        FALL_LEFT,
        FALL_RIGHT,
        DOUBLE_JUMP,
    }

    public enum GROUND_STATE
    {
        BUG = -1,
        IDLE_GROUND,
        MOVE_LEFT_GROUND,
        MOVE_RIGHT_GROUND,
        JUMP_ENTER,
        JUMP_LEFT_ENTER,
        JUMP_RIGHT_ENTER,
        DROP_ENTER,
        DROP_ENTER_LEFT,
        DROP_ENTER_RIGHT
    }
}
