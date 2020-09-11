﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StabbingWeapon")]
public class StabbingWeapon : ScriptableObject
{
    [SerializeField] string nameOfWeapon = "Без имени";
    [TextArea(minLines:3, maxLines:6)] [SerializeField] string description = "Что-то делает";
    [SerializeField] string itemClass = "E-класс";
    [SerializeField] string source = "Кузня Гефеста";
    [SerializeField] int[] damages;
    [SerializeField] float[] attackRadius;
    [SerializeField] List<FramesList> animationFramesList;
    [SerializeField] Sprite icon;


    public Sprite GetAnimationSprtie(int numOfAttack, int numOfFrame)
    {
        if (animationFramesList[numOfAttack].GetFrame(numOfFrame) == null)
        {
            Debug.Log($"Weapon {name} hasn't {numOfFrame} sprite in {numOfAttack} num of array");
        }
        return animationFramesList[numOfAttack].GetFrame(numOfFrame);
    }
    public Sprite GetIcon()
    {
        return icon;
    }
    public string GetName()
    {
        return nameOfWeapon;
    }
    
    public string GetDescription()
    {
        return description;
    }

    public int GetDamage(int numOfAttack)
    {
        if (damages.Length <= numOfAttack)
        {
            Debug.Log($"Weapon {name} hasn't any damage in {numOfAttack} num of array");
        }
        return damages[numOfAttack];
    }

    public float GetAttackRadius(int numOfAttack)
    {
        if (damages.Length <= numOfAttack)
        {
            Debug.Log($"Weapon {name} hasn't any attack radius in {numOfAttack} num of array");
        }
        return attackRadius[numOfAttack];
    }
}