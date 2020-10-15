using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] LayerMask stairsLayer;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] float slowingOnStairsParametr;
    bool isStandingOnStairs;
    [Header("Slowing")]
    [SerializeField] float timeOnSlowing;
    float timeSinceStartedSlowing = 0f;
    bool startToSlowing;
    float lastVelocityOnXAxis;

    [Header("Invisible Wall Touching")]
    [SerializeField] LayerMask invisibleWallLayer;
    [SerializeField] Vector2 invisibleWallCheckSize;
    [SerializeField] Transform invisibleWallCheckPoint;
    bool isTouchingInvisibleWall;

    private enum StatesOfMove
    {
        Run, Walk, Stand
    }
    private StatesOfMove currentStateMove;

    Vector2 currentTarget;
    Rigidbody2D myRigidbody2D;
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
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        DetectorEnemiesInAttackZone = GetComponent<DetectorEnemiesInAttackZone>();
        patrolman = GetComponent<Patrolman>();
    }
    private void Update()
    {
        CheckTargetType();
        CheckTouchingGround();
        CheckTouchingInvisibleWall();
    }


    private void CheckTouchingGround()
    {
        isStandingOnStairs = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, stairsLayer);
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
        if (currentStateMove == StatesOfMove.Stand && !startToSlowing)
        {
            startToSlowing = true;
            timeSinceStartedSlowing = Time.deltaTime;
            lastVelocityOnXAxis = myRigidbody2D.velocity.x;
        }
        else
        {
            startToSlowing = false;
        }
        if (startToSlowing && timeOnSlowing + timeSinceStartedSlowing >= Time.deltaTime)
        {
            float slowingParameter;
            if (timeOnSlowing != 0)
            {
                slowingParameter = 1f - (Time.deltaTime - timeSinceStartedSlowing) / timeOnSlowing;
            }
            else
            {
                slowingParameter = 0f;
            }
            myRigidbody2D.velocity = new Vector2(lastVelocityOnXAxis * slowingParameter, myRigidbody2D.velocity.y);
        }
        else if (startToSlowing && timeOnSlowing + timeSinceStartedSlowing <= Time.deltaTime)
        {
            myRigidbody2D.velocity = new Vector2(0f, myRigidbody2D.velocity.y);
        }
    }

    private void SlowingOnStairs()
    {
        if (isStandingOnStairs)
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x / slowingOnStairsParametr, myRigidbody2D.velocity.y);
            if (currentStateMove == StatesOfMove.Stand)
            {

                myRigidbody2D.gravityScale = 0f;
                myRigidbody2D.velocity = new Vector2(0f, 0f);

            }
            else
            {
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

    public bool IsWalking()
    {
        return currentStateMove == StatesOfMove.Walk ? true : false;
    }
    public bool IsRunning()
    {
        return currentStateMove == StatesOfMove.Run ? true:false;
    }
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);
        Gizmos.color = Color.red;
        Gizmos.DrawCube(invisibleWallCheckPoint.position, invisibleWallCheckSize);
    }
}
