using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MovementButtonsManager : MonoBehaviour
{
    Vector2 resultOfPressing = new Vector2(0f,0f);

    public void AddValueInResult(float x, float y)
    {
        resultOfPressing = new Vector2(resultOfPressing.x + x,
        resultOfPressing.y + y);
    }

    public Vector2 GetResult()
    {
        resultOfPressing = new Vector2(resultOfPressing.x != 0 ? Mathf.Sign(resultOfPressing.x) : 0,
            resultOfPressing.y != 0 ? Mathf.Sign(resultOfPressing.y) : 0);
        return resultOfPressing;
    }
}