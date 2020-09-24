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
            Debug.Log($"Здравствуй, {patrolmen[patrolmanNum].gameObject.name}. Номер твоей точки: {yourPointNum}");
        }
    }
    private void Update()
    {
        CheckEmployment();
    }

    private void CheckEmployment()
    {
        foreach (Patrolman patrolman in patrolmen)
        {
            if (patrolman.IsPatrolmanFree())
            {
                int lastPatrolPointNum = patrolman.GetCurrentPatrolPointNum();
                int newPatrolPointNum;
                if(lastPatrolPointNum+1>= numOfPoints)
                {
                    newPatrolPointNum = 0;
                }
                else
                {
                    newPatrolPointNum = lastPatrolPointNum + 1;
                }
                patrolman.SetPatrolPoint(patrolPoints[newPatrolPointNum], newPatrolPointNum);

                Debug.Log($"Молодец, {patrolman.gameObject.name}. Иди к следующей точки: {newPatrolPointNum}");
            }
        }
    }
}
