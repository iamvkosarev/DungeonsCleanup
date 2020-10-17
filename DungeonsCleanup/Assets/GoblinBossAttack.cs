using System;
using UnityEngine;

public class GoblinBossAttack : MonoBehaviour
{
    [SerializeField] private Transform player;
    private PlayerMovement playerMovement;

    [Header("Check Player")]
    [SerializeField] private Vector2 checkForAttackPlayerZone;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Color checkPlayerZoneColor;
    private bool isPlayerInAttackZone;

    [Header("Push Attack")]

    [SerializeField] private float minPushXForce = 800f;
    [SerializeField] private float maxPushXForce = 1200f;
    [SerializeField] private float minPushYForce = 300f;
    [SerializeField] private float maxPushYForce = 500f;
    [SerializeField] private int damage = 25;
    private Animator myAnimator;
    enum AttackTypes
    {
        Push
    }
    AttackTypes currentAttackType;
    void Start()
    {
        currentAttackType = AttackTypes.Push;
        myAnimator = GetComponent<Animator>();
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
        Attack();
    }

    private void Attack()
    {
        if (currentAttackType == AttackTypes.Push)
        {
            if (isPlayerInAttackZone)
            {
                myAnimator.SetTrigger("Push Attack");
            }
        }
    }

    private void CheckPlayer()
    {
        Collider2D hitColliders = Physics2D.OverlapBox(new Vector2(transform.position.x - checkForAttackPlayerZone.x / 2, transform.position.y), checkForAttackPlayerZone, 0, playerLayer);
        if (hitColliders)
        {
            isPlayerInAttackZone = true;
        }
        else
        {
            isPlayerInAttackZone = false;
        }
    }

    public void PushAttack()
    {
        if(isPlayerInAttackZone)
        {
            float pushXForce = UnityEngine.Random.Range(minPushXForce, maxPushXForce);
            float pushYForce = UnityEngine.Random.Range(minPushYForce, maxPushYForce);
            playerMovement.GetPunch(pushXForce, pushYForce);
            player.gameObject.GetComponent<HealthUI>().TakeAwayHelath(damage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = checkPlayerZoneColor;
        Gizmos.DrawCube(new Vector2(transform.position.x - checkForAttackPlayerZone.x / 2, transform.position.y), checkForAttackPlayerZone);

    }
}
