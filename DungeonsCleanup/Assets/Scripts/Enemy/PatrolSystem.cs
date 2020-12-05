using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSystem : MonoBehaviour
{
    [SerializeField] PatrolPoint[] patrolPoints;
    [SerializeField] Patrolman[] patrolmen;
    int numOfPoints;

    private void Start()
    {
        foreach(Patrolman patrolman in patrolmen)
        {
            patrolman.OnPatrolFreeEvent += SetPoint;
        }
        numOfPoints = patrolPoints.Length;
        for (int patrolmanNum = 0; patrolmanNum < patrolmen.Length; patrolmanNum++)
        {
            int yourPointNum;
            if (patrolmanNum >= patrolPoints.Length)
            {
                yourPointNum = numOfPoints - 1;
            }
            else
            {
                yourPointNum = patrolmanNum;
            }
            patrolmen[patrolmanNum].SetPatrolPoint(patrolPoints[yourPointNum], yourPointNum);
        }
    }
    private void SetPoint(object sender, Patrolman.OnPatrolFreeEventArgs e)
    {
        Patrolman patrolman = e.patrolman;
        int lastPatrolPointNum = patrolman.GetCurrentPatrolPointNum();
        int newPatrolPointNum;
        if (lastPatrolPointNum + 1 >= numOfPoints)
        {
            newPatrolPointNum = 0;
        }
        else
        {
            newPatrolPointNum = lastPatrolPointNum + 1;
        }
        patrolman.SetPatrolPoint(patrolPoints[newPatrolPointNum], newPatrolPointNum);

    }
}
