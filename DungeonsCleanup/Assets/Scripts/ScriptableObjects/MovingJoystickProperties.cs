using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "JoystickProperties")]
public class MovingJoystickProperties : ScriptableObject
{
    [SerializeField] float walkLimit = 0.2f;
    [SerializeField] float runLimit = 0.7f;
    [SerializeField] float jumpLimit = 0.7f;
    [SerializeField] float lowerMovementLimit = 0.7f;

    public float GetWalkLimit()
    {
        return walkLimit;
    }

    public float GetRunLimit()
    {
        return runLimit;
    }

    public float GetJumpLimit()
    {
        return jumpLimit;
    }

    public float GetLowerMovementLimit()
    {
        return lowerMovementLimit;
    }

}

