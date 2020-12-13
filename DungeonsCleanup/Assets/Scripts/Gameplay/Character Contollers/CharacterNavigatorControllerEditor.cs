using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class CharacterNavigatorControllerEditor
{
    [DrawGizmo(GizmoType.Selected)]
    public static void OnDrawSceneGizmo(CharacterNavigatorController waypointNavigator, GizmoType gizmoType)
    {
        Gizmos.color = Color.cyan;
        if (waypointNavigator.destination != null)
        {
            Gizmos.DrawSphere(waypointNavigator.destination, .3f);
            Gizmos.DrawLine(waypointNavigator.transform.position, waypointNavigator.destination);
        }
    }
}
