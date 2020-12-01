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
    [SerializeField] private ListOfAllShadows listOfAllShadows;
    private Vector2 radiusForPush;

    public  void CallOfTheShadows(Transform playerTransform, GameObject shadow, PatrolPoint playerPatrolPoint)
    {
        Debug.Log("Запуск Вызов теней");
        if (!shadow)
        {
            Debug.Log("Тень не указана");
            return;
        }
        GameObject newShadow = Instantiate(shadow, playerTransform.position, Quaternion.identity);
        newShadow.GetComponent<Shadow>().SetPlayer(playerPatrolPoint, playerTransform);
        
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
    public void WindPush(Vector2 playerPos, Vector2 checkPoint, Vector2 chechZone, LayerMask checkLayer, float pushForce)
    {
        Debug.Log("Do wind push");
        bool ifGoblinsInWindPushRadius = Physics2D.OverlapBox(checkPoint, chechZone, 0, checkLayer);
        Debug.Log(ifGoblinsInWindPushRadius);
        Collider2D[] enemiesInWindPushRadius = Physics2D.OverlapBoxAll(checkPoint, chechZone, 0, checkLayer);
        foreach(Collider2D enemy in enemiesInWindPushRadius)
        {
            Vector2 posEnemie = enemy.gameObject.transform.position;
            Vector2 singleVector = new Vector2(Mathf.Sign(posEnemie.x - playerPos.x), 0.2f);

            EnemiesMovement enemiesMovement = enemy.gameObject.GetComponentInParent<EnemiesMovement>();
            if (enemiesMovement)
            {
                enemiesMovement.GetPunch(singleVector.x * pushForce, singleVector.y * pushForce);
            }
        }
    }
}
