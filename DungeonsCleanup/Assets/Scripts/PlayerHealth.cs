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
        healthBar.SetMaxHealth(base.GetHealth());
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
