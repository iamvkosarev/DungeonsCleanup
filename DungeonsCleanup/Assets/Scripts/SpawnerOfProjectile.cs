using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOfProjectile : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform spawnProjectilePoint;
    [SerializeField] float shootingDelay = 1f;
    // [SerializeField] bool setProjectileSpeed;
    // [SerializeField] float projectileSpeed;
    // [SerializeField] bool setProjectileDamage;
    // [SerializeField] int damage;

    PlayerDetector detector;
    Animator myAnimator;

    private void Awake() 
    {
        myAnimator = GetComponent<Animator>();
        detector = gameObject.GetComponent<PlayerDetector>();
    }

    private void Update()
    {
        if(detector.GetResultOfAttacking())
        {
            myAnimator.SetBool("Attack", true);
        }
    }

    private void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnProjectilePoint.position, Quaternion.identity);

        myAnimator.SetBool("Attack", false);
        // if (setProjectileSpeed)
        // {
        //     projectile.GetComponent<ProjectileMovement>().SetSpeed(projectileSpeed);
        // }
        // if (setProjectileDamage)
        // {
        //     projectile.GetComponent<DamageDealer>().SetDamage(damage);
        // }
    }

    
}
