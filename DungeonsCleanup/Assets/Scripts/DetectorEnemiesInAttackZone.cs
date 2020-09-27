using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorEnemiesInAttackZone : MonoBehaviour
{
    [SerializeField] Transform detectorPoint;
    [SerializeField] float detectorRayLength;
    [SerializeField] LayerMask playerAndEnvironmentLayers;
    [SerializeField] int playerLayerNum;
    [SerializeField] bool turnRayInOppositeDirection;
    private bool isEnemyDetectedInAttackZone;
    float parameterOfTurningRayAlongXAxis = -1f;

    private void Update()
    {
        CheckingEnemies();
    }
    public bool IsEnemyDetected()
    {
        return isEnemyDetectedInAttackZone;
    }

    private void CheckingEnemies()
    {
        parameterOfTurningRayAlongXAxis = Mathf.Sign(transform.rotation.y) * (turnRayInOppositeDirection ? 1 : -1);
        RaycastHit2D hit = Physics2D.Raycast(detectorPoint.position, new Vector2(parameterOfTurningRayAlongXAxis, 0), detectorRayLength, playerAndEnvironmentLayers);
        if (hit.collider == null)
        {
            isEnemyDetectedInAttackZone = false; 
        }
        else if (hit.collider.gameObject.layer == 8)
        {
            isEnemyDetectedInAttackZone = true;
        }
        else
        {
            isEnemyDetectedInAttackZone = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(detectorPoint.position, new Vector2(detectorRayLength * parameterOfTurningRayAlongXAxis, 0));
    }
}
