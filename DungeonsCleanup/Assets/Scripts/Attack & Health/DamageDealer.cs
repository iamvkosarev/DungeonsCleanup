using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] Transform damageSpawnPoint;
    [SerializeField] Vector2 damageZone;
    [SerializeField] int damage;
    [SerializeField] LayerMask whomCanAttacked;
    [SerializeField] bool chechWorkWitchAnyThing = true;
    [SerializeField] bool dontWorkAfterTouch = true;
    [SerializeField] bool destoryAfterTounch = false;
    [SerializeField] AudioClip attackSFX;
    [SerializeField] float audioBoost = 1f;
    private bool canAttack = true;
    private AudioSource myAudioSource;

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
    public int GetDamge()
    {
        return damage;
    }
    private void Update()
    {
        if (canAttack)
        {
            CheckAttackZone();
        }
    }

    private void CheckAttackZone()
    {
        Collider2D touchedCollider= Physics2D.OverlapBox(damageSpawnPoint.position, damageZone, 0, whomCanAttacked);
        GameObject touchedObject;
        if (touchedCollider != null)
        {
            touchedObject = touchedCollider.gameObject;
        }
        else { return; }
        Health attackedObjectsHealthScripts = touchedObject.GetComponent<Health>();
        HealthUI attackedObjectsPlayerHealthScripts = touchedObject.GetComponent<HealthUI>();
        if (attackedObjectsHealthScripts != null)
        {
            attackedObjectsHealthScripts.TakeAwayHelath(damage);
            CheckWorkProperties();
        }
        else if (attackedObjectsPlayerHealthScripts != null)
        {
            attackedObjectsPlayerHealthScripts.TakeAwayHelath(damage);
            CheckWorkProperties();

        }
        SpawnGivenDamageSFX();
        if (chechWorkWitchAnyThing)
        {
            CheckWorkProperties();
        }
    }

    private void SpawnGivenDamageSFX()
    {
        if (!myAudioSource)
        {
            myAudioSource = GetComponent<AudioSource>();
        }
        if (!myAudioSource || !attackSFX) { return; }
        myAudioSource.PlayOneShot(attackSFX, audioBoost);
    }

    private void CheckWorkProperties()
    {
        if (dontWorkAfterTouch)
        {
            canAttack = false;
        }
        if (destoryAfterTounch)
        {
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(damageSpawnPoint.position, damageZone);
    }
    }
