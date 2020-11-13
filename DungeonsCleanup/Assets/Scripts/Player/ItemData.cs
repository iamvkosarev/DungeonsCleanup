using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType { TimeCrystal, Artifact }
public class ItemData : ScriptableObject
{
    [Header("Base properties")]
    [SerializeField] public ItemType itemType;
    [SerializeField] public int id;

    public void SetIdAndType( int id,ItemType itemType)
    {
        this.itemType = itemType;
        this.id = id;
    }
}
