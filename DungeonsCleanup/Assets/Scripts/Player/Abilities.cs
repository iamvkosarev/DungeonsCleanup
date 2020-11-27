using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AbilityType
{
    Null,
    WindPush,
    CallOfTheShadows
}
public class Abilities : MonoBehaviour
{
    private Vector2 radiusForPush;

    public void CallOfTheShadows(Transform playerTransform, int shadowsBottleId)
    {
        ShadowBorrleData shadowBorrleData = SaveSystem.LoadShadowBorrleData(shadowsBottleId);
        if (shadowBorrleData.HasShadows())
        {
            GameObject newShadow = Instantiate(shadowBorrleData.GetShadow(), playerTransform.position, Quaternion.identity);
            newShadow.GetComponent<Shadow>().SetPlayer(playerTransform.gameObject.GetComponentInChildren<PatrolPoint>());
        }
    }
    public void AddShadowsIntoTheBorrle(int shadowsBottleId, int newShadowId)
    {
        ShadowBorrleData shadowBorrleData = SaveSystem.LoadShadowBorrleData(shadowsBottleId);
        bool resultOfAdding = shadowBorrleData.AddShadow(newShadowId);
        if (resultOfAdding)
        {
            Debug.Log("Тень была сохранена");
            SaveSystem.SaveShadowBorrleData(shadowsBottleId, shadowBorrleData.listOfShadows);
        }
    }
    public void WindPush(Vector2 playerPosition, Vector2 windPushRadius, LayerMask enemiesLayer, float pushXForce, float pushYForce)
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
