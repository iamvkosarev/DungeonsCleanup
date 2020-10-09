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
    [SerializeField] Transform detectorPoint;
    [SerializeField] float maxDeflectionAngle;
    [SerializeField] LayerMask playerAndCollidingEnvironmentLayers;
    [SerializeField] int playerLayerNum;
    [SerializeField] float timeOnRayLoopUpdate;
    [SerializeField] bool turnRayInOppositeDirection;
    [SerializeField] float timeOnWaitPlayer;
    float currentAngle = 0;
    float currentTimeInLoop;
    bool isPlayerDetecterRayAngleIncreases;
    float parameterOfTurningRayAlongXAxis = -1;

    bool canPatrolmanGetNewPoint;
    bool goToPoint;
    bool goToPlayer;
    int countStopWaintingForPlayer;
    Vector2 directionPlayerDetecterRay;

    EnemiesMovement myMovementScript;

    private void Start()
    {
        myMovementScript = GetComponent<EnemiesMovement>();
        currentAngle = -maxDeflectionAngle;
        isPlayerDetecterRayAngleIncreases = true;
        currentTimeInLoop = 0;
        timeOnRayLoopUpdate /= 2f;
    }
    
    private void Update()
    {
        CheckIsPointFree();
        CheckReachingPoint();
        CheckReachingLastPlayerPosition();
        UpdatePlayerDetecterRayAngle();
        CheckPlayerDetected();
    }

    private void CheckReachingLastPlayerPosition()
    {
        if(!goToPlayer) { return; }
        if (currentPatrolPoint != null)
        {
            currentPatrolPoint.StopPursuing();
            currentPatrolPoint = null;
        }
        countStopWaintingForPlayer = 0;
        goToPoint = false;

        Vector2 cheakerPlayerCircleCoordinates = transform.position;
        Vector2 lastPlayerPos = myMovementScript.GetCurrentTragetPos();

        if (Mathf.Abs(lastPlayerPos.x - cheakerPlayerCircleCoordinates.x) 
            <= Mathf.Abs(radiusOfPointReachingZone ) && goToPlayer)
        {
            goToPlayer = false;
            StartCoroutine(WaitingForPlayer());
        }

    }

    IEnumerator WaitingForPlayer()
    {
        goToPlayer = false;
        yield return new WaitForSeconds(timeOnWaitPlayer);
        if (!goToPlayer)
        {
            countStopWaintingForPlayer++;
            if (countStopWaintingForPlayer <= 1)
            {
                Debug.Log("Гоблин свободен псоле патруля игрока");
                canPatrolmanGetNewPoint = true;
            }
        }

    }

    private void CheckReachingPoint()
    {
        if (goToPlayer) { return; }
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
        goToPlayer = true;
        myMovementScript.SetTarget(playersPos.position);
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
        if (!goToPlayer)
        {
            Debug.Log("Гоблин постоял на точке");
            canPatrolmanGetNewPoint = true;
        }
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
            Debug.Log($"Начнаю преследовать точку {currentPatrolPoint.gameObject.name}");
            currentPatrolPoint.StartedPursuing();
            goToPoint = true;
            myMovementScript.SetTarget(currentPatrolPoint.transform.position);
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
        RaycastHit2D hit = Physics2D.Raycast(detectorPoint.position, directionPlayerDetecterRay, sizeOfPlayerDetecterRay, playerAndCollidingEnvironmentLayers);
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
        Gizmos.DrawRay(detectorPoint.position, new Vector2(Mathf.Cos(currentAngle / 90f * Mathf.PI) * sizeOfPlayerDetecterRay * parameterOfTurningRayAlongXAxis, 
            Mathf.Sin(currentAngle / 90f * Mathf.PI) * sizeOfPlayerDetecterRay));
    }
}
