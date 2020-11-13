using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int sceneNum;
    public int playerHealth;
    public int lvl;
    public int currentExp;
    public int[] listOfItemsId;
    public int[] listOfItemsTypes;

    public PlayerData (PlayerDataManager playerDataManager)
    {
        sceneNum = playerDataManager.currentSceneNum;
        playerHealth = playerDataManager.currentHealth;
        this.lvl = playerDataManager.lvl;
        this.currentExp = playerDataManager.currentExp;
        this.listOfItemsId = playerDataManager.listOfItemsId;
        this.listOfItemsTypes = playerDataManager.listOfItemsTypes;
    }
}
