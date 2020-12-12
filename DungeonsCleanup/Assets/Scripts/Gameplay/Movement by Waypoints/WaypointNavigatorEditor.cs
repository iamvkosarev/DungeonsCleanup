using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class WaypointNavigatorEditor
{
    [DrawGizmo(GizmoType.Selected)]
    public static void OnDrawSceneGizmo(WaypointNavigator waypointNavigator, GizmoType gizmoType)
    {
        Gizmos.color = Color.cyan;
        if (waypointNavigator.currentWaypoint != null)
        {
            Gizmos.DrawSphere(waypointNavigator.currentWaypoint.transform.position, .3f);
            Gizmos.DrawLine(waypointNavigator.transform.position, waypointNavigator.currentWaypoint.transform.position);
        }
    }
}
