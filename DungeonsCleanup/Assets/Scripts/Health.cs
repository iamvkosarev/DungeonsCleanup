using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health;

    public virtual void TakeAwayHelath(int damage)
    {
        if (damage >= health)
        {
            health = 0;
        }
        else
        {
            health -= damage;
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
        gameObject.transform.Rotate(0, 0, -90);
        Destroy(gameObject, 2f);

        // we should change this Death animation
    }
}
