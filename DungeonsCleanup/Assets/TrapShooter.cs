using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapShooter : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    private void Shoot()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
    }
}
