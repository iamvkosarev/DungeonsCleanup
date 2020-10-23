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
    [SerializeField] public int neededExp;

    private HealthUI playerHealth;
    private PlayerDevelopmentManager playerDevelopmentManager;
    private void Start()
    {
        playerHealth = GetComponent<HealthUI>();
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
    public PlayerDataManager(int currentPlayerHealth, int currentSceneNum, int lvl, int currentExp, int neededExp)
    {
        this.currentHealth = currentPlayerHealth;
        this.currentSceneNum = currentSceneNum;
        this.lvl = lvl;
        this.currentExp = currentExp;
        this.neededExp = neededExp;
    }
    public PlayerDataManager(int currentPlayerHealth, int currentStabbingNum, int currentSceneNum, int lvl, int currentExp, int neededExp)
    {
        this.currentHealth = currentPlayerHealth;
        this.currentSceneNum = currentSceneNum;
        this.lvl = lvl;
        this.currentExp = currentExp;
        this.neededExp = neededExp;
    }
    public PlayerDataManager(PlayerData playerData)
    {
        this.currentHealth = playerData.playerHealth;
        this.currentSceneNum = playerData.sceneNum;
        this.lvl = playerData.lvl;
        this.currentExp = playerData.currentExp;
        this.neededExp = playerData.neededExp;
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
            SaveSystem.SavePlayer(fileDataName, new PlayerDataManager(this.currentHealth,newStartSceneNum, this.lvl, this.currentExp, this.neededExp));
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
            SaveSystem.SavePlayer(fileCheckPointDataName, new PlayerDataManager(this.currentHealth, newStartSceneNum, this.lvl, this.currentExp, this.neededExp));
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
        this.neededExp = playerData.neededExp;
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
        playerDevelopmentManager.SetNeedExp(neededExp);

        playerDevelopmentManager.SetParametersAccordingToTheLvl();
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
    }

    private void GetDataFromDeveloperManager()
    {
        currentExp = playerDevelopmentManager.GetCurrentExp();
        lvl = playerDevelopmentManager.GetCurrentLvl();
        neededExp = playerDevelopmentManager.GetNeedExp();
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
