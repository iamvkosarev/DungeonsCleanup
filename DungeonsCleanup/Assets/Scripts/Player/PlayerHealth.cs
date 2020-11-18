using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private bool canProtectHimself = true;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float reloadingDelay = 2f;
    [SerializeField] private AudioClip heartBitting;
    [SerializeField] private float audioBoost;
    private AudioSource myAudioSource;
    private PlayerDevelopmentManager playerDevelopmentManager;
    private bool isHeartBitting;
    private bool isProtecting;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        playerDevelopmentManager = GetComponent<PlayerDevelopmentManager>();
        SetMaxHealth(playerDevelopmentManager.GetMaxHealthAccordingLvl());
    }
    public void SetMaxHealth(int maxHelath)
    {
        healthBar.SetMaxHealth(maxHelath);
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
        if (canProtectHimself && isProtecting) { return; }
        base.TakeAwayHelath(damage);
        healthBar.SetHealth(base.GetHealth());
        PlayHeartBittingSVF();
    }


    private void PlayHeartBittingSVF()
    {
        if ((float)base.GetHealth()/(float)healthBar.GetMaxHelath() < 0.2f)
        {
            if (!isHeartBitting)
            {
                myAudioSource.PlayOneShot(heartBitting, audioBoost);
                StartCoroutine(WaitForNextBitting());
            }
        }
    }
    IEnumerator WaitForNextBitting()
    {
        isHeartBitting = true;
        yield return new WaitForSeconds(heartBitting.length);
        isHeartBitting = false;
    }

    public override void CheckZeroHealth()
    {
        if (base.GetHealth() == 0)
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
        SetVisibilityOfEnemies(false);
        GetComponent<PlayerMovement>().SetCollidingOfEnemiesMode(false);
        StartCoroutine(Reloading());
    }

    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(reloadingDelay);
        GetComponent<PlayerDataManager>().SetCheckPointSessionData();
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
