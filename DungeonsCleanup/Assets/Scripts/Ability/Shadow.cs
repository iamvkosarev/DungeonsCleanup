using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private EnemiesMovement enemiesMovement;
    public PatrolPoint playerPatrolPoint;
    private Patrolman patrolman;

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

    public void SetPlayer(PatrolPoint playerPatrolPoint)
    {
        this.playerPatrolPoint = playerPatrolPoint;
        enemiesMovement = GetComponent<EnemiesMovement>();
        patrolman.SetPatrolPoint(playerPatrolPoint, 0);
    }
    
}
