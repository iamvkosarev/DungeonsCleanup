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
    private int currentSelectedItemIndex = -1;
    [SerializeField] private bool activateAbility;
    [SerializeField] private HealthBar healthBar;
    private bool wasActivated;
    private int needExp;
    private PlayerAttackManager attackManager;
    private PlayerHealth healthManager;
    private PlayerActionControls playerActionControls;

    [Header("Wind Push")]
    [SerializeField] private Vector2 windPushRadius;
    [SerializeField] private LayerMask enemiesLayer;
    [SerializeField] private float pushXForce;
    [SerializeField] private float pushYForce;
    
    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        playerActionControls.Land.Activateability.started += _ => ActivateAbility();
    }
    private void Start()
    {
        attackManager = GetComponent<PlayerAttackManager>();
        healthManager = GetComponent<PlayerHealth>();
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
        if (wasActivated || currentSelectedItemIndex == -1) { return; }
        if (items[currentSelectedItemIndex].itemType == ItemType.Artifact)
        {
            Vector2 playerPosition = gameObject.transform.position;
            ArtifactData artifactData = listsOfItmes.GetArtifactData(items[currentSelectedItemIndex].id);
            artifactData.Activate(playerPosition, windPushRadius, enemiesLayer, pushXForce, pushYForce);
            artifactData.Activate(gameObject.transform, currentSelectedItemIndex);
        }
    }
    public void DeselectCurrentItem()
    {
        this.currentSelectedItemIndex = -1;
        healthBar.RemoveSelectedItem();
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
    public int GetFreeItemField()
    {
        int freeFiledIndex = -1;
        for (int index = 0; index < items.Count; index++)
        {
            if (items[index].id == -1)
            {
                freeFiledIndex = index;
                break;
            }
        }
        return freeFiledIndex;
    }
    public int GetMaxHealthAccordingLvl()
    {
        return CountMaxHP();
    }
    public int GetCurrentSelectedItem()
    {
        return currentSelectedItemIndex;
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
    public ItemData GetItem(int itemId)
    {
        if(itemId >= items.Count || itemId < 0)
        {
            return null;
        }
        return items[itemId];
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
    public void SetCurrentSelectedItem(int index)
    {
        this.currentSelectedItemIndex = index;
        if (index == -1) {
            healthBar.RemoveSelectedItem();
        }
        else if (items[index].itemType == ItemType.Artifact)
        {
            Debug.Log(index);
            Debug.Log(listsOfItmes.GetArtifactData(items[index].id).nameOfArtifact);
            healthBar.SetSelectedItem(listsOfItmes.GetArtifactData(items[index].id).icon);
        }
        else
        {
            healthBar.RemoveSelectedItem();
        }
    }
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

    public void SetItem(int index, int itemId, ItemType itemType)
    {
        if (index >= items.Count || index < 0) { return; }
        var newItem = new ItemData();
        newItem.SetIdAndType(itemId, itemType);
        items[index] = newItem;
    }
    public ItemData SwitchItem(int index, int itemId, ItemType itemType)
    {
        var oldItem = items[index];
        SetItem(index, itemId, itemType);
        return oldItem;
    }
    #endregion

    #endregion

    private void OnEnable()
    {
        playerActionControls.Enable();
    }
    private void OnDisable()
    {
        playerActionControls.Disable();
    }
}
