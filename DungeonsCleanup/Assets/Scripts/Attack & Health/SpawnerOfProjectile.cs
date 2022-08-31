using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOfProjectile : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform spawnProjectilePoint;
    [SerializeField] bool setProjectileDamage;
    [SerializeField] int damage;
    [SerializeField] bool changeDirectionOfWaveInOpposite;

    CharacterAttackChecker characterAttackChecker;

    private void Start() 
    {

        characterAttackChecker = gameObject.GetComponent<CharacterAttackChecker>();
    }


    private void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnProjectilePoint.position, Quaternion.identity);
        float changeDirectionParam = 1;
        Vector2 singleVectorDirectionPlayerDetecterRay = (characterAttackChecker.firstDetectedCharacterPos - transform.position).normalized;
        if (changeDirectionOfWaveInOpposite)
        {
            changeDirectionParam = -1;
        }
        projectile.transform.localScale =
                new Vector2(projectile.transform.localScale.x * changeDirectionParam * Mathf.Sign(transform.rotation.y), projectile.transform.localScale.y);
        projectile.GetComponent<ProjectileMovement>().SetVelocityDirection(singleVectorDirectionPlayerDetecterRay);
        if (setProjectileDamage)
        {
            projectile.GetComponent<DamageDealer>().SetDamage(damage);
        }
    }
}
