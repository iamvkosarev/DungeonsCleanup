using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimation : MonoBehaviour
{
    Animator myAnimator;
    DetectorEnemiesInAttackZone detectorEnemiesInAttackZone;
    EnemiesMovement myMovementScript;
    bool isAttacking;
    bool isWalking;
    bool IsRunning;
    bool facingRight = true;
    private void Start()
    {
        myAnimator = GetComponent<Animator>();
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
        }
        else if (myMovementScript.IsWalking())
        {
            isWalking = true;
        }
        else if (myMovementScript.IsRunning())
        {
            IsRunning = true;
        }
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
        myAnimator.SetBool("IsRunning", IsRunning);
    }
    private void ClearAllParameters()
    {
        isAttacking = false;
        isWalking = false;
        IsRunning = false;
    }
}
