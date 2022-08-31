using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField] private bool setOwnData;
    [SerializeField] public int currentHealth;
    [SerializeField] public int currentSceneNum;
    [SerializeField] public int lvl;
    [SerializeField] public int currentExp;
    [SerializeField] public int[] listOfItemsId;
    [SerializeField] public int[] listOfItemsTypes;
    [SerializeField] public int currentSelectedItem;

    private PlayerHealth playerHealth;
    private PlayerDevelopmentManager playerDevelopmentManager;
    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerDevelopmentManager = GetComponent<PlayerDevelopmentManager>();
        if (setOwnData)
        {
            SetData();
        }
        else
        {
            SetLastSessionData();
        }
    }
    public PlayerDataManager(int currentPlayerHealth, int currentSceneNum, int lvl, int currentExp, int[] listOfItemsId, int[] listOfItemsTypes, int currentSelectedItem)
    {
        this.currentHealth = currentPlayerHealth;
        this.currentSceneNum = currentSceneNum;
        this.lvl = lvl;
        this.currentExp = currentExp;
        this.listOfItemsId = listOfItemsId;
        this.listOfItemsTypes = listOfItemsTypes;
        this.currentSelectedItem = currentSelectedItem;
    }
    public PlayerDataManager(PlayerData playerData)
    {
        this.currentHealth = playerData.playerHealth;
        this.currentSceneNum = playerData.sceneNum;
        this.lvl = playerData.lvl;
        this.currentExp = playerData.currentExp;
        this.listOfItemsId = playerData.listOfItemsId;
        this.listOfItemsTypes = playerData.listOfItemsTypes;
        this.currentSelectedItem = playerData.currentSelectedItem;

    }
    public void SetLastSessionData()
    {
        string fileDataName = "currentLevelSetings_session_" + SaveSystem.LoadSession().GetActiveSessionNum().ToString();
        Debug.Log("Start loading data from" + fileDataName);
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
            SaveSystem.SavePlayer(fileDataName, new PlayerDataManager(this.currentHealth,newStartSceneNum, this.lvl, this.currentExp, this.listOfItemsId, this.listOfItemsTypes, this.currentSelectedItem));
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
            SaveSystem.SavePlayer(fileCheckPointDataName, new PlayerDataManager(this.currentHealth, newStartSceneNum, this.lvl, this.currentExp, this.listOfItemsId, this.listOfItemsTypes, currentSelectedItem));
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
        this.currentHealth = playerData.playerHealth;
        this.currentSceneNum = playerData.sceneNum;
        this.lvl = playerData.lvl;
        this.currentExp = playerData.currentExp;
        this.listOfItemsId = playerData.listOfItemsId;
        this.listOfItemsTypes = playerData.listOfItemsTypes;
        this.currentSelectedItem = playerData.currentSelectedItem;
    }


    #region Set data in Player's scripts
    private void SetData()
    {
        if (!SetScene())
        {
            SetDataInDeveloperManager();
            SetDataInHealth();
        }
    }

    private void SetDataInDeveloperManager()
    {
        playerDevelopmentManager.SetCurrentExp(currentExp);
        playerDevelopmentManager.SetCurrentLvl(lvl);
        playerDevelopmentManager.SetItems(listOfItemsId, listOfItemsTypes);
        playerDevelopmentManager.SetCurrentSelectedItem(currentSelectedItem);
        playerDevelopmentManager.SetParametersAccordingToTheLvl();
        playerDevelopmentManager.SetExpInExpBar();
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
        if (currentHealth == -1)
        {
            playerHealth.SetCurrentHealth(playerHealth.GetMaxHealth());
        }
        else
        {
            playerHealth.SetCurrentHealth(currentHealth);
        }
    }

    #endregion

    #region Get data from Player's scripts
    private void GetData()
    {
        GetDataFromHealth();
        GetDataFromDeveloperManager();
        GetDataAboutScene();
        GetItemsInfo();
    }

    private void GetItemsInfo()
    {
        listOfItemsId = playerDevelopmentManager.GetListOfItemsId();
        listOfItemsTypes = playerDevelopmentManager.GetListOfItemsTypes();
    }

    private void GetDataFromDeveloperManager()
    {
        currentExp = playerDevelopmentManager.GetCurrentExp();
        lvl = playerDevelopmentManager.GetCurrentLvl();
        currentSelectedItem = playerDevelopmentManager.GetCurrentSelectedItemIndex();
    }

    private void GetDataAboutScene()
    {
        currentSceneNum = SceneManager.GetActiveScene().buildIndex;
    }

    private void GetDataFromHealth()
    {
        currentHealth = playerHealth.GetHelath();
    }

    #endregion
}
