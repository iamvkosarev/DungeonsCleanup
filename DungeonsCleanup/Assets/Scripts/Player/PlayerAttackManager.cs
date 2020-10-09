using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer stabbingWeaponSpriteRender;
    [SerializeField] LayerMask enemiesLayer;
    [SerializeField] int numOfStabbingAttacks = 1;

    public StabbingWeapon currentStabbingWeapon;

    SpawnerOfAttackingWave mySpawnerOfAttackingWave;
    int currentStabbingAttackNum;
    bool didAnimationStart;
    PlayerMovement playerMovement;
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
        mySpawnerOfAttackingWave = GetComponent<SpawnerOfAttackingWave>();
        playerMovement = GetComponent<PlayerMovement>();
        myAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckAttackButtonPressed();
    }

    public void CheckAttackButtonPressed()
    {
        if (playerMovement.IsAttackButtonPressed())
        {
            if (didAnimationStart) { return; }
            else
            {
                didAnimationStart = true;
                currentStabbingAttackNum = Random.Range(0, numOfStabbingAttacks);
                myAnimator.SetTrigger($"Stabbing_{currentStabbingAttackNum}");
            }
        }
        else if (!playerMovement.IsAttackButtonPressed() && stabbingWeaponSpriteRender.sprite != null && myAnimator.GetBool($"isAttacking"))
        {
            SetDefaultStabbingSprite();
        }
    }

    public void RefreshStabbingAttackFrame(int numOfFrame)
    {
        stabbingWeaponSpriteRender.sprite = currentStabbingWeapon.GetAnimationSprtie(currentStabbingAttackNum, numOfFrame);
    }
    
    public void SetDefaultStabbingSprite()
    {
        stabbingWeaponSpriteRender.sprite = null;
        didAnimationStart = false;
    }

    public void AttackEnemyByWave()
    {
        mySpawnerOfAttackingWave.SetDamage(currentStabbingWeapon.GetDamage());
        mySpawnerOfAttackingWave.SpawnAttack();
        /*float playerDirection = Mathf.Sign(transform.localScale.x);
        float attackRadius = currentStabbingWeapon.GetAttackRadius(currentStabbingAttackNum);
        Vector2 attackZonePos = new Vector2(transform.position.x + playerDirection * attackRadius, transform.position.y);
        
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackZonePos, attackRadius, enemiesLayer);
        foreach (Collider2D enemy in enemies)
        {
            enemy.gameObject.GetComponent<Health>().TakeAwayHelath(currentStabbingWeapon.GetDamage());
        }
        Debug.Log($"Атаковано {enemies.Length} врагов");*/
    }

    public void SwitchCurrentStabbingWeapon(StabbingWeapon newStabbingWeapon)
    {
        Debug.Log("Оружее меняется");
        currentStabbingWeapon = newStabbingWeapon;
    }
    public StabbingWeapon GetCurrentStabbingWeapon()
    {
        return currentStabbingWeapon;
    }
}
