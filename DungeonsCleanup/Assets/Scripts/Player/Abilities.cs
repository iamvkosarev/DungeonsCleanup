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
    private Vector2 radiusForPush;
    public void Activate(AbilityType abilityType, Vector2 playerPosition, Vector2 windPushRadius, LayerMask enemiesLayer, float pushXForce, float pushYForce)
    {
        if (abilityType == AbilityType.Null)
        {
            return;
        }

        if(abilityType == AbilityType.WindPush)
        {
            WindPush(playerPosition, windPushRadius, enemiesLayer, pushXForce, pushYForce);
        }
    }

    private void WindPush(Vector2 playerPosition, Vector2 windPushRadius, LayerMask enemiesLayer, float pushXForce, float pushYForce)
    {
        Debug.Log("Do wind push");
        bool ifGoblinsInWindPushRadius = Physics2D.OverlapBox(playerPosition, windPushRadius, 0, enemiesLayer);
        Debug.Log(ifGoblinsInWindPushRadius);
        radiusForPush = new Vector2(windPushRadius.x/2, windPushRadius.y);
        Collider2D[] enemiesInWindPushRadius = Physics2D.OverlapBoxAll(playerPosition, windPushRadius, 0, enemiesLayer);
        foreach(Collider2D enemy in enemiesInWindPushRadius)
        {
            Debug.Log(enemy.gameObject.name);
            enemy.gameObject.GetComponent<EnemiesMovement>().GetPunch(pushXForce, pushYForce);
        }
    }
}
