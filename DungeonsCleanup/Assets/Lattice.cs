using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lattice : MonoBehaviour
{
    [SerializeField] private Transform activationPoint;
    [SerializeField] private Vector2 activationZone;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private bool openLatticeOnBossDeath = false;
    [SerializeField] private HealthUI bossHealth;
    private bool wasOpened = true;
    private bool checkActivationZone = true;
    private Animator myAnimator;
    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckActivationZone();
        ChechBossDeath();
    }

    private void ChechBossDeath()
    {
        if(!openLatticeOnBossDeath) { return; }
        if(bossHealth.health == 0 && !wasOpened)
        {
            wasOpened = true;
            myAnimator.Play("Opening Animation");
            checkActivationZone = false;
        }
    }

    private void CheckActivationZone()
    {
        if (!checkActivationZone) { return; } 
        Collider2D collider2D = Physics2D.OverlapBox(activationPoint.position, activationZone, 0, playerLayer);
        if (collider2D)
        {
            myAnimator.Play("Closing Animation");
            wasOpened = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(activationPoint.position, activationZone);
    }
}
