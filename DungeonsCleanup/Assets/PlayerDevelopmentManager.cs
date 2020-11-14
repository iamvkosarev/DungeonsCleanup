using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDevelopmentManager : MonoBehaviour
{
    [SerializeField] private ListLevelOfDevelopment listLevelOfDevelopment;
    [SerializeField] private ListsOfItmes listsOfItmes;
    [Header("Level's parameters")]
    [SerializeField] private int lvl;
    [SerializeField] private int exp;
    [SerializeField] private List<ItemData> items;
    [Header("Activation Ability")]
    [SerializeField] private int currentItemIndex = 1;
    [SerializeField] private bool activateAbility;
    [SerializeField] private HealthBar healthBar;
    private bool wasActivated;

    private int needExp;
    private PlayerAttackManager attackManager;
    private PlayerHealth healthManager;

    private void Start()
    {
        attackManager = GetComponent<PlayerAttackManager>();
        healthManager = GetComponent<PlayerHealth>();
    }
    private void Update()
    {
        CheckActivation();
    }

    private void CheckActivation()
    {
        if (activateAbility && !wasActivated)
        {
            wasActivated = true;
            ActivateAbility();
        }
        else if(!activateAbility && wasActivated)
        {
            wasActivated = false;
        }
    }

    public void AddExp(int exp)
    {
        if (exp + this.exp >= needExp)
        {
            this.exp = exp + this.exp - needExp;
            IncreaseLevel();
        }
        else
        {
            this.exp += exp;
        }
        SetExpInExpBar();
    }
    public void SetExpInExpBar()
    {
        healthBar.SetExpSliderParam(exp, needExp);
    }
    private void IncreaseLevel()
    {
        lvl++;
        SetParametersAccordingToTheLvl();
        healthManager.SetCurrentHealth(healthManager.GetMaxHealth());
        needExp = listLevelOfDevelopment.GetParammeterOfLevel(lvl).GetNeedExp();
        // shoe some VFX;
    }

    public void ActivateAbility()
    {
        if (items[currentItemIndex].itemType == ItemType.Artifact)
        {
            ArtifactData artifactData = listsOfItmes.GetArtifactData(items[currentItemIndex].id);
            artifactData.Activate();
        }
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
    public List<ItemData> GetItmes()
    {
        return items;
    }
    public int[] GetListOfItemsId()
    {
        var length = items.Count;
        var resultList =new int[length];
        for (int i = 0; i < length; i++)
        {
            resultList[i] = items[i].id;
        }
        return resultList;
    }
    public int[] GetListOfItemsTypes()
    {
        var length = items.Count;
        var resultList = new int[length];
        for (int i = 0; i < length; i++)
        {
            resultList[i] = (int)items[i].itemType;
        }
        return resultList;
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
    public void SetItems(int[] listOfItemsId, int[] listOfItemsTypes)
    {
        int minLangth = Math.Min(listOfItemsId.Length, listOfItemsTypes.Length);
        for(int index =0; index < minLangth; index++)
        {
            var newItem = new ItemData();

            if (listOfItemsTypes[index] == (int)ItemType.TimeCrystal)
            {
                if (listsOfItmes.GetTimeCrystalData(listOfItemsId[index]) != null)
                {
                    newItem.SetIdAndType(listOfItemsId[index], ItemType.TimeCrystal);
                }
                else
                {
                    newItem.SetIdAndType(-1, ItemType.TimeCrystal);
                }
            }
            else if (listOfItemsTypes[index] == (int)ItemType.Artifact)
            {
                if (listsOfItmes.GetArtifactData(listOfItemsId[index]) != null)
                {
                    newItem.SetIdAndType(listOfItemsId[index], ItemType.Artifact);
                }
                else
                {
                    newItem.SetIdAndType(-1, ItemType.Artifact);
                }
            }
            else
            {
                newItem.SetIdAndType(-1, ItemType.Artifact);
            }

            items.Add(newItem);
        }
    }
    #endregion

    #endregion
}
