using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorOfEnemysGettingIntoAttackZone : MonoBehaviour
{
    [SerializeField] Transform detectorPoint;
    [SerializeField] Vector2 detectorZone;
    [SerializeField] LayerMask enemysLayer;
    private bool isEnemyDetectedInAttackZone;

    private void Update()
    {
        CheckingEnemies();
    }
    public bool IsEnemyDetected()
    {
        return isEnemyDetectedInAttackZone;
    }
    public bool GetResultOfDetecting() { return isEnemyDetectedInAttackZone; }

    private void CheckingEnemies()
    {
        isEnemyDetectedInAttackZone = (bool)Physics2D.OverlapBox(detectorPoint.position, detectorZone, 0, enemysLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(detectorPoint.position, detectorZone);
    }
}
