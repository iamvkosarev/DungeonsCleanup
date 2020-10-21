using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDevelopmentManager : MonoBehaviour
{
    [Header("Level's parameters")]
    [SerializeField] private int lvl;
    [SerializeField] private int exp;
    [SerializeField] private int needExp;

    // Count Damage
    private const int START_DAMAGE = 10;
    private const int STEP_DAMAGE = 5;

    //Count HP
    private const int START_HP = 100;
    private const int STEP_HP = 10;

    public void AddExp(int exp)
    {
        if(exp + this.exp >= needExp)
        {
            this.exp = exp + this.exp - needExp;
            IncreaseLevel();
        }
        else
        {
            this.exp += exp;
        }
    }

    private void IncreaseLevel()
    {
        lvl++;
        // shoe some VFX;
    }


    #region Counters
    public int CountDamage()
    {
        return START_DAMAGE + lvl * STEP_DAMAGE;
    }

    public int CountHP()
    {
        return START_HP + lvl * STEP_HP;
    }
    #endregion

    #region Getters
    public int GetCurrentLvl()
    {
        return lvl;
    }
    public int GetCurrentExp()
    {
        return exp;
    }
    public int GetNeedExp()
    {
        return needExp;
    }
    #endregion

    #region Setters
    public void SetCurrentLvl(int lvl)
    {
        this.lvl = lvl;
    }
    public void SetCurrentExp(int exp)
    {
        this.exp = exp;
    }
    public void SetNeedExp(int needExp)
    {
        this.needExp = needExp;
    }
    #endregion
}
