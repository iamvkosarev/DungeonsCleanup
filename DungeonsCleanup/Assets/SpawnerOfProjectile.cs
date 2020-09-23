﻿using System.Collections;
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

    public void SpawnProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnProjectilePoint.position, Quaternion.identity);
        if (setProjectileSpeed)
        {
            projectile.GetComponent<ProjectileMovement>().SetSpeed(projectileSpeed);
        }
        if (setProjectileDamage)
        {
            projectile.GetComponent<DamageDealer>().SetDamage(damage);
        }
    }
}