using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointRoot : MonoBehaviour
{
    List<Transform> waypointsTransform = new List<Transform>();
    int countWaypoints;

    [System.Obsolete]
    private void Awake()
    {
        foreach(Transform childTransform in transform)
        {
            waypointsTransform.Add(childTransform);
        }
        countWaypoints = transform.GetChildCount();
    }

    public Transform GetClosestWaypoint(Vector3 vector2)
    {
        float minDistance = float.MaxValue;
        Transform needPoint = transform;
        for(int index = 0; index < countWaypoints; index++)
        {
            float distanceBetweenPoints = (waypointsTransform[index].position - vector2).magnitude;
            if (distanceBetweenPoints < minDistance)
            {
                minDistance = distanceBetweenPoints;
                needPoint = waypointsTransform[index];
            }
        }
        if(needPoint == transform)
        {
            return null;
        }
        return needPoint;
    }
}
