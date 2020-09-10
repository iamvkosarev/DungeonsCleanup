﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] PlayerProperties playerProperties;
    [SerializeField] SpriteRenderer stabbingWeaponSpriteRender;
    [SerializeField] LayerMask enemysLayer;
    [SerializeField] int numOfStabbingAttacks = 1;

    StabbingWeapon currentStabbingWeapon;
    int currentStabbingAttackNum;
    bool didAttackAnimationStart = false;
    Animator myAnimator;
    // ProjjectileWeapon currentProjjectileWeapon;
    // title
    // level
    // HP
    // MP
    // strength
    // agility
    // sense
    // vitality
    // intelligence

    private void Start()
    {
        currentStabbingWeapon = playerProperties.GetCurrentStabbingWeapons();
        myAnimator = GetComponent<Animator>();
    }

    public void StartStabbingAttackAnimation()
    {
        if (didAttackAnimationStart) { return; }
        else { didAttackAnimationStart = true;}
        currentStabbingAttackNum = Random.Range(0, numOfStabbingAttacks - 1);
        // Animation
        myAnimator.SetBool("isAttacking", true);
        myAnimator.SetTrigger($"Stabbing_{currentStabbingAttackNum}");
    }
    public void StopAttackAnimation()
    {
        didAttackAnimationStart = false;
        myAnimator.SetBool("isAttacking", false);
        SetDefaultStabbingSprite();
    }

    public void RefreshStabbingAttackFrame(int numOfFrame)
    {
        stabbingWeaponSpriteRender.sprite = currentStabbingWeapon.GetAnimationSprtie(currentStabbingAttackNum, numOfFrame);
    }
    
    public void SetDefaultStabbingSprite()
    {
        stabbingWeaponSpriteRender.sprite = null;
    }

    public void DetectEnemysAndAttack()
    {
        float playerDirection = Mathf.Sign(transform.localScale.x);
        float attackRadius = currentStabbingWeapon.GetAttackRadius(currentStabbingAttackNum);
        Vector2 attackZonePos = new Vector2(transform.position.x + playerDirection * attackRadius, transform.position.y);
<<<<<<< HEAD
        
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackZonePos, attackRadius, enemiesLayer);
        foreach (Collider2D enemy in enemies)
        {
            enemy.gameObject.GetComponent<Health>().TakeAwayHelath(playerDamage);
        }
=======
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackZonePos, attackRadius, enemysLayer);
>>>>>>> parent of 80da8a7... Add Enemy death + some changes
        Debug.Log($"Атаковано {enemies.Length} врагов");
    }
}
