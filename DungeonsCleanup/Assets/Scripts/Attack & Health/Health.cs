using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    
    [Header("Health && Death")]
    public int health;
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
    private int firstHealth;

    private void Start()
    {
        firstHealth = health;
    }

    public virtual void TakeAwayHelath(int damage)
    {
        if (damage >= health)
        {
            health = 0;
        }
        else
        {
            health -= damage;
            SpawnBlood();
            SpawnFloatingPoints(damage);
        }

        CheckZeroHealth();
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

    private void Death()
    {
        transform.Rotate(0, 0, -90);
        Destroy(gameObject, delayBeforeDeath);
    }
}
