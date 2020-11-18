using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform[] spawnProjectilesPoints;
    [Range(0f,360f)] [SerializeField] private float angle = 0f;
    private void Shoot()
    {
        foreach (Transform spawnProjectilesPoint in spawnProjectilesPoints)
        {

            GameObject projectile = Instantiate(projectilePrefab, spawnProjectilesPoint.transform.position, Quaternion.identity) as GameObject;
            TrapArrowMovement trapArrowMovement = projectile.GetComponent<TrapArrowMovement>();
            trapArrowMovement.SetDirectionByAngle(angle);
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(spawnProjectilesPoints[0].position, new Vector2(Mathf.Cos(angle * Mathf.PI / 180f), Mathf.Sin(angle * Mathf.PI / 180f)));
    }
}
