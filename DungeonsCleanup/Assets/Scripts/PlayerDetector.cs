using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector : MonoBehaviour
{
    [SerializeField] Transform detectorPoint;
    [SerializeField] Vector2 detectorZone;
    [SerializeField] LayerMask enemiesLayer;
    bool isEnemyDetected;

    private void Update()
    {
        CheckingEnemies();
    }

    public bool GetResultOfDetecting() { return isEnemyDetected; }

    private void CheckingEnemies()
    {
        isEnemyDetected = (bool)Physics2D.OverlapBox(detectorPoint.position, detectorZone, 0, enemiesLayer);
        Debug.Log(isEnemyDetected);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(detectorPoint.position, detectorZone);
    }
}
