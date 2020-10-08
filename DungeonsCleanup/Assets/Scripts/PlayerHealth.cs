using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] HealthBar healthBar;
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
        // KillPlayer
    }
}
