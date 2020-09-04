using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerProperties")]
public class PlayerProperties : ScriptableObject
{
    [SerializeField] StabbingWeapon currentStabbingWeapon;
    //[SerializeField] ProjjectileWeapon currentProjjectileWeapon;

    // title
    // level
    // HP
    // MP
    // strength
    // agility
    // sense
    // vitality
    // intelligence

    public StabbingWeapon GetCurrentStabbingWeapons()
    {
        return currentStabbingWeapon;
    }
    public void SetCurrentStabbingWeapons(StabbingWeapon newStabbingWeapon)
    {
        currentStabbingWeapon = newStabbingWeapon;
    }
}
