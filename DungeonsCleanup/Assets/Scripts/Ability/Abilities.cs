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
    public void WindPush(Vector2 playerPosition, Vector2 windPushRadius, LayerMask enemiesLayer, float pushForce)
    {
        Debug.Log("Do wind push");
        bool ifGoblinsInWindPushRadius = Physics2D.OverlapBox(playerPosition, windPushRadius, 0, enemiesLayer);
        Debug.Log(ifGoblinsInWindPushRadius);
        Collider2D[] enemiesInWindPushRadius = Physics2D.OverlapBoxAll(playerPosition, windPushRadius, 0, enemiesLayer);
        foreach(Collider2D enemy in enemiesInWindPushRadius)
        {
            Vector2 feetPosition = enemy.gameObject.transform.parent.position;
            Debug.Log(feetPosition +" "+ playerPosition);
            float gipotenusa = Mathf.Sqrt(Mathf.Pow(feetPosition.x - playerPosition.x, 2) + Mathf.Pow(feetPosition.y - playerPosition.y,2));
            Vector2 singleVector;
            if (gipotenusa == 0)
            {
                singleVector = new Vector2(0, 0);
            }
            else { singleVector = new Vector2(feetPosition.x - playerPosition.x / gipotenusa, feetPosition.y - playerPosition.y / gipotenusa); }

            EnemiesMovement enemiesMovement = enemy.gameObject.GetComponentInParent<EnemiesMovement>();
            if (enemiesMovement)
            {
                enemiesMovement.GetPunch(singleVector.x * pushForce, singleVector.y * pushForce);
            }
        }
    }
}
