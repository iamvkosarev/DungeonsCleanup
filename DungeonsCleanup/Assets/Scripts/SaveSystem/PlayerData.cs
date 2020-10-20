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
    // new:
    public int lvl;
    public int currentExp;
    public int damage;
    public int neededExp;

    public PlayerData (PlayerDataManager playerDataManager)
    {
        sceneNum = playerDataManager.currentSceneNum;
        maxPlayerHealth = playerDataManager.maxHealth;
        playerHealth = playerDataManager.currentHealth;
        stabbingWeaponNum = playerDataManager.currentStabbingNum;
        this.lvl = playerDataManager.lvl;
        this.currentExp = playerDataManager.currentExp;
        this.neededExp = playerDataManager.neededExp;
        this.damage = playerDataManager.damage;
    }
}
