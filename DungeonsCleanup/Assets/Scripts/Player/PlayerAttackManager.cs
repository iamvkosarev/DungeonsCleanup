using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] LayerMask enemiesLayer;
    [SerializeField] int numOfAttackAnimations = 1;
    [SerializeField] int damage = 100;
    SpawnerOfAttackingWave mySpawnerOfAttackingWave;
    int currentAttackNum;
    bool isAnimationContinue;
    PlayerMovement playerMovement;
    Animator myAnimator;

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
            if (isAnimationContinue) { return; }
            else
            {
                isAnimationContinue = true;
                currentAttackNum = Random.Range(0, numOfAttackAnimations);
                myAnimator.SetTrigger($"Stabbing_{currentAttackNum}");
            }
        }
    }


    public void AttackEnemyByWave()
    {
        mySpawnerOfAttackingWave.SetDamage(damage);
        mySpawnerOfAttackingWave.SpawnAttack();
    }
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }
    public void AttackHasEnded()
    {
        isAnimationContinue = false;
    }
}
