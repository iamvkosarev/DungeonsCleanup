using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    [Header("Simple Attack")]
    [SerializeField] private int simpleAttackDamage = 20;

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
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private float durationOfNoise;
    [SerializeField] private float noiseAmplitude = 5f;
    [SerializeField] private float noiseFrequency = 5f;
    [Header("Spawn Goblins")]
    [SerializeField] private int numberOfGoblins = 5;
    [SerializeField] private Vector2[] spawnPlace;
    [SerializeField] private GameObject[] goblinPreafb;
    private Animator myAnimator;
    public int currentNumberOfGoblins = 1;

    private enum AttackTypes
    {
        Simple,
        Push,
        Earthquake,
        SpawnGoblins
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
    private void Update()
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
            if (isPlayerInAttackZoneToPush)
            {
                movement.shouldGoToPlayer = false;
                myAnimator.SetTrigger("Simple Attack");
            }
        }

        if (currentAttackType == AttackTypes.Push)
        {
            if (isPlayerInAttackZoneToPush)
            {
                movement.shouldGoToPlayer = false;
                myAnimator.SetTrigger("Push Attack");
            }
        }

        if (currentAttackType == AttackTypes.Earthquake)
        {
            if (isPlayerInAttackZoneToEarthquake)
            {
                currentAttackType = AttackTypes.Simple;
                movement.shouldGoToPlayer = false;
                myAnimator.SetTrigger("Earthquake Attack");
            }
        }
        if (currentAttackType == AttackTypes.SpawnGoblins)
        {
            currentAttackType = AttackTypes.Simple;
            movement.shouldGoToPlayer = false;
            myAnimator.SetBool("Spawn Goblins", true);
        }

        CommingOutOfTheShadow();
    }

    public void SimpleAttack()
    {
        if (isPlayerInAttackZoneToPush)
        {
            player.gameObject.GetComponent<PlayerHealth>().TakeAwayHelath(simpleAttackDamage);
        }
        movement.shouldGoToPlayer = true;
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
        currentAttackType = AttackTypes.Simple;
        movement.shouldGoToPlayer = true;
    }

    public void EarthquakeAttack()
    {
        GameObject earthquakeChild = Instantiate(earthquake, transform.position, transform.rotation);
        StartCoroutine(MakeANoise());
        Destroy(earthquakeChild, timeForDestroy);
    }

    public void SpawnGoblins()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
            for (int i = 0; i < numberOfGoblins; i++)
            {
               int randomPosition = UnityEngine.Random.Range(0, spawnPlace.Length);
               int randomGoblin = UnityEngine.Random.Range(0, goblinPreafb.Length);
               Instantiate(goblinPreafb[randomGoblin], new Vector2(spawnPlace[randomPosition].x, spawnPlace[randomPosition].y), Quaternion.identity);
            }
        

        currentNumberOfGoblins = FindObjectsOfType<GoblinAnimation>().Length;
    }

    private void CommingOutOfTheShadow()
    {
        if(myAnimator.GetBool("Spawn Goblins") && currentNumberOfGoblins == 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            movement.shouldGoToPlayer = true;
            myAnimator.SetBool("Spawn Goblins", false);
            currentNumberOfGoblins = 1;
        }
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

    private IEnumerator MakeANoise()
    {
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = noiseAmplitude;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = noiseFrequency;
        yield return new WaitForSecondsRealtime(durationOfNoise);

        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
    }





    

    
}
