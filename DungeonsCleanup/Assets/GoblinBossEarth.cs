using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBossEarth : MonoBehaviour
{
    private PlayerMovement player;
    private GoblinBossAttack goblinBoss;
    private Rigidbody2D myRigidbody;
    Vector2 playerPosition;


    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        goblinBoss = FindObjectOfType<GoblinBossAttack>().GetComponent<GoblinBossAttack>();
        playerPosition = player.transform.position;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == player.gameObject)
        {
            player.GetPunch(Mathf.Sign(goblinBoss.gameObject.transform.localScale.x) * goblinBoss.pushXForceForEarth, goblinBoss.pushYForceForEarth);
            player.gameObject.GetComponent<PlayerHealth>().TakeAwayHelath(goblinBoss.earthquakeDamage);
        }
    }
}
