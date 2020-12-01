using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] AudioClip[] getHitsSFX;
    private int getHitsSFXLength;
    [SerializeField] float audioBoostGetHitSFX;
    [SerializeField] private bool canProtectHimself = true;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float reloadingDelay = 2f;
    [SerializeField] private AudioClip heartBitting;
    [SerializeField] private float audioBoost;
    [SerializeField] private LoseMenuScript loseCanvas;
    private AudioSource myAudioSource;
    private PlayerDevelopmentManager playerDevelopmentManager;
    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;
    private bool isHeartBitting;
    private bool isProtecting;

    private void Start()
    {
        getHitsSFXLength = getHitsSFX.Length;
        playerMovement = GetComponent<PlayerMovement>();
        myAudioSource = GetComponent<AudioSource>();
        playerDevelopmentManager = GetComponent<PlayerDevelopmentManager>();
        playerAnimation = GetComponent<PlayerAnimation>();
        SetMaxHealth(playerDevelopmentManager.GetMaxHealthAccordingLvl());
    }
    public void SetMaxHealth(int maxHelath)
    {
        healthBar.SetMaxHealth(maxHelath);
    }
    public void AddHealth(int health)
    {
        healthBar.AddHealth(health);
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
        SpawnHitSFX();
    }

    private void SpawnHitSFX()
    {
        myAudioSource.PlayOneShot(getHitsSFX[UnityEngine.Random.Range(0, getHitsSFXLength)], audioBoostGetHitSFX);
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
        SpawnDeathSFX();
        SetVisibilityOfEnemies(false);
        playerAnimation.DoDeathAnimation();
        playerMovement.StopHorizontalMovement();
        playerMovement.StopRotating();
        playerMovement.StopGroundJumps();
        playerMovement.SetCollidingOfEnemiesMode(false);
        StartCoroutine(Reloading());
    }
    public void GiveMaxHP()
    {
        this.SetCurrentHealth(this.GetMaxHealth());
    }
    public void StartToBeAlive()
    {
        SetVisibilityOfEnemies(true);
        playerAnimation.DoIdle();
        playerMovement.StartHorizontalMovement();
        playerMovement.StartRotaing();
        playerMovement.StartGroundJumps();
        playerMovement.SetCollidingOfEnemiesMode(true);
    }
    IEnumerator Reloading()
    {
        yield return new WaitForSeconds(reloadingDelay);
        loseCanvas.SetLoseCanvas();
        //GetComponent<PlayerDataManager>().SetCheckPointSessionData();
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


    public LoseMenuScript GetLoseCanvasScripts()
    {
        return loseCanvas;
    }
}
