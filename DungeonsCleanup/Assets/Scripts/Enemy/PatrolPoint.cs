using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    [SerializeField] bool stopOnPoint;
    [SerializeField] float timeOnStandHere;
    private bool isPointFree = true;
    public void StartedPursuing()
    {
        isPointFree = false;
    }
    public void StopPursuing()
    {
        isPointFree = true;
    }
    public bool IsPointFree()
    {
        return isPointFree;
    }
    public float GetTimeOnStand()
    {
        if (!stopOnPoint) { return 0f; }
        return timeOnStandHere;
    }
}
