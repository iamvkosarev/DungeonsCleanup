using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] MovingJoystickProperties movingJoystickProperties;
    [SerializeField] bool hasPlayerLowerSpecialAnimation = false;

    PlayerActionControls playerActionControls;
    PlayerMovement playerMovementScript;
    Rigidbody2D myRigitBody;
    Animator myAnimator;

    bool isAttackButtonPressed;
    // Parameters
    bool isOnGround;
    bool isRunning;
    bool isWalking;
    bool isIdling;
    bool isMakingJump;
    bool isAttacking;
    bool isGoingUp;
    bool isGoingDown;
    bool startLanding;
    
    private void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
        playerActionControls = playerMovementScript.GetActionControls();
        myRigitBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        ClearAllParameters();
        CheckPlayerPos();
        SetParameters();
    }


    private void CheckPlayerPos()
    {
        if (playerMovementScript.IsPlyerStanding())
        {
            isOnGround = true;
            CheckJoystickPos();
        }
        else
        {
            CheckYVelocityDirection();
        }
    }

    private void CheckYVelocityDirection()
    {
        if(myRigitBody.velocity.y >= 0f)
        {
            isGoingUp = true;
        }
        else
        {
            isGoingDown = true;
        }
    }


    private void CheckJoystickPos()
    {
        Vector2 joystickVector = playerActionControls.Land.Move.ReadValue<Vector2>();
        float joystickXAxis = joystickVector.x;
        float joystickYAxis = joystickVector.y;
        float joystickXAxisAbs = Mathf.Abs(joystickXAxis);
        if (joystickYAxis >= movingJoystickProperties.GetJumpLimit())
        {
            isMakingJump = true;
        }
        else if(joystickYAxis <= movingJoystickProperties.GetLowerMovementLimit() && hasPlayerLowerSpecialAnimation)
        {
            // Кувырок
        }
        else if(joystickXAxisAbs < movingJoystickProperties.GetWalkLimit())
        {
            isIdling = true;
        }
        else if(joystickXAxisAbs >= movingJoystickProperties.GetWalkLimit() & joystickXAxisAbs < movingJoystickProperties.GetRunLimit())
        {
            isWalking = true;
        }
        else if(joystickXAxisAbs >= movingJoystickProperties.GetRunLimit())
        {
            isRunning = true;
        }
    }

    private void SetParameters()
    {
        myAnimator.SetBool("IsOnGround", isOnGround);
        myAnimator.SetBool("IsRunning", isRunning);
        myAnimator.SetBool("IsWalking", isWalking);
        myAnimator.SetBool("IsIdling", isIdling);
        myAnimator.SetBool("IsMakingJump", isMakingJump);
        myAnimator.SetBool("IsAttacking", isAttacking);
        myAnimator.SetBool("IsGoingUp", isGoingUp);
        myAnimator.SetBool("isGoingDown", isGoingDown);
        if (startLanding)
        {
            myAnimator.SetTrigger("Landing");
        }
    }

    private void ClearAllParameters()
    {
        isOnGround = false;
        isRunning = false;
        startLanding = false;
        isWalking = false;
        isIdling = false;
        isMakingJump = false;
        isAttacking = false;
        isGoingUp = false;
        isGoingDown = false;
    }
}
