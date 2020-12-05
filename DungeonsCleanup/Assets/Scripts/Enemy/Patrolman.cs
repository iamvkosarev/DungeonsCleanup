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
    [SerializeField] Color patrolPointCheackerColor;
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

    bool waitingOnPoint;
    Vector2 directionPlayerDetecterRay;
    public EventHandler<OnPatrolFreeEventArgs> OnPatrolFreeEvent;
    public class OnPatrolFreeEventArgs : EventArgs
    {
        public Patrolman patrolman;
    }
    EnemiesMovement myMovementScript;

    enum Target
    {
        nothing,
        point,
        enemie
    }
    Target currentTarget =  Target.nothing;

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
        if(currentTarget != Target.enemie) { return; }
        if (currentPatrolPoint != null)
        {
            currentPatrolPoint.StopPursuing();
            currentPatrolPoint = null;
        }

        Vector2 cheakerPlayerCircleCoordinates = transform.position;
        Vector2 lastPlayerPos = myMovementScript.GetCurrentTragetPos();

        if ((Mathf.Abs(lastPlayerPos.x - cheakerPlayerCircleCoordinates.x) 
            <= Mathf.Abs(radiusOfPointReachingZone ) || myMovementScript.IsTouchingInvisibleWall()))
        {
            StartCoroutine(WaitingForPlayer());
        }

    }

    IEnumerator WaitingForPlayer()
    {
        currentTarget = Target.nothing;
        yield return new WaitForSeconds(timeOnWaitPlayer);
        if (currentTarget != Target.enemie)
        {
            if(OnPatrolFreeEvent!= null)
            {
                OnPatrolFreeEvent.Invoke(this, new OnPatrolFreeEventArgs { patrolman = this });
            }
        }
    }

    private void CheckReachingPoint()
    {
        if (currentTarget == Target.enemie || currentPatrolPoint == null) { return; }
        Collider2D pointsCollider = (Collider2D)Physics2D.OverlapCircle(patrolPointCheackerCoordinates.position,
            radiusOfPointReachingZone, patrolPointsLayer);
        if (pointsCollider == null) { return; }
        if (pointsCollider.gameObject == currentPatrolPoint.gameObject)
        {
            Debug.Log($"{this.gameObject.name} достиг точку {currentPatrolPoint}");
            StartCoroutine(WaitingOnPoint());

        }
    }
    public void StartPursuingPlayer(Transform playersPos)
    {
        currentTarget =  Target.enemie;
        myMovementScript.SetTarget(playersPos.position);
    }
    IEnumerator WaitingOnPoint()
    {
        PatrolPoint myLastPatrolPoint = currentPatrolPoint;
        currentPatrolPoint = null;
        waitingOnPoint = true;
        if (currentTarget != Target.enemie) { currentTarget = Target.nothing; }           
        yield return new WaitForSeconds(myLastPatrolPoint.GetTimeOnStand());
        Debug.Log($"{this.gameObject.name} отстоял на {myLastPatrolPoint}");
        waitingOnPoint = false;
        myLastPatrolPoint.StopPursuing();
        if (currentTarget != Target.enemie)
        {
            if (OnPatrolFreeEvent != null)
            {
                OnPatrolFreeEvent.Invoke(this, new OnPatrolFreeEventArgs { patrolman = this });
            }

            Debug.Log($"{this.gameObject.name} приянл новую точку {currentPatrolPoint}");
        }
    }
    public void SetPatrolPoint(PatrolPoint patrolPoint, int patrolPointNum)
    {
        
        if (currentTarget != Target.enemie)
        {
            this.currentPatrolPoint = patrolPoint;
            this.currentPatrolPointNum = patrolPointNum;
        }
    }
    private void CheckIsPointFree()
    {
        if (currentTarget == Target.point || currentPatrolPoint == null) { return; }
        bool isPointFree = currentPatrolPoint.IsPointFree();
        if (isPointFree)
        {
            currentPatrolPoint.StartedPursuing();
            if (currentTarget != Target.enemie)
            {
                currentTarget = Target.point;
            }
            myMovementScript.SetTarget(currentPatrolPoint.transform.position);
        }
    }
    public int GetCurrentPatrolPointNum()
    {
        return currentPatrolPointNum;
    }
    public bool ShoulIGoToPatrolPoint()
    {
        return currentTarget == Target.point;
    }
    public bool ShoulIGoToPlayer()
    {
        return currentTarget == Target.enemie;
    }
    public bool CanPatrolmanGetNewPoint()
    {
        return currentTarget == Target.nothing;
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
        parameterOfTurningRayAlongXAxis = (transform.rotation.y < 0 ? -1 : 1) * (turnRayInOppositeDirection ? 1 : -1);
        directionPlayerDetecterRay = new Vector2(Mathf.Cos(currentAngle / 90f * Mathf.PI) * sizeOfPlayerDetecterRay * parameterOfTurningRayAlongXAxis,
            Mathf.Sin(currentAngle / 90f * Mathf.PI) * sizeOfPlayerDetecterRay);

        RaycastHit2D hit = Physics2D.Raycast(detectorPoint.position, directionPlayerDetecterRay, sizeOfPlayerDetecterRay, playerAndCollidingEnvironmentLayers);
        if (!hit) { return; }
        if (hit.collider.gameObject.layer == playerLayerNum && !hit.collider.gameObject.GetComponent<BatPathing>())
        {
            StartPursuingPlayer(hit.collider.gameObject.transform);
        }
    }

    private void OnDestroy()
    {
        if(currentPatrolPoint == null) { return; }
        currentPatrolPoint.StopPursuing();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = patrolPointCheackerColor;
        Gizmos.DrawSphere(patrolPointCheackerCoordinates.position, radiusOfPointReachingZone);

        Gizmos.color = Color.gray;
        Gizmos.DrawRay(detectorPoint.position, new Vector2(Mathf.Cos(currentAngle / 90f * Mathf.PI) * sizeOfPlayerDetecterRay * parameterOfTurningRayAlongXAxis, 
            Mathf.Sin(currentAngle / 90f * Mathf.PI) * sizeOfPlayerDetecterRay));
    }
    public bool IsWaitingOnPoint()
    {
        return waitingOnPoint;
    }

    bool wasHadTurned;
    public void TurnRay()
    {
        wasHadTurned = true;
        detectorPoint.localPosition= new Vector2(-detectorPoint.localPosition.x, detectorPoint.localPosition.y);
        turnRayInOppositeDirection = !turnRayInOppositeDirection;
    }
    public void TurnRayBack()
    {
        if (wasHadTurned)
        {
            wasHadTurned = false;
            detectorPoint.localPosition = new Vector2(-detectorPoint.localPosition.x, detectorPoint.localPosition.y);
            turnRayInOppositeDirection = !turnRayInOppositeDirection;
        }
    }
}
