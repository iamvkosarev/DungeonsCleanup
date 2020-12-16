using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class WaypointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
    {
        if((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.yellow;
        }
        else
        {
            Gizmos.color = Color.yellow * 0.6f;
        }
        Gizmos.DrawSphere(waypoint.transform.position, .3f);
        
        if(waypoint.previousWaypoint != null)
        {
            if ((gizmoType & GizmoType.Selected) != 0)
            {
                Gizmos.color = Color.red;
            }
            else
            {
                Gizmos.color = Color.red * 0.6f;
            }
            Vector2 currentPos = waypoint.transform.position;
            Vector2 previousPos = waypoint.previousWaypoint.transform.position;
            float lengthBetweenPoints = Mathf.Sqrt(
                Mathf.Pow(currentPos.x - previousPos.x, 2)
                + Mathf.Pow(currentPos.y - previousPos.y, 2));
            Vector2 normalVector = (previousPos - currentPos) / lengthBetweenPoints;
            Gizmos.DrawLine(currentPos, currentPos + normalVector * lengthBetweenPoints / 2f);
        }
        if(waypoint.nextWaypoint != null)
        {
            if ((gizmoType & GizmoType.Selected) != 0)
            {
                Gizmos.color = Color.green;
            }
            else
            {
                Gizmos.color = Color.green * 0.6f;
            }
            Vector2 currentPos = waypoint.transform.position;
            Vector2 nextPoint = waypoint.nextWaypoint.transform.position;
            float lengthBetweenPoints = Mathf.Sqrt(
                Mathf.Pow(currentPos.x - nextPoint.x, 2)
                + Mathf.Pow(currentPos.y - nextPoint.y, 2));
            Vector2 normalVector = (nextPoint - currentPos) / lengthBetweenPoints;
            Gizmos.DrawLine(currentPos, currentPos + normalVector * lengthBetweenPoints / 2f);
        }
    }
}
