using System;
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
    [SerializeField] private bool useLowerParametr = false;
    [SerializeField] private float attackLowerParametr = 1f;

    [Header("SFX")]
    [SerializeField] private AudioClip attackSFX;
    [SerializeField] private float audioBoost;
    AudioSource myAudioSource;
    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    public void SpawnAttack()
    {
        GameObject attackWave = Instantiate(attackWavePrefab, pointOfSpwan.position, Quaternion.identity);
        SpawnAttackSFX();
        float changeDirectionParam = 1;
        if (changeDirectionOfWaveInOpposite)
        {
            changeDirectionParam = -1;
        }
        attackWave.transform.localScale = 
                new Vector2(attackWave.transform.localScale.x * changeDirectionParam * Mathf.Sign(transform.rotation.y), attackWave.transform.localScale.y);
        DamageDealer damageDealer = attackWave.GetComponent<DamageDealer>();
        if (setDamage)
        {
            damageDealer.SetDamage(damage);
            
        }
        if (useLowerParametr)
        {
            damageDealer.SetDamage((int)((float)damageDealer.GetDamge()*attackLowerParametr));
        }
    }

    private void SpawnAttackSFX()
    {
        if (attackSFX)
        {
            myAudioSource.PlayOneShot(attackSFX, audioBoost);
        }
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

}
