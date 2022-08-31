using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolSystem : MonoBehaviour
{
    [SerializeField] PatrolPoint[] patrolPoints;
    [SerializeField] Patrolman[] patrolmen;
    [SerializeField] Transform patrolZonePoint;
    [SerializeField] Vector2 patrolZoneSize;
    [SerializeField] Color patrolZoneColorCheck;
    [SerializeField] LayerMask playerZoneLayer;
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
    private void Update()
    {
        CheckPlayerZone();
    }

    private bool settedTrue = false;
    private bool settedFalse = false;
    private void CheckPlayerZone()
    {
        if (patrolZonePoint)
        {
            Collider2D playerChechZone = Physics2D.OverlapBox(patrolZonePoint.position, patrolZoneSize, 0, playerZoneLayer);
            if (playerChechZone != null)
            {
                if (!settedTrue)
                {
                    Debug.Log("Игрок в зоне!");
                    foreach (Patrolman patrolman in patrolmen)
                    {
                        if (patrolman)
                            patrolman.gameObject.SetActive(true);
                    }
                    settedTrue = true;
                    settedFalse = false;
                }
            }
            else
            {
                if (!settedFalse)
                {
                    Debug.Log("Игрок вне зоны!");
                    foreach (Patrolman patrolman in patrolmen)
                    {
                        if (patrolman)
                            patrolman.gameObject.SetActive(false);
                    }
                    settedTrue = false;
                    settedFalse = true;
                }
            }
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
    private void OnDrawGizmos()
    {
        if (patrolZoneColorCheck == null || patrolZonePoint == null || patrolZoneSize == null)
        {
            return;
        }
        Gizmos.color = patrolZoneColorCheck;
        Gizmos.DrawCube(patrolZonePoint.position, patrolZoneSize);
    }
}
