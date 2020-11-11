﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    
    [Header("Health && Death")]
    public int health;
    public BoxCollider2D healthCollider;
    public Collider2D feetCoolider;
    public int deathAnimationsNum = 0;
    public float timeBefroGoingUnderGround = 3f;
    public float speedOnGoingUnderGround = 0.6f;
    public float timeBeforeDestroyAfterGoingUnderGround = 3f;
    public float delayBeforeDeath;
    
    [Header("Damage Particles")]
    [SerializeField] private GameObject getDamageParticle;
    [SerializeField] private float particlesDestroyDelay = 0.2f;
    [SerializeField] private Color particlesColor;

    [Header("Floating Points")]
    [SerializeField] private GameObject floatingPointsPrefab;
    [SerializeField] private float floatingPointSpeed;
    [SerializeField] private float maxAngleFloatingPointDirection;
    [SerializeField] private float floatingPointDestroyDelay = 2f;

    [Header("SFX")]
    [SerializeField] private AudioClip getHitSFX;
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] private float audioBoost;
    private AudioSource myAudioSource;
    private GoblinBossAttack goblinBoss;

    private int firstHealth;
    private EnemiesMovement enemiesMovement;
    private Rigidbody2D myRB;
    Animator animator;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myRB = GetComponent<Rigidbody2D>();
        firstHealth = health;
        animator = GetComponent<Animator>();
        goblinBoss = FindObjectOfType<GoblinBossAttack>();
    }

    public virtual void TakeAwayHelath(int damage)
    {
        if (damage >= health)
        {
            health = 0;
        }
        else
        {
            FlipIfDontKnowWhereAttackFrom();
            SpawnGetHitSFX();
            health -= damage;
        }

        SpawnBlood();
        SpawnFloatingPoints(damage);
        CheckZeroHealth();
    }

    private void FlipIfDontKnowWhereAttackFrom()
    {
        if (!enemiesMovement)
        {
            enemiesMovement = GetComponent<EnemiesMovement>();
        }
        if (enemiesMovement)
        {
            enemiesMovement.RotateOnHit();
        }
    }

    private void SpawnFloatingPoints(int damage)
    {
        GameObject floatingPoint = Instantiate(floatingPointsPrefab, transform.position, Quaternion.identity);
        float randomAngle = UnityEngine.Random.RandomRange(-maxAngleFloatingPointDirection, maxAngleFloatingPointDirection);
        Vector2 direction = new Vector2(Mathf.Sin(randomAngle/90f*Mathf.PI), Mathf.Cos(randomAngle / 90f * Mathf.PI));
        TextMeshPro textMesh = floatingPoint.GetComponent<TextMeshPro>();
        textMesh.text = damage.ToString();
        textMesh.color = particlesColor;
        floatingPoint.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x * floatingPointSpeed, direction.y * floatingPointSpeed);
        Destroy(floatingPoint, floatingPointDestroyDelay);
    }

    private void SpawnBlood()
    {
        GameObject particles = Instantiate(getDamageParticle, gameObject.transform.position, Quaternion.identity);
        particles.GetComponent<ParticleSystem>().startColor = particlesColor;
        Destroy(particles, particlesDestroyDelay);
    }

    public virtual void CheckZeroHealth()
    {
        if (health <= 0)
        {
            Debug.Log($"GameObject \"{gameObject.name}\" has destroyed");
            Death();
        }
    }
    public int GetHealth()
    {
        return health;
    }

    public void SetVisibilityOfEnemies(bool mode)
    {
        if(healthCollider == null) { return; }
        healthCollider.enabled = mode;
    }
    private void Death()
    {
        SpawnDeathSFX();
        DeathAnimaton();
        AttackTag attackTag = GetComponent<AttackTag>();
        if (attackTag)
        {
            attackTag.DestroyAttackTag();
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    public void StartGoingUnderGround()
    {
        SpawnExp();
        StartCoroutine(GoingUnderGround());
    }
    IEnumerator GoingUnderGround()
    {
        yield return new WaitForSeconds(timeBefroGoingUnderGround);
        myRB.velocity = Vector2.down * speedOnGoingUnderGround;
        yield return new WaitForSeconds(timeBeforeDestroyAfterGoingUnderGround);
        Destroy();
    }
    public void SwitchOffFeetCollider()
    {
        if(feetCoolider == null) {return;}
        feetCoolider.enabled = false;
    }
    private void DeathAnimaton()
    {
        if (deathAnimationsNum == 0)
        {
            Destroy(gameObject, delayBeforeDeath);
            return;
        }
        SetVisibilityOfEnemies(false);
        SwitchOffFeetCollider();
        myRB.bodyType = RigidbodyType2D.Kinematic;
        myRB.velocity = new Vector2(0, 0);
        int randomNumOfAnimation = UnityEngine.Random.Range(1, deathAnimationsNum+1);
        animator.Play($"Death_{randomNumOfAnimation}");
    }

    private void SpawnExp()
    {
        SpawExperience spawExperience = GetComponent<SpawExperience>();
        if (spawExperience)
        {
            spawExperience.SpawnExp();
        }
    }

    #region SFX
    private void SpawnGetHitSFX()
    {
        if (getHitSFX)
        {
            myAudioSource.PlayOneShot(getHitSFX, audioBoost);
        }
    }
    private void SpawnDeathSFX()
    {
        if (deathSFX)
        {
            GameObject deathGO = new GameObject();
            deathGO.transform.position = transform.position;
            AudioSource deathAudioSource = deathGO.AddComponent<AudioSource>();
            deathAudioSource.PlayOneShot(deathSFX, audioBoost);
            Destroy(deathGO, deathSFX.length);
        }
    }
    #endregion

    private void OnDestroy()
    {
        if(!goblinBoss) { return; }
        goblinBoss.currentNumberOfGoblins--;
    }
}
