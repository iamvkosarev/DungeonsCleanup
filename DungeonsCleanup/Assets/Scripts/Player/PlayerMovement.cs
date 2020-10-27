﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private MovingJoystickProperties movingJoystickProperties;

    [Header("For Horizontal Movement")]
    [SerializeField] private float runSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float startingMovingTransitionTime;
    [SerializeField] private float endingMovingTransitionTime;
    [SerializeField] private Vector2 slowingOnStairsParametr;
    [SerializeField] private float horizontalMovingSuspendDelay;
    private bool areHorizontalMovingSuspended= false;
    private float velocityOnTheStartOfTransition;
    private float velocityInTheEndOfTransition;
    private float timeSinceStartTransition;

    private enum StateOFMove
    {
        Run, Walk, Stand, TransitonUp, TransitionDown
    }
    private StateOFMove currentState;
    private StateOFMove previousState;
    private bool isMovingStateManagementSuspended;

    [Header("Ground Jump")]
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask stairsLayer;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private Color groundCheckColor;
    [SerializeField] private GameObject hazePrefab;
    [SerializeField] private float checkStairsRayLength;
    [SerializeField] private float groundJumpsDelay;
    private bool isStandingOnFloor;
    private bool isStandingOnGround;
    private bool isStandingOnStairs;
    private bool canJump;
    private bool areGroundJumpsSuspended = false;

    [Header("Slide")]
    [SerializeField] private float wallSlideSpeed = 0;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private Transform wallCheckPoint;
    [SerializeField] private Vector2 wallCheckSize;
    private bool isTouchingWall;
    private bool isWallSliding;

    [Header("WallJump")]
    [SerializeField] private float wallJumpForce = 18f;
    [SerializeField] private float wallJumpDirection = -1f;
    [SerializeField] private Vector2 wallJumpAngle;
    [SerializeField] private float wallJumpsDelay;
    private bool areWallJumpsSuspended = false;


    [Header("Player Elements")]
    [SerializeField] private GameObject bodyChild;

    [Header("VFX")]
    [SerializeField] private GameObject runParticlesPrefab;
    [SerializeField] private ParticleSystem runParticals;
    [SerializeField] private float particlesDestroyDelay = 0.1f;

    [Header("Tumbleweed")]
    [SerializeField] private float tumbleweedSpeed;
    [SerializeField] private CapsuleCollider2D feetCollider;
    [SerializeField] private CapsuleCollider2D feetNotTouchingFeetCollide;
    [SerializeField] private Transform firstPointForCheckingEnemiesDuringATumbleweed;
    [SerializeField] private Transform secondPointForCheckingEnemiesDuringATumbleweed;
    [SerializeField] private LayerMask enemyLayer;
    private bool wasFirstPointTouched;
    private bool wasSecondPointTouched;
    private bool isTumbleweed;

    //catching files
    private PlayerActionControls playerActionControls;
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private PlayerHealth myHealth;
    private PlayerAttackManager myAttackManager;

    // param
    private bool wasJumpRecently = false;
    private bool facingRight = true;
    private bool isAttackButtonPressed;
    private bool canRotation = true;
    private float joystickXAxis;
    private float joystickYAxis;

    #region Customization Player Action Controls
    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        // Attack
        playerActionControls.Land.Attack.started += _ => isAttackButtonPressed = true;
        playerActionControls.Land.Attack.canceled += _ => isAttackButtonPressed = false;
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }
    private void OnDisable()
    {
        playerActionControls.Disable();
    }
    #endregion

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myHealth = GetComponent<PlayerHealth>();
        myAttackManager = GetComponent<PlayerAttackManager>();
    }
    void Update()
    {
        Inputs();
        CheckTouching();
        CheckTumbleweed();
        UpdateColliderInBody();
        PlayerRotation();
        CheckingEnemiesDuringATumbleweed();
        ManageStateOfMove();
    }


    void FixedUpdate()
    {
        HorizontalMove();
        Jump();
        WallSlide();
        WallJump();
        SlowingOnStairs();
        tumbleweedMove();
    }


    private void Inputs()
    {
        Vector2 joystickVector = playerActionControls.Land.Move.ReadValue<Vector2>();
        joystickXAxis = joystickVector.x;
        joystickYAxis = joystickVector.y;
        canJump = (joystickYAxis >= movingJoystickProperties.GetJumpLimit()) ? true : false;
    }

    #region Touching

    private void CheckTouching()
    {
        isStandingOnFloor = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, feetCollider.size.y / 2 * checkStairsRayLength, layerMask: groundLayer);

        if (hit && isStandingOnFloor)
        {
            if (hit.normal != Vector2.up)
            {
                isStandingOnStairs = true;
                isStandingOnGround = false;
            }
            else
            {
                isStandingOnGround = true;
                isStandingOnStairs = false;
            }
        }
        else
        {
            isStandingOnGround = false;
            isStandingOnStairs = false;
        }
        isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer);
    }
    public bool IsPlyerStanding()
    {
        return isStandingOnFloor;
    }
    #endregion

    #region Tumbleweed (Подкат)

    private void tumbleweedMove()
    {
        if (isTumbleweed)
        {
            myRigidbody2D.velocity = new Vector2(tumbleweedSpeed * Mathf.Sign(transform.rotation.y), myRigidbody2D.velocity.y);
        }
    }

    private void CheckingEnemiesDuringATumbleweed()
    {
        if (isTumbleweed)
        {
            if (Physics2D.OverlapPoint(firstPointForCheckingEnemiesDuringATumbleweed.position, enemyLayer))
            {
                wasFirstPointTouched = true;
            }
            if (Physics2D.OverlapPoint(secondPointForCheckingEnemiesDuringATumbleweed.position, enemyLayer))
            {
                wasSecondPointTouched = true;
            }
        }
        else
        {
            wasFirstPointTouched = false;
            wasSecondPointTouched = false;
        }
    }
    public void TurnAroundIfWentThrowEnemy()
    {
        if (wasSecondPointTouched && wasFirstPointTouched)
        {
            Flip();
        }
    }

    private void CheckTumbleweed()
    {
        if (joystickYAxis <= -movingJoystickProperties.GetLowerMovementLimit() && Mathf.Abs(joystickXAxis) >= movingJoystickProperties.GetWalkLimit())
        {
            isTumbleweed = true;
            SetCollidingOfEnemiesMode(false);
            myHealth.SetProtectingMode(true);
        }
    }
    public bool IsTumbleweed()
    {
        return isTumbleweed;
    }
    public void StopTumbleweed()
    {
        isTumbleweed = false;
        SetCollidingOfEnemiesMode(true);
        myHealth.SetProtectingMode(false);
    }
    public void SetCollidingOfEnemiesMode(bool mode)
    {
        feetNotTouchingFeetCollide.enabled = !mode;
        feetCollider.enabled = mode;
    }
    #endregion

    #region Wall Slide
    private void WallSlide()
    {
        if (isTouchingWall && !isStandingOnFloor && myRigidbody2D.velocity.y < 0f)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, -wallSlideSpeed);
        }
    }
    #endregion

    #region Jump

    #region Ground Jump
    private void Jump()
    {
        if (canJump && isStandingOnFloor && !areGroundJumpsSuspended)
        {
            SpawnHaze();
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpForce);
            canJump = false;
            StartCoroutine(SuspendGroundJumps());
        }
    }


    IEnumerator SuspendGroundJumps()
    {
        areGroundJumpsSuspended = true;
        yield return new WaitForSeconds(groundJumpsDelay);
        areGroundJumpsSuspended = false;
    }
    #endregion
    #region Wall Jump 
    private void WallJump()
    {
        if ((isWallSliding || isTouchingWall) && canJump && !areWallJumpsSuspended)
        {
            //myRigitbody2D.AddForce(new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.x), ForceMode2D.Impulse );
            float playerDirection = Mathf.Sign(transform.rotation.y);
            StartCoroutine(SuspendHorizontalMoving());
            myRigidbody2D.velocity = new Vector2(wallJumpForce * -playerDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.y);
            canJump = false;
            StartCoroutine(SuspendWallJumps());
        }
    }
    IEnumerator SuspendWallJumps()
    {
        areWallJumpsSuspended = true;
        yield return new WaitForSeconds(wallJumpsDelay);
        areWallJumpsSuspended = false;
    }
    #endregion

    public bool WasJumpRecently()
    {
        if (areWallJumpsSuspended || areGroundJumpsSuspended)
        {
            wasJumpRecently = true;
        }
        else if (isStandingOnFloor)
        {
            wasJumpRecently = false;
        }
        return wasJumpRecently;
    }
    #endregion

    #region Rotation
    private void PlayerRotation()
    {
        if (!canRotation) { return; }
        if (joystickXAxis == 0) { return; }
        if (joystickXAxis < 0 && facingRight)
        {
            Flip();
        }
        else if (joystickXAxis > 0 && !facingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        wallJumpDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void StartRotaing()
    {
        canRotation = true;
    }
    public void StopRotating()
    {
        canRotation = false;
    }
    #endregion

    #region Horizontal move
    IEnumerator SuspendHorizontalMoving()
    {
        StopHorizontalMovement();
        yield return new WaitForSeconds(horizontalMovingSuspendDelay);
        StartHorizontalMovement();

    }
    private void ManageStateOfMove()
    {
        if (IsPlyerStanding())
        {
            StartHorizontalMovement();
        }
        float absJpystickXAxis = Mathf.Abs(joystickXAxis);
        if (isMovingStateManagementSuspended && absJpystickXAxis > 0f || currentState == StateOFMove.TransitionDown && isMovingStateManagementSuspended) { return; }
        previousState = currentState;

        #region If player's jumped from wall and then landed on ground before end of supsended we should give acess to move
        
        #endregion


        if (absJpystickXAxis < movingJoystickProperties.GetWalkLimit() || myAnimator.GetBool("IsAttacking"))
        {
            currentState = StateOFMove.Stand;
            if (previousState == StateOFMove.Run || previousState == StateOFMove.Walk)
            {
                SetVolumesOfMoveTransition(0f);
                currentState = StateOFMove.TransitionDown;
            }
            else
            {
                isMovingStateManagementSuspended = false;
            }
        }
        else if (absJpystickXAxis >= movingJoystickProperties.GetWalkLimit()
            && absJpystickXAxis < movingJoystickProperties.GetRunLimit()) // Walk
        {
            currentState = StateOFMove.Walk;
            if (previousState == StateOFMove.Run)
            {
                SetVolumesOfMoveTransition(runSpeed);
                currentState = StateOFMove.TransitionDown;
            }
            else if(previousState == StateOFMove.Stand)
            {
                SetVolumesOfMoveTransition(walkSpeed);
                currentState = StateOFMove.TransitonUp;
            }
            else
            {
                isMovingStateManagementSuspended = false;
            }
        }
        else if (absJpystickXAxis >= movingJoystickProperties.GetRunLimit()) // Run
        {
            currentState = StateOFMove.Run;
            if (previousState == StateOFMove.Stand || previousState == StateOFMove.Walk)
            {
                SetVolumesOfMoveTransition(runSpeed);
                currentState = StateOFMove.TransitonUp;
            }
            else
            {
                isMovingStateManagementSuspended = false;
            }
        }
    }

    private void SetVolumesOfMoveTransition(float velocityInTheEndOfTransition_input)
    {
        isMovingStateManagementSuspended = true;
        timeSinceStartTransition = Time.time;
        velocityOnTheStartOfTransition = myRigidbody2D.velocity.x ;
        velocityInTheEndOfTransition = velocityInTheEndOfTransition_input * Mathf.Sign(joystickXAxis);
    }

    private void HorizontalMove()
    {
        if (areHorizontalMovingSuspended) { return; }
        float velocityYAxis = myRigidbody2D.velocity.y;
        float absJpystickXAxis = Mathf.Abs(joystickXAxis);
        if (!IsPlyerStanding() && absJpystickXAxis == 0 || areHorizontalMovingSuspended) { return; }
        if (currentState == StateOFMove.TransitonUp)
        {
            if (Time.time <= timeSinceStartTransition + startingMovingTransitionTime)
            {
                float increaseRatioOfTheSpeed;
                if (startingMovingTransitionTime == 0)
                {
                    increaseRatioOfTheSpeed = 0;
                }
                else
                {
                    increaseRatioOfTheSpeed = 1 - (Time.time - timeSinceStartTransition) / startingMovingTransitionTime;
                }
                myRigidbody2D.velocity = new Vector2(velocityOnTheStartOfTransition + velocityInTheEndOfTransition - 
                    velocityInTheEndOfTransition * Mathf.Pow(increaseRatioOfTheSpeed, 2), velocityYAxis);
            }
            else
            {
                isMovingStateManagementSuspended = false;
            }
        }
        else if(currentState == StateOFMove.TransitionDown)
        {
            if (Time.time <= timeSinceStartTransition + endingMovingTransitionTime)
            {
                float increaseRatioOfTheSpeed;
                if (endingMovingTransitionTime == 0)
                {
                    increaseRatioOfTheSpeed = 1;
                }
                else
                {
                    increaseRatioOfTheSpeed = (Time.time - timeSinceStartTransition) / endingMovingTransitionTime;
                }
                myRigidbody2D.velocity = new Vector2(velocityOnTheStartOfTransition -
                    (velocityOnTheStartOfTransition - velocityInTheEndOfTransition) * Mathf.Pow(increaseRatioOfTheSpeed, 2), velocityYAxis);
            }
            else
            {
                isMovingStateManagementSuspended = false;
            }
        }
        else if(currentState == StateOFMove.Stand)
        {
            myRigidbody2D.velocity = new Vector2(0f, velocityYAxis);
        }
        else if(currentState == StateOFMove.Run)
        {
            myRigidbody2D.velocity = new Vector2(runSpeed * Mathf.Sign(joystickXAxis), velocityYAxis);
        }
        else if (currentState == StateOFMove.Walk) 
        {
            myRigidbody2D.velocity = new Vector2(walkSpeed * Mathf.Sign(joystickXAxis), velocityYAxis);
        }
    }


    public void StopHorizontalMovement()
    {
        areHorizontalMovingSuspended = true;
    }
    public void StartHorizontalMovement()
    {
        areHorizontalMovingSuspended = false;
    }

    #endregion

    #region Slowing
    private void SlowingOnStairs()
    {
        if (isStandingOnStairs && !WasJumpRecently())
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x / slowingOnStairsParametr.x, myRigidbody2D.velocity.y);
            //myRigidbody2D.velocity += Vector2.down * slowingOnStairsParametr.y * Time.deltaTime; // 50
            
            if ((myRigidbody2D.velocity.x == 0f || joystickXAxis == 0) && !areGroundJumpsSuspended)
            {

                myRigidbody2D.gravityScale = 0f;
                myRigidbody2D.velocity = new Vector2(0f, 0f);

            }
            else
            {
                myRigidbody2D.velocity += Vector2.down * slowingOnStairsParametr.y;
                myRigidbody2D.gravityScale = 1f;
            }
        }
        else
        {
            myRigidbody2D.gravityScale = 1f;
        }
    }
    #endregion

    #region Others
    public bool IsAttackButtonPressed()
    {
        return isAttackButtonPressed;
    }

    public void GetPunch(float pushXForce, float pushYForce)
    {
        myRigidbody2D.AddForce(new Vector2(-pushXForce * Mathf.Sign(transform.localScale.x), pushYForce));
        StartCoroutine(SuspendHorizontalMoving());
    }
    private void UpdateColliderInBody()
    {
        Destroy(bodyChild.GetComponent<PolygonCollider2D>());
        bodyChild.AddComponent<PolygonCollider2D>();
    }
    public PlayerActionControls GetActionControls()
    {
        return playerActionControls;
    }
    public void SpawnRunParticlesVFX(int isDirectionInSameSide)
    {
        int angleOfRotate;
        if (transform.localScale.x >= 0)
        {
            angleOfRotate = isDirectionInSameSide == 1? 0 : 180;
        }
        else
        {
            angleOfRotate = isDirectionInSameSide == 1? 180 : 180;
        }
        GameObject particles = Instantiate(runParticlesPrefab, transform.position + runParticlesPrefab.transform.position, Quaternion.Euler(0, angleOfRotate, 0));
        Destroy(particles, particlesDestroyDelay);
    }
    
    public void SpawnHaze()
    {
        GameObject haze = Instantiate(hazePrefab, transform.position + hazePrefab.transform.position, Quaternion.identity);
        Haze hazeScript = haze.GetComponent<Haze>();

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, feetCollider.size.y / 2 * checkStairsRayLength, layerMask: groundLayer);

        if (hit)
        {            
            if(hit.normal != Vector2.up)
            {
                if(hit.normal.x < 0)
                {
                    hazeScript.RotateHaze(); // если идёт вверх вправо
                }
                hazeScript.SetHazeOnStairs();
            }
        }

        hazeScript.StartAnimation();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = groundCheckColor;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);

        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);
    }
    #endregion
}