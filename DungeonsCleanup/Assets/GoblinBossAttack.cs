using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBossAttack : MonoBehaviour
{
    [SerializeField] private Transform player;
    private PlayerMovement playerMovement;
    private GoblinBossMovement movement;
    [SerializeField] float frequencyOfSpecialAttack = 10f;

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
    [SerializeField] private int pushDamage = 25;

    [Header("Earthquake")]
    [SerializeField] private GameObject earthquake;
    public int numberOfGrounds = 7;
    public float groundYPosition = -5.5f;
    public float perionOfSpawn = 0.03f;
    public float timeForDestroy = 2f;
    public int earthquakeDamage = 100;
    public float pushXForceForEarth = 0;
    public float pushYForceForEarth = 100f;
    private Animator myAnimator;
    private enum AttackTypes
    {
        Simple,
        Push,
        Earthquake
    }
    [SerializeField] AttackTypes currentAttackType;

    private void Start()
    {
        myAnimator = GetComponent<Animator>();
        playerMovement = player.GetComponent<PlayerMovement>();
        movement = GetComponent<GoblinBossMovement>();
        
        StartCoroutine(SetSpecialAttack());
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
        if (currentAttackType == AttackTypes.Simple)
        {

        }
        if (currentAttackType == AttackTypes.Push)
        {
            if (isPlayerInAttackZoneToPush)
            {
                movement.shouldGoToPlayer = false;
                myAnimator.SetTrigger("Push Attack");
                currentAttackType = AttackTypes.Simple;
            }
        }

        if (currentAttackType == AttackTypes.Earthquake)
        {
            if (isPlayerInAttackZoneToEarthquake)
            {
                movement.shouldGoToPlayer = false;
                myAnimator.SetTrigger("Earthquake Attack");
                currentAttackType = AttackTypes.Simple;
            }
        }
    }

    public void PushAttack()
    {
        if (isPlayerInAttackZoneToPush)
        {
            float pushXForce = UnityEngine.Random.Range(minPushXForce, maxPushXForce);
            float pushYForce = UnityEngine.Random.Range(minPushYForce, maxPushYForce);
            playerMovement.GetPunch(pushXForce * Mathf.Sign(transform.localScale.x), pushYForce);
            player.gameObject.GetComponent<PlayerHealth>().TakeAwayHelath(pushDamage);
            
        }
        movement.shouldGoToPlayer = true;
    }

    public void EarthquakeAttack()
    {
        GameObject earthquakeChild = Instantiate(earthquake, transform.position, transform.rotation);
        earthquakeChild.transform.SetParent(gameObject.transform);
        Destroy(earthquakeChild, timeForDestroy);
    }
    
    private IEnumerator SetSpecialAttack()
    {
        while(true)
        {
            yield return new WaitForSeconds(frequencyOfSpecialAttack);
            int number = UnityEngine.Random.Range(1, Enum.GetNames(typeof(AttackTypes)).Length);
            currentAttackType = (AttackTypes)number;
            Debug.Log(currentAttackType);

        }
    }



    

    
}
