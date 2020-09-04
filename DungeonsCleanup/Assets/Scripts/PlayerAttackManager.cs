using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] PlayerProperties playerProperties;
    [SerializeField] SpriteRenderer stabbingWeaponSpriteRender;
    [SerializeField] int numOfStabbingAttacks = 1;

    StabbingWeapon currentStabbingWeapon;
    int currentStabbingAttackNum;
    bool isAttacking = false;
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

    public void StartStabbingAttack()
    {
        if (isAttacking) { return; }
        else { isAttacking = true;}
        currentStabbingAttackNum = Random.Range(0, numOfStabbingAttacks - 1);
        // Animation
        myAnimator.SetBool("isAttacking", true);
        myAnimator.SetTrigger($"Stabbing_{currentStabbingAttackNum}");
    }
    public void StopAttacking()
    {
        isAttacking = false;
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

}
