using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField] private StabbingWeaponList stabbingWeaponList;
    [SerializeField] private bool setOwnData;
    [SerializeField] public int maxHealth;
    [SerializeField] public int currentHealth;
    // Убрать/переделать под артефакты :
    [SerializeField] public int currentStabbingNum;
    [SerializeField] public int currentSceneNum;
    // new:
    [SerializeField] public int lvl;
    [SerializeField] public int currentExp;
    [SerializeField] public int damage;
    [SerializeField] public int neededExp;

    PlayerAttackManager playerAttackManager;
    HealthUI playerHealth;
    private void Start()
    {
        playerAttackManager = GetComponent<PlayerAttackManager>();
        playerHealth = GetComponent<HealthUI>();
        if (setOwnData)
        {
            SetData();
        }
        else
        {
            SetLastSessionData();
        }
    }
    public PlayerDataManager(int maxPlayerHealth, int currentStabbingNum, int currentSceneNum, int lvl, int currentExp, int neededExp, int damage)
    {
        this.maxHealth = maxPlayerHealth;
        this.currentHealth = maxPlayerHealth;
        this.currentStabbingNum = currentStabbingNum;
        this.currentSceneNum = currentSceneNum;
        this.lvl = lvl;
        this.currentExp = currentExp;
        this.neededExp = neededExp;
        this.damage = damage;
    }
    public PlayerDataManager(int maxPlayerHealth, int currentPlayerHealth, int currentStabbingNum, int currentSceneNum, int lvl, int currentExp, int neededExp, int damage)
    {
        this.maxHealth = maxPlayerHealth;
        this.currentHealth = currentPlayerHealth;
        this.currentStabbingNum = currentStabbingNum;
        this.currentSceneNum = currentSceneNum;
        this.lvl = lvl;
        this.currentExp = currentExp;
        this.neededExp = neededExp;
        this.damage = damage;
    }
    public PlayerDataManager(PlayerData playerData)
    {
        this.maxHealth = playerData.maxPlayerHealth;
        this.currentHealth = playerData.playerHealth;
        this.currentStabbingNum = playerData.stabbingWeaponNum;
        this.currentSceneNum = playerData.sceneNum;
        this.lvl = playerData.lvl;
        this.currentExp = playerData.currentExp;
        this.neededExp = playerData.neededExp;
        this.damage = playerData.damage;
    }
    public void SetLastSessionData()
    {
        string fileDataName = "currentLevelSetings_session_" + SaveSystem.LoadSession().GetActiveSessionNum().ToString();
        LoadData(fileDataName);

    }
    public void SetCheckPointSessionData()
    {
        string fileCheckPointDataName = "checkPointLevelSetings_session_" + SaveSystem.LoadSession().GetActiveSessionNum().ToString();
        PlayerData data = SaveSystem.LoadPlayer(fileCheckPointDataName);

        string fileDataName = "currentLevelSetings_session_" + SaveSystem.LoadSession().GetActiveSessionNum().ToString();
        SaveSystem.SavePlayer(fileDataName, new PlayerDataManager(data));

        SetLastSessionData();
    }
    public void RefreshLastSessionData(bool setNewSceneNum = false, int newStartSceneNum = -1)
    {
        GetData();
        string fileDataName = "currentLevelSetings_session_" + SaveSystem.LoadSession().GetActiveSessionNum().ToString();
        if (!setNewSceneNum)
        {
            SaveSystem.SavePlayer(fileDataName, this);
        }
        else
        {
            SaveSystem.SavePlayer(fileDataName, new PlayerDataManager(this.maxHealth, this.currentHealth, this.currentStabbingNum, newStartSceneNum, this.lvl, this.currentExp, this.neededExp, this.damage));
        }
    }

    public void RefreshCheckPointSessionData(bool setNewSceneNum = false, int newStartSceneNum = -1)
    {
        RefreshLastSessionData(setNewSceneNum, newStartSceneNum);

        string fileCheckPointDataName = "checkPointLevelSetings_session_" + SaveSystem.LoadSession().GetActiveSessionNum().ToString();
        if (!setNewSceneNum)
        {
            SaveSystem.SavePlayer(fileCheckPointDataName, this);
        }
        else
        {
            SaveSystem.SavePlayer(fileCheckPointDataName, new PlayerDataManager(this.maxHealth, this.currentHealth, this.currentStabbingNum, newStartSceneNum, this.lvl, this.currentExp, this.neededExp, this.damage));
        }
    }


    private void LoadData(string fileDataName)
    {
        PlayerData data = SaveSystem.LoadPlayer(fileDataName);
        SetDataInCurrentClass(data);
        SetData();
    }

    private void SetDataInCurrentClass(PlayerData playerData)
    {
        this.maxHealth = playerData.maxPlayerHealth;
        this.currentHealth = playerData.playerHealth;
        this.currentStabbingNum = playerData.stabbingWeaponNum;
        this.currentSceneNum = playerData.sceneNum;
        this.lvl = playerData.lvl;
        this.currentExp = playerData.currentExp;
        this.neededExp = playerData.neededExp;
        this.damage = playerData.damage;
    }


    #region Set data in Player's scripts
    private void SetData()
    {
        if (!SetScene())
        {
            SetDataInAttack();
            SetDataInHealth();
        }
    }

    private bool SetScene()
    {
        int openedScene = SceneManager.GetActiveScene().buildIndex;
        if (!GetComponent<HealthUI>().IsPlayerDead())
        {
            return false;
        }
        SceneManager.LoadScene(currentSceneNum);
        return true;
    }

    private void SetDataInHealth()
    {
        playerHealth.SetMaxHealth(maxHealth);
        playerHealth.SetCurrentHealth(currentHealth);
    }

    private void SetDataInAttack()
    {
        playerAttackManager.currentStabbingWeapon = stabbingWeaponList.GetStabbingWeapon(currentStabbingNum);
    }
    #endregion

    #region Get data from Player's scripts
    private void GetData()
    {
        GetDataFromAttack();
        GetDataFromHealth();
        GetDataAboutScene();
    }

    private void GetDataAboutScene()
    {
        currentSceneNum = SceneManager.GetActiveScene().buildIndex;
    }

    private void GetDataFromHealth()
    {
        currentHealth = playerHealth.GetHelath();
        maxHealth = playerHealth.GetMaxHealth();
    }

    private void GetDataFromAttack()
    {
        StabbingWeapon lastStabbingWeapon = playerAttackManager.currentStabbingWeapon;
        int stabbingWeaponListLenght = stabbingWeaponList.GetListLength();
        for (int stabbingWeaponNum = 0; stabbingWeaponNum < stabbingWeaponListLenght; stabbingWeaponNum++)
        {
            if(stabbingWeaponList.GetStabbingWeapon(stabbingWeaponNum) == lastStabbingWeapon)
            {
                currentStabbingNum = stabbingWeaponNum;
                break;
            }
        }
    }
    #endregion
}
