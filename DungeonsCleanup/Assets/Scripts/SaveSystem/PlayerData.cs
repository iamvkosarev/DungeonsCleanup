using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int sceneNum;
    public int maxPlayerHealth;
    public int playerHealth;
    public int stabbingWeaponNum;

    public PlayerData (PlayerDataManager playerDataManager)
    {
        sceneNum = playerDataManager.currentSceneNum;
        maxPlayerHealth = playerDataManager.maxPlayerHealth;
        playerHealth = playerDataManager.currentPlayerHealth;
        stabbingWeaponNum = playerDataManager.currentStabbingNum;
    }
}
