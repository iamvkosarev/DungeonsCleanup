﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapButton : MonoBehaviour
{
    [SerializeField] private int playerFeetLayerNum;
    [SerializeField] private int enemyFeetLayerNum;
    [SerializeField] private bool canEnemyTouchButton = true;
    private Animator animator;
    private Animator parentAnimator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        parentAnimator = transform.parent.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(playerFeetLayerNum == other.gameObject.layer || enemyFeetLayerNum == other.gameObject.layer && canEnemyTouchButton)
        {
            animator.SetBool("On", true);
            parentAnimator.SetBool("Shot", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (playerFeetLayerNum == other.gameObject.layer || enemyFeetLayerNum == other.gameObject.layer && canEnemyTouchButton)
        {
            animator.SetBool("On", false);
            parentAnimator.SetBool("Shot", false);
        }
    }


}