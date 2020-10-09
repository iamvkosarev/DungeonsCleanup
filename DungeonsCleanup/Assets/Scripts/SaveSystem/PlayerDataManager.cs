using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField] private StabbingWeaponList stabbingWeaponList;
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
        LoadData();
    }

    public void LoadData()
    {
        // Нужно выбирать какой тип данных игрока подгружать:
        //     - Те, что после смерти
        //     - Те, что после выхода их игры
        string levelSettingsName = "levelSetings_session_" + SaveSystem.LoadSession().ToString();
        PlayerData data = SaveSystem.LoadPlayer(levelSettingsName);
        maxPlayerHealth = data.maxPlayerHealth;
        currentPlayerHealth = data.playerHealth;
        currentStabbingNum = data.stabbingWeaponNum;
        currentSceneNum = data.sceneNum; ;

        SetData();
    }

    public void SaveData()
    {
        GetData();
        // Нужно выбирать какой тип данных игрока сохранять:
        //     - Те, что после смерти
        //     - Те, что после выхода их игры
        string levelSettingsName = "levelSetings_session_" + SaveSystem.LoadSession().ToString();
        SaveSystem.SavePlayer(levelSettingsName, this);
    }

    #region Set data in Player's scripts
    private void SetData()
    {
        SetScene();
        SetDataInAttack();
        SetDataInHealth();
    }

    private void SetScene()
    {
        int openedScene = SceneManager.GetActiveScene().buildIndex;
        if (openedScene == currentSceneNum)
        {
            return;
        }
        SceneManager.LoadScene(currentSceneNum);
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
        StabbingWeapon lastStavvingWeapon = playerAttackManager.currentStabbingWeapon;
        int stabbingWeaponListLenght = stabbingWeaponList.GetListLength();
        for (int stabbingWeaponNum = 0; stabbingWeaponNum < stabbingWeaponListLenght; stabbingWeaponNum++)
        {
            if(stabbingWeaponList.GetStabbingWeapon(stabbingWeaponNum) == lastStavvingWeapon)
            {
                currentStabbingNum = stabbingWeaponNum;
                break;
            }
        }
    }
    #endregion
}
