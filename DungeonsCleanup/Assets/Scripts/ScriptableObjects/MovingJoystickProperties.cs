using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "JoystickProperties")]
public class MovingJoystickProperties : ScriptableObject
{
    [Range(0f,1f)][SerializeField] float walkLimit = 0.2f;
    [Range(0f, 1f)] [SerializeField] float runLimit = 0.7f;
    [Range(0f, 1f)] [SerializeField] float jumpLimit = 0.7f;
    [Range(0f, 1f)] [SerializeField] float lowerMovementLimit = 0.7f;

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

