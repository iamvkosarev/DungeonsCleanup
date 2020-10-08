using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    [SerializeField] private StabbingWeaponList stabbingWeaponList;
    public int maxPlayerHealth;
    public int currentPlayerHealth;
    public int currentStabbingNum;

    PlayerAttackManager playerAttackManager;
    PlayerHealth playerHealth;
    private void Start()
    {
        playerAttackManager = GetComponent<PlayerAttackManager>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void LoadData()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        maxPlayerHealth = data.maxPlayerHealth;
        currentPlayerHealth = data.playerHealth;
        currentStabbingNum = data.stabbingWeaponNum;

        SetData();
    }

    public void SaveData()
    {
        GetData();
        SaveSystem.SavePlayer(this);
    }

    #region Set data in Player's scripts
    private void SetData()
    {
        SetDataInAttack();
        SetDataInHealth();
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
