using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "List Level Of Development")]
public class ListLevelOfDevelopment : ScriptableObject
{
    [SerializeField] LevelOfDevelopment[] levelOfDevelopments;
    
    public LevelOfDevelopment GetParammeterOfLevel(int lvlNum)
    {
        return levelOfDevelopments[lvlNum];
    }
}


