using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBossAttack : MonoBehaviour
{
    [SerializeField] private Transform player;
    private PlayerMovement playerMovement;

    [Header("Check Player")]
    [SerializeField] private float distanceToPush = 2f;
    [SerializeField] private float distanceToEarthquake = 5f;
    private bool isPlayerInAttackZoneToPush;
    private bool isPlayerInAttackZoneToEarthquake;

    [Header("Push Attack")]
    [SerializeField] private float minPushXForce = 800f;
    [SerializeField] private float maxPushXForce = 1200f;
    [SerializeField] private float minPushYForce = 300f;
    [SerializeField] private float maxPushYForce = 500f;
    [SerializeField] private int damage = 25;

    [Header("Earthquake")]
    [SerializeField] private GameObject earthquake;
    public int numberOfGrounds = 7;
    public float groundYPosition = -5.5f;
    public float perionOfSpawn = 0.03f;
    private Animator myAnimator;
    private enum AttackTypes
    {
        Push,
        Earthquake
    }
    AttackTypes currentAttackType;
    void Start()
    {
        currentAttackType = AttackTypes.Earthquake;
        myAnimator = GetComponent<Animator>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanceToAttack();
        Attack();
    }

    private void CheckDistanceToAttack()
    {
        isPlayerInAttackZoneToPush = (Mathf.Abs(transform.position.x - player.position.x) < distanceToPush);
        isPlayerInAttackZoneToEarthquake = (Mathf.Abs(transform.position.x - player.position.x) < distanceToEarthquake);
    }

    private void Attack()
    {
        if (currentAttackType == AttackTypes.Push)
        {
            if (isPlayerInAttackZoneToPush)
            {
                myAnimator.SetTrigger("Push Attack");
            }
        }

        if (currentAttackType == AttackTypes.Earthquake)
        {
            if (isPlayerInAttackZoneToEarthquake)
            {
                myAnimator.SetTrigger("Earthquake Attack");
            }
        }
    }

    public void PushAttack()
    {
            float pushXForce = UnityEngine.Random.Range(minPushXForce, maxPushXForce);
            float pushYForce = UnityEngine.Random.Range(minPushYForce, maxPushYForce);
            playerMovement.GetPunch(pushXForce * Mathf.Sign(transform.localScale.x), pushYForce);
            player.gameObject.GetComponent<HealthUI>().TakeAwayHelath(damage);
    }

    public void EarthquakeAttack()
    {
        GameObject earthquakeChild = Instantiate(earthquake, transform.position, transform.rotation);
        earthquakeChild.transform.SetParent(gameObject.transform);
    }

    

    
}
