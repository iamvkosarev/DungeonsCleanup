using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float checkStairsRayLength;
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private CapsuleCollider2D feetCollider;
    [SerializeField] private Vector2 groundCheckSize;
    [SerializeField] private Vector2 slowingOnStairsParametr;
    private bool isStandingOnStairs;
    private bool isStandingOnFloor;
    [Header("Slowing")]
    [SerializeField] private float timeOnSlowing;
    private float timeSinceStartedSlowing = 0f;
    private bool startToSlowing;
    private float lastVelocityOnXAxis;

    [Header("Invisible Wall Touching")]
    [SerializeField] private LayerMask invisibleWallLayer;
    [SerializeField] private Vector2 invisibleWallCheckSize;
    [SerializeField] private Transform invisibleWallCheckPoint;
    private bool isTouchingInvisibleWall;
    [Header("Audio")]
    [SerializeField] private AudioClip firstStepSFX;
    [SerializeField] private AudioClip secondStepSFX;
    [SerializeField] private float audioBoost;
    AudioSource myAudioSource;
    private enum StatesOfMove
    {
        Run, Walk, Stand
    }
    private StatesOfMove currentStateMove;

    Vector2 currentTarget;
    Rigidbody2D myRigidbody2D;
    Health myHealth;
    Animator myAnimator;
    DetectorEnemiesInAttackZone DetectorEnemiesInAttackZone;
    Patrolman patrolman;
    bool doWeKnowTargetDirection;
    float signXAxisDirection;
    bool facingRight = false;
    bool canRotate = true;
    bool canMove = true;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myHealth = GetComponent<Health>();
        myAnimator = GetComponent<Animator>();
        DetectorEnemiesInAttackZone = GetComponent<DetectorEnemiesInAttackZone>();
        patrolman = GetComponent<Patrolman>();
    }
    private void Update()
    {
        CheckTargetType();
        CheckTouchingGround();
        CheckTouchingInvisibleWall();
        CheckZeroHealth();
    }

    public void RotateOnHit()
    {
        if (!patrolman.ShoulIGoToPlayer())
        {
            Flip();
        }
    }
    private void CheckZeroHealth()
    {
        if (myHealth.GetHealth() == 0)
        {
            StopRotating();
            StopMoving();
        }
    }

    private void CheckTouchingGround()
    {

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, feetCollider.size.y / 2 * checkStairsRayLength, layerMask: groundLayer);
        isStandingOnFloor = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
        if (hit && isStandingOnFloor)
        {
            if (hit.normal != Vector2.up)
            {
                isStandingOnStairs = true;
            }
            else
            {
                isStandingOnStairs = false;
            }
        }
        else
        {
            isStandingOnStairs = false;
        }
    }
    private void CheckTouchingInvisibleWall()
    {
        isTouchingInvisibleWall = Physics2D.OverlapBox(invisibleWallCheckPoint.position, invisibleWallCheckSize, 0, invisibleWallLayer);
    }
    public bool IsTouchingInvisibleWall()
    {
        return isTouchingInvisibleWall;
    }
    private void CheckTargetType()
    {
        if (currentTarget == null) { return; }

        if ((patrolman.ShoulIGoToPlayer() && DetectorEnemiesInAttackZone.IsEnemyDetected()) || (!patrolman.ShoulIGoToPlayer() && !patrolman.ShoulIGoToPatrolPoint()) || isTouchingInvisibleWall)
        {
            currentStateMove = StatesOfMove.Stand;
        }
        else if (patrolman.ShoulIGoToPlayer() && !DetectorEnemiesInAttackZone.IsEnemyDetected())
        {
            currentStateMove = StatesOfMove.Run;
        }
        else if (patrolman.ShoulIGoToPatrolPoint()) {
            currentStateMove = StatesOfMove.Walk;
        }
        /*else
        {
            currentState = StatesOfMove.Stand;
        }*/

    }

    public void SetTarget(Vector2 newTarget)
    {
        currentTarget = newTarget;
        doWeKnowTargetDirection = false;
    }
    private void FixedUpdate()
    {
        Movement();
        SlowingOnStairs();
        Slowing();
    }

    private void Slowing()
    {
        if (currentStateMove == StatesOfMove.Stand && !startToSlowing  && myRigidbody2D.velocity.x !=0)
        {
            startToSlowing = true;
            timeSinceStartedSlowing = Time.time;
            lastVelocityOnXAxis = myRigidbody2D.velocity.x;
        }
        else if(currentStateMove != StatesOfMove.Stand)
        {
            startToSlowing = false;
        }
        if (startToSlowing && timeOnSlowing + timeSinceStartedSlowing > Time.time)
        {
            float slowingParameter;
            if (timeOnSlowing != 0)
            {
                slowingParameter = 1f - (Time.time - timeSinceStartedSlowing) / timeOnSlowing;
            }
            else
            {
                slowingParameter = 0f;
            }
            myRigidbody2D.velocity = new Vector2(lastVelocityOnXAxis * slowingParameter, myRigidbody2D.velocity.y);
        }
        else if (startToSlowing && timeOnSlowing + timeSinceStartedSlowing <= Time.time)
        {
            myRigidbody2D.velocity = new Vector2(0f, myRigidbody2D.velocity.y);
            startToSlowing = false;
        }
    }

    private void SlowingOnStairs()
    {
        if (isStandingOnStairs)
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x / slowingOnStairsParametr.x, myRigidbody2D.velocity.y);
            if (currentStateMove == StatesOfMove.Stand)
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
    public Vector2 GetCurrentTragetPos()
    {
        return currentTarget;
    }

    private void Movement()
    {
        if (currentTarget == null) { return; }
        if (!doWeKnowTargetDirection)
        {
            GetDirection();
            doWeKnowTargetDirection = true;
        }
        if (!canMove) { return; }
        Run();
        Walk();
    }
    private void GetDirection()
    {
        signXAxisDirection = Mathf.Sign(currentTarget.x - transform.position.x);
        if (signXAxisDirection < 0 && facingRight && canRotate)
        {
            Flip();
        }
        else if (signXAxisDirection > 0 && !facingRight && canRotate)
        {
            Flip();
        }
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public void StopRotating()
    {
        canRotate = false;
    }
    public void StartRotating()
    {
        canRotate = true;
    }
    public void StopMoving()
    {
        canMove = false;
    }
    public void StartMoving()
    {
        canMove = true;
    }
    private void Walk()
    {
        if (currentStateMove != StatesOfMove.Walk) { return; }
        myRigidbody2D.velocity = new Vector2(Math.Abs(walkSpeed) * signXAxisDirection, myRigidbody2D.velocity.y);
    }

    private void Run()
    {
        if (currentStateMove != StatesOfMove.Run) { return; }
        myRigidbody2D.velocity = new Vector2(Math.Abs(runSpeed) * signXAxisDirection, myRigidbody2D.velocity.y);
        doWeKnowTargetDirection = false;
    }
    #region Checkers
    public bool IsWalking()
    {
        return currentStateMove == StatesOfMove.Walk ? true : false;
    }
    public bool IsRunning()
    {
        return currentStateMove == StatesOfMove.Run ? true:false;
    }
    #endregion
    #region Others

    public void SpawnFirstSFX()
    {
        if (firstStepSFX)
        {
            myAudioSource.PlayOneShot(firstStepSFX, audioBoost);
        }
        
    }
    public void SpawnSecondSFX()
    {
        if (secondStepSFX)
        {
            myAudioSource.PlayOneShot(secondStepSFX, audioBoost);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(invisibleWallCheckPoint.position, invisibleWallCheckSize);
    }
    #endregion
}
