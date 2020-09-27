using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolman : MonoBehaviour
{
    [Header("To check patrol points")]
    [SerializeField] float radiusOfPointReachingZone;
    [SerializeField] LayerMask patrolPointsLayer;
    [SerializeField] Transform patrolPointCheackerCoordinates;
    PatrolPoint currentPatrolPoint;
    int currentPatrolPointNum;
    [Header("To check enemy")]
    [SerializeField] float sizeOfPlayerDetecterRay;
    [SerializeField] Transform playerDetecterRayCoordinates;
    [SerializeField] float maxDeflectionAngle;
    [SerializeField] LayerMask playerAndEnvironmentLayers;
    [SerializeField] int playerLayerNum;
    [SerializeField] float timeOnRayLoopUpdate;
    [SerializeField] bool turnRayInOppositeDirection;
    float currentAngle;
    float currentTimeInLoop;
    bool isPlayerDetecterRayAngleIncreases;
    float parameterOfTurningRayAlongXAxis;

    bool canPatrolmanGetNewPoint;
    bool goToPoint;
    bool goToPlayer;
    Vector2 directionPlayerDetecterRay;

    EnemysMovement myMovementScript;

    private void Start()
    {
        myMovementScript = GetComponent<EnemysMovement>();
        currentAngle = -maxDeflectionAngle;
        isPlayerDetecterRayAngleIncreases = true;
        currentTimeInLoop = 0;
        timeOnRayLoopUpdate /= 2f;
    }
    
    private void Update()
    {
        CheckIsPointFree();
        CheckReachingPoint();
        UpdatePlayerDetecterRayAngle();
        CheckPlayerDetected();
    }


    private void CheckReachingPoint()
    {
        if (currentPatrolPoint == null) { return; }
        Collider2D pointsCollider = (Collider2D)Physics2D.OverlapCircle(patrolPointCheackerCoordinates.position,
            radiusOfPointReachingZone, patrolPointsLayer);
        if (pointsCollider == null) { return; }
        if (pointsCollider.gameObject == currentPatrolPoint.gameObject)
        {

            StartCoroutine(WaitingOnPoint());

        }
    }
    public void StartPursuingPlayer(Transform playersPos)
    {
        goToPoint = false;
        goToPlayer = true;
        if (currentPatrolPoint!= null)
        {
            currentPatrolPoint.StopPursuing();
            currentPatrolPoint = null;
        }
        myMovementScript.SetTarget(playersPos);
    }
    public void TurnRayInOppositeDirection()
    {
        turnRayInOppositeDirection = !turnRayInOppositeDirection;
    }
    IEnumerator WaitingOnPoint()
    {
        PatrolPoint myLastPatrolPoint = currentPatrolPoint;
        currentPatrolPoint = null;
        goToPoint = false;
        yield return new WaitForSeconds(myLastPatrolPoint.GetTimeOnStand());
        myLastPatrolPoint.StopPursuing();
        canPatrolmanGetNewPoint = true;
    }
    public void SetPatrolPoint(PatrolPoint patrolPoint, int patrolPointNum)
    {
        this.currentPatrolPoint = patrolPoint;
        this.currentPatrolPointNum = patrolPointNum;
        canPatrolmanGetNewPoint = false;
    }
    private void CheckIsPointFree()
    {
        if (goToPoint || currentPatrolPoint == null) { return; }
        bool isPointFree = currentPatrolPoint.IsPointFree();
        if (isPointFree)
        {
            currentPatrolPoint.StartedPursuing();
            goToPoint = true;
            myMovementScript.SetTarget(currentPatrolPoint.transform);
        }
    }
    public int GetCurrentPatrolPointNum()
    {
        return currentPatrolPointNum;
    }
    public bool ShoulIGoToPatrolPoint()
    {
        return goToPoint;
    }
    public bool ShoulIGoToPlayer()
    {
        return goToPlayer;
    }

    public bool CanPatrolmanGetNewPoint()
    {
        return canPatrolmanGetNewPoint;
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

    private void CheckPlayerDetected()
    {
        currentAngle = (currentTimeInLoop / timeOnRayLoopUpdate) * maxDeflectionAngle * 2f - maxDeflectionAngle;
        parameterOfTurningRayAlongXAxis = Mathf.Sign(transform.rotation.y) * (turnRayInOppositeDirection ? 1 : -1);
        directionPlayerDetecterRay = new Vector2(Mathf.Cos(currentAngle / 90f * Mathf.PI) * sizeOfPlayerDetecterRay * parameterOfTurningRayAlongXAxis,
            Mathf.Sin(currentAngle / 90f * Mathf.PI) * sizeOfPlayerDetecterRay);
        RaycastHit2D hit = Physics2D.Raycast(playerDetecterRayCoordinates.position, directionPlayerDetecterRay, sizeOfPlayerDetecterRay, playerAndEnvironmentLayers);
        if (!hit) { return; }
        if (hit.collider.gameObject.layer == playerLayerNum)
        {
            StartPursuingPlayer(hit.collider.gameObject.transform);
            Debug.Log($"{gameObject.name} обнаружил игрока");
        }
    }

    private void OnDestroy()
    {
        if(currentPatrolPoint == null) { return; }
        currentPatrolPoint.StopPursuing();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(patrolPointCheackerCoordinates.position, radiusOfPointReachingZone);

        Gizmos.color = Color.gray;
        Gizmos.DrawRay(playerDetecterRayCoordinates.position, new Vector2(Mathf.Cos(currentAngle / 90f * Mathf.PI) * sizeOfPlayerDetecterRay * parameterOfTurningRayAlongXAxis, 
            Mathf.Sin(currentAngle / 90f * Mathf.PI) * sizeOfPlayerDetecterRay));
    }
}
