using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [SerializeField] public WaypointRoot waypointRoot;
    private CharacterNavigatorController navigatorController;
    public Waypoint currentWaypoint;
    public bool canFindNewPoint = true;
    private void Awake()
    {
        navigatorController = GetComponent<CharacterNavigatorController>();
    }

    private void Start()
    {
        if (currentWaypoint != null)
        {
            navigatorController.SetDestination(currentWaypoint.GetPosition(), MovementType.Walk);
        }
        else
        {
            if (canFindNewPoint)
            {
                if (waypointRoot)
                {
                    currentWaypoint = waypointRoot.GetClosestWaypoint(transform.position).GetComponent<Waypoint>();
                    navigatorController.SetDestination(currentWaypoint.GetPosition(), MovementType.Walk);
                }
                else
                {
                    Debug.LogError("Root component must be selected. Please assign a root component.");
                }
            }
        }
    }

    public void StopChasingWaypoints()
    {
        canFindNewPoint = false;
        currentWaypoint = null;
    }

    public void StartChasingWaypoints()
    {
        canFindNewPoint = true;
    }

    private void Update()
    {
        if(currentWaypoint != null)
        {
            if (navigatorController.reachedDestination)
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
                navigatorController.SetDestination(currentWaypoint.GetPosition(), MovementType.Walk);
            }
        }
        else
        {
            if (canFindNewPoint)
            {
                if (waypointRoot)
                {
                    currentWaypoint = waypointRoot.GetClosestWaypoint(transform.position).GetComponent<Waypoint>();
                    navigatorController.SetDestination(currentWaypoint.GetPosition(), MovementType.Walk);
                }
                else
                {
                    Debug.LogError("Root component must be selected. Please assign a root component.");
                }
            }
        }
    }
}
