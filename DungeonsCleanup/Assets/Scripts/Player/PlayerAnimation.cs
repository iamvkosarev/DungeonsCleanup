using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private float delayBeforeRelife = 1.5f;
    [SerializeField] MovingJoystickProperties movingJoystickProperties;
    [SerializeField] bool hasPlayerLowerSpecialAnimation = false;

    LoseMenuScript loseMenuScript;
    PlayerActionControls playerActionControls;
    PlayerMovement playerMovementScript;
    Rigidbody2D myRigitBody;
    public EventHandler OnReadyForLife;
    Animator myAnimator;
    bool wasSlidingOnWall;

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
    bool isTumbleweed;


    private void Start()
    {
        playerMovementScript = GetComponent<PlayerMovement>();
        myRigitBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        loseMenuScript = GetComponent<PlayerHealth>().GetLoseCanvasScripts();
        if (loseMenuScript)
        {
            loseMenuScript.OnPlayerRelife += StartReLife;
        }
    }

    private void Update()
    {
        ClearAllParameters();
        CheckPlayerPos();
        SetParameters();
    }

    public void DoDeathAnimation()
    {
        myAnimator.Play("Death");
    }
    public void DoIdle()
    {
        myAnimator.Play("Idle");
    }
    public void StartReLife(object obj, EventArgs e)
    {
        StartCoroutine(StartingReLife(delayBeforeRelife));
    }
    public void ReadyForMove()
    {
        DoIdle();
        if (OnReadyForLife != null)
        {
            OnReadyForLife.Invoke(this, EventArgs.Empty);
        }
    }
    IEnumerator StartingReLife(float delayBeforeRelife)
    {
        yield return new WaitForSeconds(delayBeforeRelife);

        Debug.Log("Сделать анимацию перерождения");
        myAnimator.Play("ReLife");
    }
    private void CheckPlayerPos()
    {
        if (wasSlidingOnWall && !playerMovementScript.IsWallSliding())
        {
            wasSlidingOnWall = false;
            myAnimator.Play("Idle");
        }
        if (playerMovementScript.IsTumbleweed() && hasPlayerLowerSpecialAnimation && playerMovementScript.IsPlyerStanding())
        {
            isTumbleweed = true;
        }
        else if (playerMovementScript.IsWallSliding())
        {
            myAnimator.Play("Wall Slide");
            wasSlidingOnWall = true;
        }
        else if (playerMovementScript.IsPlyerStanding())
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
        Vector2 joystickVector = playerMovementScript.GetMovementParameters();
        float joystickXAxis = joystickVector.x;
        float joystickYAxis = joystickVector.y;
        float joystickXAxisAbs = Mathf.Abs(joystickXAxis);
        if (playerMovementScript.IsAttackButtonPressed())
        {
            isAttacking = true;
        }
        else if (joystickYAxis >= movingJoystickProperties.GetJumpLimit())
        {
            isMakingJump = true;
        }
        else if(Mathf.Abs(myRigitBody.velocity.x) <= 0.5f)
        {
            isIdling = true;
        }
        else if(joystickXAxisAbs >= movingJoystickProperties.GetWalkLimit() & joystickXAxisAbs < movingJoystickProperties.GetRunLimit())
        {
            isWalking = true;
        }
        else if(Mathf.Abs(myRigitBody.velocity.x) > 1f)
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
        myAnimator.SetBool("IsTumbleweed", isTumbleweed);
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
        isTumbleweed = false;
        isGoingDown = false;
    }
}
