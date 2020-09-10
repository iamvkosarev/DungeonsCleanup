using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
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
        }
    }
    public int GetHealth()
    {
        return health;
    }
}
