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
    [SerializeField] private float distanceToAttack = 2f;
    [SerializeField] private float distanceToPush = 3f;
    [SerializeField] private float distanceToEarthquake = 5f;
    private bool isPlayerInAttackZoneToAttack;
    private bool isPlayerInAttackZoneToPush;
    private bool isPlayerInAttackZoneToEarthquake;

    [Header("Simple Attack")]
    [SerializeField] private int simpleAttackDamage = 20;
    private SpawnerOfAttackingWave spawnerOfAttackingWave;

    [Header("Push Attack")]
    [SerializeField] private float minPushForce = 800f;
    [SerializeField] private float maxPushForce = 1200f;
    [SerializeField] private int pushDamage = 25;
    [SerializeField] private Transform pointToCheckPush;
    [SerializeField] private Vector2 sizeOfPushZone;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private AudioClip pushSFX;
    [SerializeField] private float audioBoosPushSFX;

    [Header("Earthquake")]
    [SerializeField] private GameObject earthquake;
    [SerializeField] public Transform earthquakeSpawnPosition;
    public int numberOfGrounds = 7;
    public float perionOfSpawn = 0.03f;
    public float timeForDestroy = 2f;
    public int earthquakeDamage = 100;
    public float pushXForceForEarth = 0;
    public float pushYForceForEarth = 100f;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private float durationOfNoise;
    [SerializeField] private float noiseAmplitude = 5f;
    [SerializeField] private float noiseFrequency = 5f;

    [SerializeField] private AudioClip earthquakeSFX;
    [SerializeField] private float audioBoosEarthquakeSFX;


    [Header("Spawn Goblins")]
    [SerializeField] private int maxNumberOfGoblins = 2;
    [SerializeField] private Vector3[] spawnPlace;
    [SerializeField] private GameObject[] goblinPreafb;
    private Animator myAnimator;
    private CinemachineBasicMultiChannelPerlin cinemachine;
    public int currentNumberOfGoblins = 1;
    private PlayerHealth playerHealth;
    private enum AttackTypes
    {
        Simple,
        Push,
        Earthquake,
        SpawnGoblins
    }
    [SerializeField] AttackTypes currentAttackType;

    private AudioSource myAudioSource;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
           cinemachine = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        spawnerOfAttackingWave = GetComponent<SpawnerOfAttackingWave>();
        myAnimator = GetComponent<Animator>();
        playerMovement = player.GetComponent<PlayerMovement>();
        movement = GetComponent<GoblinBossMovement>();
        playerHealth = player.gameObject.GetComponent<PlayerHealth>();
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
        isPlayerInAttackZoneToAttack = (Mathf.Abs(transform.position.x - player.position.x) < distanceToAttack);
        isPlayerInAttackZoneToPush = Physics2D.OverlapBox(pointToCheckPush.position, sizeOfPushZone,0,playerLayer);
        isPlayerInAttackZoneToEarthquake = (Mathf.Abs(transform.position.x - player.position.x) < distanceToEarthquake);
    }


    private void Attack()
    {
        if (currentAttackType == AttackTypes.Simple)
        {
            if (isPlayerInAttackZoneToAttack)
            {
                myAnimator.SetTrigger("Simple Attack");
            }
        }
        else if (currentAttackType == AttackTypes.Push)
        {
            if (isPlayerInAttackZoneToPush)
            {
                myAnimator.SetTrigger("Push Attack");
            }
        }

        else if(currentAttackType == AttackTypes.Earthquake)
        {
            if (isPlayerInAttackZoneToEarthquake)
            {
                currentAttackType = AttackTypes.Simple;
                myAnimator.SetTrigger("Earthquake Attack");
            }
        }
        else if (currentAttackType == AttackTypes.SpawnGoblins)
        {
            currentAttackType = AttackTypes.Simple;
            myAnimator.SetBool("Spawn Goblins", true);
        }

        //CommingOutOfTheShadow();
    }


    public void PushAttack()
    {
        RaycastHit2D[] raycastHit2Ds = Physics2D.BoxCastAll(pointToCheckPush.position, sizeOfPushZone, 0, new Vector2(1f, 0), 0, playerLayer);
        foreach (RaycastHit2D raycastHit2D in raycastHit2Ds)
        {
            if (raycastHit2D.collider.gameObject)
            {
                float pushForce = UnityEngine.Random.Range(minPushForce, maxPushForce);
                GameObject detectedObject = raycastHit2D.collider.gameObject;

                PlayerMovement playerMovement = detectedObject.GetComponent<PlayerMovement>();
                EnemiesMovement enemiesMovement = detectedObject.GetComponent<EnemiesMovement>();
                Health health = detectedObject.gameObject.GetComponent<Health>();

                Vector2 singleVecotor = new Vector2(Mathf.Sign(detectedObject.transform.position.x 
                    - transform.position.x), 0.2f) ;
                if (playerMovement)
                {
                    playerMovement.GetPunch(singleVecotor.x * pushForce, singleVecotor.y * pushForce);
                    playerHealth.TakeAwayHelath(pushDamage);
                }
                if (enemiesMovement)
                {
                    enemiesMovement.GetPunch(singleVecotor.x * pushForce, singleVecotor.y * pushForce);
                    health.TakeAwayHelath(pushDamage);
                }
            }
        }
        currentAttackType = AttackTypes.Simple;
    }

    public void EarthquakeAttack()
    {
        GameObject earthquakeChild = Instantiate(earthquake, earthquakeSpawnPosition.position, earthquakeSpawnPosition.rotation);
        earthquakeChild.GetComponent<Earthquake>().SetBoss(this);
        StartCoroutine(MakeANoise(durationOfNoise, noiseAmplitude, noiseFrequency));
        Destroy(earthquakeChild, timeForDestroy);
    }

    public void SpawnGoblins()
    {
       //gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //int randomPosition = UnityEngine.Random.Range(0, spawnPlace.Length);
        for(int i = 0; i < spawnPlace.Length; i++)
        {
            int randomGoblin = UnityEngine.Random.Range(0, goblinPreafb.Length);
            GameObject newEnemie =  Instantiate(goblinPreafb[randomGoblin], transform.position + spawnPlace[i], Quaternion.identity) as GameObject;
            newEnemie.GetComponent<EnemiesMovement>().SetTarget(player.transform.position);
        }
            
        movement.StartHorizontalMove();
        myAnimator.SetBool("Spawn Goblins", false);
    }

    private void CommingOutOfTheShadow()
    {
        if(myAnimator.GetBool("Spawn Goblins") && currentNumberOfGoblins == 0)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            movement.StartHorizontalMove();
            myAnimator.SetBool("Spawn Goblins", false);
            currentNumberOfGoblins = 1;
        }
    }
    
    private IEnumerator SetSpecialAttack()
    {
        int number;
        while(true)
        {
            yield return new WaitForSeconds(frequencyOfSpecialAttack);
            currentNumberOfGoblins = FindObjectsOfType<GoblinAnimation>().Length;
            if(currentNumberOfGoblins < maxNumberOfGoblins)
            {
                number = UnityEngine.Random.Range(1, Enum.GetNames(typeof(AttackTypes)).Length);
            }
            else
            {
                number = UnityEngine.Random.Range(1, Enum.GetNames(typeof(AttackTypes)).Length - 1);
            }
                
            currentAttackType = (AttackTypes)number;
            Debug.Log(currentAttackType);

        }
    }

    public IEnumerator MakeANoise(float timeOnNoize, float noiseAmplitude, float noiseFrequency)
    {
        cinemachine.m_AmplitudeGain = noiseAmplitude;
        cinemachine.m_FrequencyGain = noiseFrequency;
        yield return new WaitForSecondsRealtime(timeOnNoize);

        cinemachine.m_AmplitudeGain = 0;
        cinemachine.m_FrequencyGain = 0;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(pointToCheckPush.position, sizeOfPushZone);
    }

    public void SpawnEarthquakeSFX()
    {
        if (earthquakeSFX)
        {
            myAudioSource.PlayOneShot(earthquakeSFX, audioBoosEarthquakeSFX);
        }
    }

    public void SpawnRoatSFX()
    {
        if (pushSFX)
        {
            myAudioSource.PlayOneShot(pushSFX, audioBoosPushSFX);
        }
    }




}
