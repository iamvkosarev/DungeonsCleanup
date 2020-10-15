using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField] private StabbingWeaponList stabbingWeaponList;
    [SerializeField] private bool setOwnData;
    public int maxPlayerHealth;
    public int currentPlayerHealth;
    public int currentStabbingNum;
    public int currentSceneNum;

    PlayerAttackManager playerAttackManager;
    PlayerHealth playerHealth;
    private void Start()
    {
        playerAttackManager = GetComponent<PlayerAttackManager>();
        playerHealth = GetComponent<PlayerHealth>();
        if (setOwnData)
        {
            SetData();
        }
        else
        {
            SetLastSessionData();
        }
    }
    public PlayerDataManager(int maxPlayerHealth, int currentStabbingNum, int currentSceneNum)
    {
        this.maxPlayerHealth = maxPlayerHealth;
        this.currentPlayerHealth = maxPlayerHealth;
        this.currentStabbingNum = currentStabbingNum;
        this.currentSceneNum = currentSceneNum;
    }
    public PlayerDataManager(int maxPlayerHealth, int currentPlayerHealth, int currentStabbingNum, int currentSceneNum)
    {
        this.maxPlayerHealth = maxPlayerHealth;
        this.currentPlayerHealth = currentPlayerHealth;
        this.currentStabbingNum = currentStabbingNum;
        this.currentSceneNum = currentSceneNum;
    }
    public PlayerDataManager(PlayerData playerData)
    {
        this.maxPlayerHealth = playerData.maxPlayerHealth;
        this.currentPlayerHealth = playerData.playerHealth;
        this.currentStabbingNum = playerData.stabbingWeaponNum;
        this.currentSceneNum = playerData.sceneNum;
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
            SaveSystem.SavePlayer(fileDataName, new PlayerDataManager(this.maxPlayerHealth, this.currentPlayerHealth, this.currentStabbingNum, newStartSceneNum));
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
            SaveSystem.SavePlayer(fileCheckPointDataName, new PlayerDataManager(this.maxPlayerHealth, this.currentPlayerHealth, this.currentStabbingNum, newStartSceneNum));
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
        this.maxPlayerHealth = playerData.maxPlayerHealth;
        this.currentPlayerHealth = playerData.playerHealth;
        this.currentStabbingNum = playerData.stabbingWeaponNum;
        this.currentSceneNum = playerData.sceneNum;
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
        if (!GetComponent<PlayerHealth>().IsPlayerDead())
        {
            return false;
        }
        SceneManager.LoadScene(currentSceneNum);
        return true;
    }

    private void SetDataInHealth()
    {
        playerHealth.SetMaxHealth(maxPlayerHealth);
        playerHealth.SetCurrentHealth(currentPlayerHealth);
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
        currentPlayerHealth = playerHealth.GetHelath();
        maxPlayerHealth = playerHealth.GetMaxHealth();
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
