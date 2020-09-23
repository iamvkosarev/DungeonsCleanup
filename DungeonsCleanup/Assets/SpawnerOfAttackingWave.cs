using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOfAttackingWave : MonoBehaviour
{
    [SerializeField] GameObject attackWavePrefab;
    [SerializeField] Transform pointOfSpwan;
    [SerializeField] bool setDamage;
    [SerializeField] int damage;

    public void SpawnAttack()
    {
        GameObject attackWave = Instantiate(attackWavePrefab, pointOfSpwan.position, Quaternion.identity);
        if (transform.localScale.x < 0)
        {
            attackWave.transform.localScale = 
                new Vector2(attackWave.transform.localScale.x * -1, attackWave.transform.localScale.y);
        }
        if (setDamage)
        {
            attackWave.GetComponent<DamageDealer>().SetDamage(damage);
        }
    }

}
