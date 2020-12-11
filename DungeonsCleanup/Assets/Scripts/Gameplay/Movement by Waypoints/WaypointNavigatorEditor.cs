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
        if ((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.red;
        }
        Gizmos.DrawSphere(waypointNavigator.currentWaypoint.transform.position, .3f);
    }
}
