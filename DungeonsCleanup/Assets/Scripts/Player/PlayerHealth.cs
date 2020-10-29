using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float reloadingDelay = 2f;
    private PlayerDevelopmentManager playerDevelopmentManager;
    private bool isProtecting;

    private void Start()
    {
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
        if (isProtecting) { return; }
        base.TakeAwayHelath(damage);
        healthBar.SetHealth(base.GetHealth());
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
