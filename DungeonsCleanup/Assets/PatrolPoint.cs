using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
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
}
