using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "StabbingWeaponList")]
public class StabbingWeaponList : ScriptableObject
{
    [SerializeField] StabbingWeapon[] stabbingWeapons;

    public StabbingWeapon GetStabbingWeapon(int num)
    {
        if (num < stabbingWeapons.Length)
        {
            return stabbingWeapons[num];
        }
        else { return null; }
    }
    public int GetListLength()
    {
        return stabbingWeapons.Length;
    }
}
