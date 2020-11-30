using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : MonoBehaviour
{
    [SerializeField] private LayerMask enemiesLayer;
    [SerializeField] private int numOfAttackAnimations = 1;
    [SerializeField] private int damage = 100;
    [SerializeField] private AudioClip attackWind;
    [SerializeField] private float audioBoostAttackWind;
    [SerializeField] private AudioClip[] attackPunch;
    [SerializeField] private float audioBoostPunch;

    private SpawnerOfAttackingWave mySpawnerOfAttackingWave;
    private int currentAttackNum;
    private bool isAnimationContinue;
    private PlayerMovement playerMovement;
    private AudioSource audioSource;
    private Animator myAnimator;
    private PlayerDevelopmentManager playerDevelopmentManager;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerDevelopmentManager = GetComponent<PlayerDevelopmentManager>();
        mySpawnerOfAttackingWave = GetComponent<SpawnerOfAttackingWave>();
        playerMovement = GetComponent<PlayerMovement>();
        myAnimator = GetComponent<Animator>();
        SetDamage(playerDevelopmentManager.GetDamageAccordingLvl());
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

    public void SpawnAttackWindSFX()
    {
        if(attackWind)
        audioSource.PlayOneShot(attackWind, audioBoostAttackWind);
    }

    public void SpawnPunchSFX()
    {
        int attackPunchSoundsCount = attackPunch.Length;
        if (attackPunchSoundsCount != 0)
        {
            audioSource.PlayOneShot(attackPunch[Random.Range(0, attackPunchSoundsCount)], audioBoostPunch);
        }
    }
}
