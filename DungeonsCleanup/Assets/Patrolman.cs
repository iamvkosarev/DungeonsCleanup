using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolman : MonoBehaviour
{
    [SerializeField] float sizeZoneOfPointReaching;
    [SerializeField] LayerMask patrolPointsLayer;
    [SerializeField] Transform patrolCheckerPoint;
    [Header("Movement")]
    [SerializeField] float speed;
    PatrolPoint currentPatrolPoint;
    int currentPatrolPointNum;
    bool isPatrolmanFree;
    bool goToPoint;
    bool doWeKnowDirection;
    Rigidbody2D myRigidBode2D;
    EnemysMovement myMovementScript;
    float signXAxisDirection;

    private void Start()
    {
        myRigidBode2D = GetComponent<Rigidbody2D>();
        myMovementScript = GetComponent<EnemysMovement>();
    }
    public void SetPatrolPoint(PatrolPoint patrolPoint, int patrolPointNum)
    {
        this.currentPatrolPoint = patrolPoint;
        this.currentPatrolPointNum = patrolPointNum;
        isPatrolmanFree = false;
    }
    public int GetCurrentPatrolPointNum()
    {
        return currentPatrolPointNum;
    }
    private void Update()
    {
        CheckIsPointFree();
        CheckReachingPoint();
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
            doWeKnowDirection = false;
        }
    }

    public bool ShoulIGoToPatrolPoint()
    {
        return goToPoint;
    }

    public bool IsPatrolmanFree()
    {
        return isPatrolmanFree;
    }

    private void CheckReachingPoint()
    {
        if (currentPatrolPoint == null) { return; }
        Collider2D pointsCollider = (Collider2D)Physics2D.OverlapCircle(patrolCheckerPoint.position, sizeZoneOfPointReaching, patrolPointsLayer);
        if (pointsCollider == null) { return; }
        if (pointsCollider.gameObject == currentPatrolPoint.gameObject)
        {
            isPatrolmanFree = true;
            goToPoint = false;
            currentPatrolPoint.StopPursuing();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(patrolCheckerPoint.position, sizeZoneOfPointReaching);
    }
}
