using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimation : MonoBehaviour
{
    [SerializeField] private int maxNumOfTurnings;
    Animator myAnimator;
    Patrolman myPatrolmanScript;
    DetectorEnemiesInAttackZone detectorEnemiesInAttackZone;
    EnemiesMovement myMovementScript;
    bool isAttacking;
    bool isWalking;
    bool isTurningHead;
    bool isRunning;
    bool facingRight = true;
    int countTrunings = 1;
    int numOfTurning = 0;
    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myPatrolmanScript = GetComponent<Patrolman>();
        detectorEnemiesInAttackZone = GetComponent<DetectorEnemiesInAttackZone>();
        myMovementScript = GetComponent<EnemiesMovement>();
    }

    private void Update()
    {
        ClearAllParameters();
        CheckScripts();
        SetParameters();
    }

    private void CheckScripts()
    {
        if (detectorEnemiesInAttackZone.IsEnemyDetected())
        {
            isAttacking = true;
            countTrunings = 1;
            numOfTurning = -1;
        }
        else if (myMovementScript.IsWalking())
        {
            isWalking = true;
            countTrunings = 1;
            numOfTurning = -1;
        }
        else if (myMovementScript.IsRunning())
        {
            isRunning = true;
            countTrunings = 1;
            numOfTurning = -1;
        }
        else if (myPatrolmanScript != null)
        {
            if (myPatrolmanScript.IsWaitingOnPoint())
            {
                if (numOfTurning == -1)
                {
                    numOfTurning = UnityEngine.Random.Range(0, maxNumOfTurnings + 1);
                }
                if (numOfTurning >= countTrunings)
                {
                    isTurningHead = true;
                }
            }
        }
    }

    public void CountTurn()
    {
        countTrunings++;
    }

    private void CheckMovmentScript()
    {
        if (myMovementScript.IsWalking())
        {
            isWalking = true;
        }
    }

    private void SetParameters()
    {
        myAnimator.SetBool("IsAttacking", isAttacking);
        myAnimator.SetBool("IsWalking", isWalking);
        myAnimator.SetBool("IsRunning", isRunning);
        myAnimator.SetBool("isTurningHead", isTurningHead);
    }
    private void ClearAllParameters()
    {
        isAttacking = false;
        isWalking = false;
        isRunning = false;
        isTurningHead = false;
    }
}
