using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUI : Health
{
    [SerializeField] HealthBar healthBar;
    [SerializeField] BoxCollider2D playerHealthCollider;
    [SerializeField] float reloadingDelay = 2f;
    bool isProtecting;
    private void Start()
    {
        SetMaxHealth(base.health);
        SetCurrentHealth(base.health);
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
