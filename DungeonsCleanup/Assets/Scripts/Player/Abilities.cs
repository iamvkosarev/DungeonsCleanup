using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AbilityType
{
    Null,
    WindPush
}
public class Abilities : MonoBehaviour
{
    static public void Activate(AbilityType abilityType)
    {
        if (abilityType == AbilityType.Null)
        {
            return;
        }

        if(abilityType == AbilityType.WindPush)
        {
            WindPush();
        }
    }

    private static void WindPush()
    {
        Debug.Log("Do wind push");
    }
}
