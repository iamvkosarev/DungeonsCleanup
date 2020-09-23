using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchManagment : MonoBehaviour
{
    [SerializeField] float changeHatchRotateLimit = 0.3f;
    [SerializeField] float timeOnWait = 1f;
    PlayerActionControls playerActionControls;
    PlatformEffector2D myPlatformEffector2D;
    float waitTime;
    bool isButtonPressed;
    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        myPlatformEffector2D = GetComponent<PlatformEffector2D>();
        playerActionControls.Land.Move.started += _=> isButtonPressed = true;
        playerActionControls.Land.Move.canceled += _ => isButtonPressed = false;
    }

    private bool IsJoystickYAxisLowerButtonLevel()
    {
        float joystickYAxis = playerActionControls.Land.Move.ReadValue<Vector2>().y;
        if (joystickYAxis <= -changeHatchRotateLimit)
        {
            return true;
        }
        return false;

    }
    private bool IsJoystickYAxisBiggerTopLevel()
    {
        float joystickYAxis = playerActionControls.Land.Move.ReadValue<Vector2>().y;
        if (joystickYAxis >= changeHatchRotateLimit)
        {
            return true;
        }
        return false;

    }
    void Update()
    {
        CheckHatchRotate();
    }
    private void CheckHatchRotate()
    {
        if (!isButtonPressed)
        {
            if (waitTime <= 0)
            {
                myPlatformEffector2D.rotationalOffset = 0f;
                waitTime = timeOnWait;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        if (isButtonPressed && IsJoystickYAxisLowerButtonLevel())
        {
            myPlatformEffector2D.rotationalOffset = 180f;
        }
        if (IsJoystickYAxisBiggerTopLevel()  && isButtonPressed)
        {
            myPlatformEffector2D.rotationalOffset = 0f;
        }
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }
    private void OnDisable()
    {
        playerActionControls.Disable();
    }

}
