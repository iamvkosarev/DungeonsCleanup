using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAnimation : MonoBehaviour
{
    Animator myAnimator;
    EnemysMovement myMovementScript;
    bool isAttacking;
    bool isWalking;
    bool facingRight = true;
    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        myMovementScript = GetComponent<EnemysMovement>();
    }

    private void Update()
    {
        ClearAllParameters();
        CheckScripts();
        SetParameters();
    }

    private void CheckScripts()
    {
        CheckMovmentScript();
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
    }
    private void ClearAllParameters()
    {
        isAttacking = false;
        isWalking = false;
    }
}
