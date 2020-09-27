using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOfProjectile : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform spawnProjectilePoint;
    [SerializeField] bool setProjectileSpeed;
    [SerializeField] float projectileSpeed;
    [SerializeField] bool setProjectileDamage;
    [SerializeField] int damage;
    [SerializeField] bool changeDirectionOfWaveInOpposite;

    DetectorEnemiesInAttackZone detector;

    private void Start() 
    {
        
        detector = gameObject.GetComponent<DetectorEnemiesInAttackZone>();
    }


    private void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnProjectilePoint.position, Quaternion.identity);
        float changeDirectionParam = 1;
        if (changeDirectionOfWaveInOpposite)
        {
            changeDirectionParam = -1;
        }
        projectile.transform.localScale =
                new Vector2(projectile.transform.localScale.x * changeDirectionParam * Mathf.Sign(transform.rotation.y), projectile.transform.localScale.y);
        if (setProjectileSpeed)
        {
            projectile.GetComponent<ProjectileMovement>().SetSpeed(projectileSpeed * changeDirectionParam * Mathf.Sign(transform.rotation.y));
        }
        if (setProjectileDamage)
        {
            projectile.GetComponent<DamageDealer>().SetDamage(damage);
        }
    }
}
