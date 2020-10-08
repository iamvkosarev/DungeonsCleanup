using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int maxPlayerHealth;
    public int playerHealth;
    public int stabbingWeaponNum;

    public PlayerData (PlayerDataManager playerDataManager)
    {
        maxPlayerHealth = playerDataManager.maxPlayerHealth;
        playerHealth = playerDataManager.currentPlayerHealth;
        stabbingWeaponNum = playerDataManager.currentStabbingNum;
    }
}
