using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorEnemiesInAttackZone : MonoBehaviour
{
    [SerializeField] Transform detectorPoint;
    [SerializeField] float sizeOfPlayerDetecterRay;
    [SerializeField] float maxDeflectionAngle;
    [SerializeField] LayerMask playerAndEnvironmentLayers;
    [SerializeField] int playerLayerNum;
    [SerializeField] float timeOnRayLoopUpdate;
    [SerializeField] bool turnRayInOppositeDirection;
    private bool isEnemyDetectedInAttackZone;
    float currentAngle = 0;
    float currentTimeInLoop;
    float fixedAngle = 0;
    bool isPlayerDetecterRayAngleIncreases;
    Vector2 directionPlayerDetecterRay;
    float parameterOfTurningRayAlongXAxis = -1f;
    private void Start()
    {
        currentAngle = -maxDeflectionAngle;
        currentTimeInLoop = 0;
        timeOnRayLoopUpdate /= 2f;
        isPlayerDetecterRayAngleIncreases = true;
    }
    private void Update()
    {
        UpdatePlayerDetecterRayAngle();
        CheckingEnemies();
    }
    public bool IsEnemyDetected()
    {
        return isEnemyDetectedInAttackZone;
    }

    private void CheckingEnemies()
    {
        if (isEnemyDetectedInAttackZone){ currentAngle = fixedAngle;}
        else { currentAngle = (currentTimeInLoop / timeOnRayLoopUpdate) * maxDeflectionAngle * 2f - maxDeflectionAngle; }
        parameterOfTurningRayAlongXAxis = Mathf.Sign(transform.rotation.y) * (turnRayInOppositeDirection ? 1 : -1);
        directionPlayerDetecterRay = new Vector2(Mathf.Cos(currentAngle / 90f * Mathf.PI) * sizeOfPlayerDetecterRay * parameterOfTurningRayAlongXAxis,
            Mathf.Sin(currentAngle / 90f * Mathf.PI) * sizeOfPlayerDetecterRay);
        RaycastHit2D hit = Physics2D.Raycast(detectorPoint.position, directionPlayerDetecterRay, sizeOfPlayerDetecterRay, playerAndEnvironmentLayers);
        if (!hit)
        {
            isEnemyDetectedInAttackZone = false; 
            return; }
        if (hit.collider.gameObject.layer == playerLayerNum)
        {
            isEnemyDetectedInAttackZone = true;
            fixedAngle = currentAngle;
        }
        else
        {
            isEnemyDetectedInAttackZone = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(detectorPoint.position, new Vector2(Mathf.Cos(currentAngle / 90f * Mathf.PI) * sizeOfPlayerDetecterRay * parameterOfTurningRayAlongXAxis,
            Mathf.Sin(currentAngle / 90f * Mathf.PI) * sizeOfPlayerDetecterRay));
    }
    private void UpdatePlayerDetecterRayAngle()
    {
        if (currentTimeInLoop >= timeOnRayLoopUpdate)
        {
            currentTimeInLoop = timeOnRayLoopUpdate;
            isPlayerDetecterRayAngleIncreases = false;
        }
        else if (currentTimeInLoop <= 0)
        {
            isPlayerDetecterRayAngleIncreases = true;
            currentTimeInLoop = 0;
        }
        if (isPlayerDetecterRayAngleIncreases)
        {
            currentTimeInLoop += Time.deltaTime;
        }
        else
        {
            currentTimeInLoop -= Time.deltaTime;
        }
    }
}
