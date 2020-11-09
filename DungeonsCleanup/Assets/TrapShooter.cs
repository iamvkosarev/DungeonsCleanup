using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform spawnProjectilesPoint;
    [Range(0f,360f)] [SerializeField] private float angle = 0f;
    private void Shoot()
    {
        GameObject projectile =  Instantiate(projectilePrefab, spawnProjectilesPoint.transform.position, Quaternion.identity) as GameObject;
        TrapArrowMovement trapArrowMovement = projectile.GetComponent<TrapArrowMovement>();
        trapArrowMovement.SetDirectionByAngle(angle);

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(spawnProjectilesPoint.position, new Vector2(Mathf.Cos(angle * Mathf.PI / 180f), Mathf.Sin(angle * Mathf.PI / 180f)));
    }
}
