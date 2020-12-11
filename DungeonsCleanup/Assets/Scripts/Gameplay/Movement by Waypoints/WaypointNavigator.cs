using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    CharacterNavigatorController controller;
    public Waypoint currentWaypoint;

    private void Awake()
    {
        controller = GetComponent<CharacterNavigatorController>();
    }

    private void Start()
    {
        controller.SetDestination(currentWaypoint.GetPosition(), MovementType.Walk);
    }

    private void Update()
    {
        if (controller.reachedDestination)
        {
            currentWaypoint = currentWaypoint.nextWaypoint;
            controller.SetDestination(currentWaypoint.GetPosition(), MovementType.Walk);
        }
    }
}
