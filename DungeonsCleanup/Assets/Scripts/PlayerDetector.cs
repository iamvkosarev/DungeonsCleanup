using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorOfEnemysGettingIntoAttackZone : MonoBehaviour
{
    [SerializeField] Transform detectorPoint;
    [SerializeField] Vector2 detectorZone;
    [SerializeField] LayerMask enemysLayer;
    private bool isEnemyDetected;

    private void Update()
    {
        CheckingEnemies();
    }

    public bool GetResultOfDetecting() { return isEnemyDetected; }

    private void CheckingEnemies()
    {
        isEnemyDetected = (bool)Physics2D.OverlapBox(detectorPoint.position, detectorZone, 0, enemysLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(detectorPoint.position, detectorZone);
    }
}
