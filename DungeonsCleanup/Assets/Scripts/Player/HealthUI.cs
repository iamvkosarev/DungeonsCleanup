using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : Health
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] BoxCollider2D playerHealthCollider;
    [SerializeField] float reloadingDelay = 2f;
    [SerializeField] float parametrOfStartingDeathAnimation = 0.05f;
    [SerializeField] ActivationBoss activationBoss;
    bool wasStartedDeath = false;
    bool isProtecting;
    AudioSource myAudioSource;
    Animator myAnimator;
    private void Start()
    {
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
        healthBar.SetHealth(base.GetHealth());
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
