using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBossEarth : MonoBehaviour
{
    private PlayerMovement player;
    private GoblinBossAttack goblinBoss;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        goblinBoss = FindObjectOfType<GoblinBossAttack>().GetComponent<GoblinBossAttack>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject == player.gameObject)
        {
            player.GetPunch(Mathf.Sign(goblinBoss.gameObject.transform.localScale.x) * goblinBoss.pushXForceForEarth, goblinBoss.pushYForceForEarth);
            player.gameObject.GetComponent<HealthUI>().TakeAwayHelath(goblinBoss.earthquakeDamage);
        }
    }
}
