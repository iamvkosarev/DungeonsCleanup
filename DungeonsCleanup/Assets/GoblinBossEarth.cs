using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBossEarth : MonoBehaviour
{
    private PlayerMovement player;

    private void Start()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == player)
        {
            Debug.Log("hi!");
        }
    }
}
