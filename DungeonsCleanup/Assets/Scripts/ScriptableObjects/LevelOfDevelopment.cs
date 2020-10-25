using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Single Level Of Development")]
public class LevelOfDevelopment : ScriptableObject
{
    [SerializeField] private int needExperience;
    [SerializeField] private int damageOnLvl;
    [SerializeField] private int maxHPOnLvl;

    public int GetNeedExp()
    {
        return needExperience;
    }

    public int GetDamage()
    {
        return damageOnLvl;
    }

    public int GetMaxHP()
    {
        return maxHPOnLvl;
    }
}