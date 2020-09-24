using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] Transform detectorPoint;
    [SerializeField] Vector2 detectorToAttack;
    [SerializeField] Vector2 detectorToMoving;
    [SerializeField] LayerMask playerLayer;
    
    private bool isAttackingPlayer;
    private bool isPlayerDetected;

    private void Update()
    {
        CheckingPlayer();
    }

    public bool GetResultOfAttacking() { return isAttackingPlayer; }
    public bool GetResultOfDetected() { return isPlayerDetected; }

    private void CheckingPlayer()
    {
        isAttackingPlayer = (bool)Physics2D.OverlapBox(detectorPoint.position, detectorToAttack, 0, playerLayer);
        isPlayerDetected = (bool)Physics2D.OverlapBox(detectorPoint.position, detectorToMoving, 0, playerLayer);

        Debug.Log(isPlayerDetected);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(detectorPoint.position, detectorToAttack);

        Gizmos.DrawCube(detectorPoint.position, detectorToMoving);
    }

    
}
