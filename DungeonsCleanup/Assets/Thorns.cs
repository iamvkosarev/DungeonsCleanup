using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    [SerializeField] private bool hasTeleportToStart = false;
    [SerializeField] private Transform startPlayerPos;
    [SerializeField] private int damage = 10;
    [SerializeField] private float delayOnAttack = 2f;
    [SerializeField] private int playerLayerNum;
    [SerializeField] private int enemiesLayerNum;
    private List<AttackedCreature> attackedCreatures = new List<AttackedCreature>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.layer == playerLayerNum)
        {
            if (HasTimePassedSinceAttack(collision.gameObject))
            {
                if (hasTeleportToStart)
                {
                    collision.gameObject.transform.position = startPlayerPos.position;
                }
                collision.gameObject.GetComponent<PlayerHealth>().TakeAwayHelath(damage);
                
            }
        }
        else if(collision.gameObject.layer == enemiesLayerNum)
        {
            if (HasTimePassedSinceAttack(collision.gameObject))
            {
                collision.gameObject.GetComponent<Health>().TakeAwayHelath(damage);
            }
        }

    }

    private bool HasTimePassedSinceAttack(GameObject gameObject)
    {
        bool isGameObjectReadyToAttack = false;

        foreach(AttackedCreature attackedCreature in attackedCreatures)
        {
            if (gameObject.GetInstanceID() == attackedCreature.instanceID)
            {
                if(Time.time - attackedCreature.lastAttackTime >= delayOnAttack)
                {
                    isGameObjectReadyToAttack = true;
                    break;
                }
            }
        }
        if (!isGameObjectReadyToAttack)
        {
            attackedCreatures.Add(new AttackedCreature(gameObject.GetInstanceID(), Time.time));
            isGameObjectReadyToAttack = true;
        }
        return isGameObjectReadyToAttack;
    }

    public class AttackedCreature: MonoBehaviour
    {
        public float lastAttackTime { set; get; }
        public int instanceID { set; get; }

        public AttackedCreature(int instanceID, float lastAttackTime)
        {
            this.lastAttackTime = lastAttackTime;

            this.instanceID = instanceID;
        }
    }
}
