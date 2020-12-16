using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class EnemieSpawnerEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(EnemieSpawner enemieSpawner, GizmoType gizmoType)
    {
        if ((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.white;
        }
        else
        {
            Gizmos.color = Color.white * 0.7f;
        }
        Gizmos.DrawSphere(enemieSpawner.transform.position, .5f);
    }
}
