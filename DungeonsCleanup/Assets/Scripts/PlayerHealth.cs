using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] HealthBar healthBar;

    private void Start()
    {
        healthBar.SetMaxHealth(base.GetHealth());
    }
    public override void TakeAwayHelath(int damage)
    {
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

    private void Death()
    {
        // KillPlayer
    }
}
