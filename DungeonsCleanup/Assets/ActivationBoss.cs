using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationBoss : MonoBehaviour
{
    [SerializeField] private GoblinBossMovement goblinBoss;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            goblinBoss.GetComponent<Animator>().SetTrigger("Start");
            Destroy(gameObject);
        }
    }
}
