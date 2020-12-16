using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimation : MonoBehaviour
{
    [SerializeField] private int maxNumOfTurnings;
    Animator myAnimator;
    Patrolman myPatrolmanScript;
    CharacterAttackChecker characterAttackCheker;
    CharacterNavigatorController characterNavigatorController;
    Rigidbody2D myRigidbody2D;
    EnemiesMovement myMovementScript;
    bool isAttacking;
    bool isWalking;
    bool isTurningHead;
    bool isRunning;
    int countTrunings = 1;
    int numOfTurning = 0;
    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        characterNavigatorController = GetComponent<CharacterNavigatorController>();
        myAnimator = GetComponent<Animator>();
        myPatrolmanScript = GetComponent<Patrolman>();
        characterAttackCheker = GetComponent<CharacterAttackChecker>();
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
        if (characterAttackCheker.detected)
        {
            isAttacking = true;
            countTrunings = 1;
            numOfTurning = -1;
        }
        else if (characterNavigatorController.movementType == MovementType.Walk)
        {
            isWalking = true;
            countTrunings = 1;
            numOfTurning = -1;
        }
        else if (characterNavigatorController.movementType == MovementType.Run)
        {
            isRunning = true;
            countTrunings = 1;
            numOfTurning = -1;
        }
        // turn head;
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
