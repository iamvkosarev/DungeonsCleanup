using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float walkSpeed;
    bool isWalking; 
    [SerializeField] float runSpeed;
    bool isRunning;
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
    }


    private void CheckTouchingGround()
    {
        isStandingOnStairs = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, stairsLayer);
    }

    private void CheckTargetType()
    {
        if (currentTarget == null) { return; }
        // if (игрок был обнаружен скриптом обнаружения и игрок вне зоны досигаемости атаки)
        //      берём последнии координты (currentTarget из скрипта обнаржуения) игрока и бужим туда isRunning = true; isWalking = false;
        //      return;
        // else if(isRunning == true) // Проверяем бежали ли мы к игроку
        //      Ставим карутину, чтобы постоять на точке isRunning = false; isWalking = false;
        //      После этого идём по точкам объода isRunning = false; isWalking = true;
        if (patrolman.ShoulIGoToPlayer() && !DetectorEnemiesInAttackZone.IsEnemyDetected())
        {
            isRunning = true;
            isWalking = false;
        }
        else if((patrolman.ShoulIGoToPlayer() && DetectorEnemiesInAttackZone.IsEnemyDetected()) || (!patrolman.ShoulIGoToPlayer() && !patrolman.ShoulIGoToPatrolPoint()))
        {
            isRunning = false;
            isWalking = false;
        }
        else if (patrolman.ShoulIGoToPatrolPoint()) {
            isWalking = true;
            isRunning = false;
        }
        else
        {
            isWalking = false;
        }

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
        if (!isRunning && !isWalking && !startToSlowing)
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
            if (!isWalking && !isRunning)
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
        if (!isWalking) { return; }
        myRigidbody2D.velocity = new Vector2(Math.Abs(walkSpeed) * signXAxisDirection, myRigidbody2D.velocity.y);
    }

    private void Run()
    {
        if (!isRunning) { return; }
        myRigidbody2D.velocity = new Vector2(Math.Abs(runSpeed) * signXAxisDirection, myRigidbody2D.velocity.y);
        doWeKnowTargetDirection = false;
    }

    public bool IsWalking()
    {
        return isWalking;
    }
    public bool IsRunning()
    {
        return isRunning;
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
    }
}
