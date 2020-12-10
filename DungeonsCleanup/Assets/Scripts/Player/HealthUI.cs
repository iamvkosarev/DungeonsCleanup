using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : Health
{
    [SerializeField] private AudioClip[] getHitsSFX;
    private int getHitsSFXLength;
    [SerializeField] private float audioBoostGetHitSFX;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private BoxCollider2D playerHealthCollider;
    [SerializeField] private float reloadingDelay = 2f;
    [SerializeField] private float parametrOfStartingDeathAnimation = 0.05f;
    [SerializeField] ActivationBoss activationBoss;
    [Header("Death")]
    [SerializeField] private GameObject bossHeadItem;
    [SerializeField] private Transform bossHeadItemPosition;
    bool wasStartedDeath = false;
    bool isProtecting;
    AudioSource myAudioSource;
    Animator myAnimator;
    private void Start()
    {
        getHitsSFXLength = getHitsSFX.Length;
        SetMaxHealth(base.health);
        myAnimator = GetComponent<Animator>();
        SetCurrentHealth(base.health);
        myAudioSource = GetComponent<AudioSource>();
    }
    public void SetMaxHealth(int maxHelath)
    {
        healthBar.SetMaxHealth(maxHelath);
    }
    public void SetVisibilityOfEnemies(bool mode)
    {
        playerHealthCollider.enabled = mode;
    }
    public void SetCurrentHealth(int health)
    {
        healthBar.SetHealth(health);
        base.health = health;
    }
    public int GetMaxHealth()
    {
        return healthBar.GetMaxHelath();
    }

    public int GetHelath()
    {
        return healthBar.GetHelath();
    }
    public override void TakeAwayHelath(int damage)
    {
        if (isProtecting) { return; }
        base.TakeAwayHelath(damage);
        SpawnHitSFX();
        healthBar.SetHealth(base.GetHealth());
    }

    private void SpawnHitSFX()
    {
        myAudioSource.PlayOneShot(getHitsSFX[UnityEngine.Random.Range(0, getHitsSFXLength)], audioBoostGetHitSFX);
    }

    public override void CheckZeroHealth()
    {
        if (base.GetHealth() <= (int)(parametrOfStartingDeathAnimation * GetMaxHealth()) && base.GetHealth()>0f)
        {
            if (!wasStartedDeath)
            {
                wasStartedDeath = true;
                myAnimator.Play("Boss Start Death");
            }
        }
        else if (base.GetHealth() <= 0f)
        {
            Death();
        }
    }
    public void SetProtectingMode(bool isProtecting)
    {
        this.isProtecting = isProtecting;
    }
    private void Death()
    {
        SpawnDeathSFX();
        myAnimator.SetTrigger("EndDeath");
        SetVisibilityOfEnemies(false);
        activationBoss.BossDeath();
    }

    public void SpawnBossHeadItem()
    {
        Instantiate(bossHeadItem, bossHeadItemPosition.position, Quaternion.identity);
    }

    private void SpawnGetHitSFX()
    {
        if (getHitSFX)
        {
            myAudioSource.PlayOneShot(getHitSFX, audioBoostGetHit);
        }
    }
    private void SpawnDeathSFX()
    {
        if (deathSFX)
        {
            myAudioSource.PlayOneShot(deathSFX, audioBoostDeathSFX);
            myAudioSource.PlayOneShot(bodyCrash, audioBoostBodyCrash);
        }
    }
    public bool IsPlayerDead()
    {
        if (base.GetHealth() == 0)
        {
            return true;
        }
        return false;
    }
}
