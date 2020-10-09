using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerOfAttackingWave : MonoBehaviour
{
    [SerializeField] GameObject attackWavePrefab;
    [SerializeField] Transform pointOfSpwan; 
    [SerializeField] bool setDamage;
    [SerializeField] int damage;
    [SerializeField] bool changeDirectionOfWaveInOpposite;

    public void SpawnAttack()
    {
        GameObject attackWave = Instantiate(attackWavePrefab, pointOfSpwan.position, Quaternion.identity);
        float changeDirectionParam = 1;
        if (changeDirectionOfWaveInOpposite)
        {
            changeDirectionParam = -1;
        }
        attackWave.transform.localScale = 
                new Vector2(attackWave.transform.localScale.x * changeDirectionParam * Mathf.Sign(transform.rotation.y), attackWave.transform.localScale.y);
        if (setDamage)
        {
            attackWave.GetComponent<DamageDealer>().SetDamage(damage);
        }
    }
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

}
