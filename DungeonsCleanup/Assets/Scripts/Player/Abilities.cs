using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AbilityType
{
    Null,
    WindPush
}
[CreateAssetMenu(menuName = "Abilitys")]
public class Abilities : ScriptableObject
{
    [Header("Wind Push")]
    [SerializeField] private float windPushRadius;
    [SerializeField] private LayerMask enemiesLayer;
    [SerializeField] private float pushXForce;
    [SerializeField] private float pushYForce;

    public void Activate(AbilityType abilityType, Vector2 playerPosition, float direction)
    {
        if (abilityType == AbilityType.Null)
        {
            return;
        }

        if(abilityType == AbilityType.WindPush)
        {
            WindPush(playerPosition);
        }
    }

    private void WindPush(Vector2 playerPosition)
    {
        Debug.Log("Do wind push");
        bool ifGoblinsInWindPushRadius = Physics2D.OverlapCircle(playerPosition, windPushRadius, 0, enemiesLayer);
        Debug.Log(ifGoblinsInWindPushRadius);
        Collider2D[] enemiesInWindPushRadius = Physics2D.OverlapCircleAll(playerPosition, windPushRadius, 0, enemiesLayer);
        foreach(Collider2D enemy in enemiesInWindPushRadius)
        {
            Debug.Log(enemy.gameObject.name);
            enemy.gameObject.GetComponent<EnemiesMovement>().GetPunch(pushXForce, pushYForce);
        }
        // if (isPlayerInAttackZoneToPush)
        // {
        //     float pushXForce = UnityEngine.Random.Range(minPushXForce, maxPushXForce);
        //     float pushYForce = UnityEngine.Random.Range(minPushYForce, maxPushYForce);
        //     playerMovement.GetPunch(pushXForce * Mathf.Sign(transform.localScale.x), pushYForce);
        //     player.gameObject.GetComponent<PlayerHealth>().TakeAwayHelath(pushDamage);
            
        // }
        // currentAttackType = AttackTypes.Simple;
        // movement.shouldGoToPlayer = true;
    }
}
