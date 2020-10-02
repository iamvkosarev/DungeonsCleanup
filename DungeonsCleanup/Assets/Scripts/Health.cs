using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private GameObject getDamageParticle;
    [SerializeField] private float particlesDestroyDelay = 0.2f;
    [SerializeField] private Color particlesColor;
    public int health;
    public float delayBeforeDeath;
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
            GameObject particles = Instantiate(getDamageParticle, gameObject.transform.position, Quaternion.identity);
            particles.GetComponent<ParticleSystem>().startColor = particlesColor;
            Destroy(particles, particlesDestroyDelay);
        }

        CheckZeroHealth();
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
