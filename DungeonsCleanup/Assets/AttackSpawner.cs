using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpawner : MonoBehaviour
{
    [SerializeField] GameObject attackWavePrefab;
    [SerializeField] Transform pointOfSpwan;

    public void SpawnAttack()
    {
        GameObject attackWave = Instantiate(attackWavePrefab, pointOfSpwan.position, Quaternion.identity);
        if (transform.localScale.x < 0)
        {
            attackWave.transform.localScale = 
                new Vector2(attackWave.transform.localScale.x * -1, attackWave.transform.localScale.y);
        }
    }
}
