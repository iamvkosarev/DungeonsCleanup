using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [SerializeField] private float distanceToTeleport =  12f;
    private EnemiesMovement enemiesMovement;
    private PatrolPoint playerPatrolPoint;
    private Patrolman patrolman;
    private Transform playerTransform;

    private void Start()
    {
        patrolman = GetComponent<Patrolman>();
        if (playerPatrolPoint)
        {
            patrolman.SetPatrolPoint(playerPatrolPoint, 0);
        }
    }
    private void Update()
    {
        CheckFreeShadow();
        CheckPlayerPosToTeleport();
    }

    private void CheckPlayerPosToTeleport()
    {
        if(Mathf.Sqrt(Mathf.Pow(playerTransform.position.x - transform.position.x, 2) +
            Mathf.Pow(playerTransform.position.y - transform.position.y, 2)) >= distanceToTeleport)
        {
            transform.position = playerTransform.position;
        }
    }

    private void CheckFreeShadow()
    {
        if (patrolman.CanPatrolmanGetNewPoint())
        {
            if (playerPatrolPoint)
            {
                patrolman.SetPatrolPoint(playerPatrolPoint, 0);
            }
        }
    }

    public void SetPlayer(PatrolPoint playerPatrolPoint, Transform playerTransform)
    {
        if (!patrolman)
        {
            patrolman = GetComponent<Patrolman>();
            this.playerTransform = playerTransform;
        }
        this.playerPatrolPoint = playerPatrolPoint;
        enemiesMovement = GetComponent<EnemiesMovement>();
        patrolman.SetPatrolPoint(playerPatrolPoint, 0);
    }
    
}
