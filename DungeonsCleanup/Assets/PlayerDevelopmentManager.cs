using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDevelopmentManager : MonoBehaviour
{
    [SerializeField] private ListLevelOfDevelopment listLevelOfDevelopment;
    [Header("Level's parameters")]
    [SerializeField] private int lvl;
    [SerializeField] private int exp;
    
    private int needExp;
    private PlayerAttackManager attackManager;
    private PlayerHealth healthManager;

    private void Start()
    {
        attackManager = GetComponent<PlayerAttackManager>();
        healthManager = GetComponent<PlayerHealth>();
    }

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
        SetParametersAccordingToTheLvl();
        needExp = listLevelOfDevelopment.GetParammeterOfLevel(lvl).GetNeedExp();
        // shoe some VFX;
    }

    #region Counters
    public int CountDamage()
    {
        return listLevelOfDevelopment.GetParammeterOfLevel(lvl - 1).GetDamage();
    }

    public int CountMaxHP()
    {
        return listLevelOfDevelopment.GetParammeterOfLevel(lvl - 1).GetMaxHP();
    }

    #endregion

    #region Getters

    public int GetMaxHealthAccordingLvl()
    {
        return CountMaxHP();
    }
    public int GetDamageAccordingLvl()
    {
        return CountDamage();
    }
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

    #region Set Parameters According ToThe Lvl
    public void SetParametersAccordingToTheLvl()
    {
        needExp = listLevelOfDevelopment.GetParammeterOfLevel(lvl).GetNeedExp();
        SetHealth();
        SetDamage();
    }
    private void SetHealth()
    {
        if (healthManager)
        {
            healthManager.SetMaxHealth(CountMaxHP());
        }
    }
    private void SetDamage()
    {
        if (attackManager)
        { 
            attackManager.SetDamage(CountDamage());
        }
    }
    #endregion

    #region Set Values
    public void SetCurrentLvl(int lvl)
    {
        this.lvl = lvl;
    }
    public void SetCurrentExp(int exp)
    {
        this.exp = exp;
    }
    #endregion

    #endregion
}
