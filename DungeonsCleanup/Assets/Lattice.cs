using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lattice : MonoBehaviour
{
    [SerializeField] private Transform activationPoint;
    [SerializeField] private Vector2 activationZone;
    [SerializeField] private LayerMask playerLayer;
    private Animator myAnimator;
    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckActivationZone();
    }

    private void CheckActivationZone()
    {
        Collider2D collider2D = Physics2D.OverlapBox(activationPoint.position, activationZone, 0, playerLayer);
        if (collider2D)
        {
            myAnimator.Play("Closing Animation");
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(activationPoint.position, activationZone);
    }
}
