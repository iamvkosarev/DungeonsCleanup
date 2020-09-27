using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] float walkSpeed;
    [SerializeField] bool isWalking; // Временно сделал true, чтобы ходть по точкам
    [SerializeField] float runSpeed;
    bool isRunning;
    [SerializeField] LayerMask stairsLayer;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] float slowingOnStairsParametr;
    bool isStandingOnStairs;

    Transform currentTarget;
    Rigidbody2D myRigidbody2D;
    Patrolman patrolman;
    bool doWeKnowTargetDirection;
    float signXAxisDirection;
    bool facingRight = false;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
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
        if (patrolman.ShoulIGoToPatrolPoint()) {
            isWalking = true;
            return;
        }
        isWalking = false;

    }

    public void SetTarget(Transform newTarget)
    {
        currentTarget = newTarget;
        doWeKnowTargetDirection = false;
    }
    private void FixedUpdate()
    {
        Movement();
        SlowingOnStairs();
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

    private void Movement()
    {
        if (currentTarget == null) { return; }
        if (!doWeKnowTargetDirection)
        {
            GetDirection();
            doWeKnowTargetDirection = true;
        }
        Run();
        Walk();
    }
    private void GetDirection()
    {
        signXAxisDirection = Mathf.Sign(currentTarget.transform.position.x - transform.position.x);
        if (signXAxisDirection < 0 && facingRight)
        {
            Flip();
        }
        else if (signXAxisDirection > 0 && !facingRight)
        {
            Flip();
        }
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
