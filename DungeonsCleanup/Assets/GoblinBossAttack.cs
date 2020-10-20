using System;
using UnityEngine;

public class GoblinBossAttack : MonoBehaviour
{
    [SerializeField] private Transform player;
    private PlayerMovement playerMovement;

    [Header("Check Player")]
    [SerializeField] private float distanceToPush = 3f;
    private bool isPlayerInAttackZoneToPush;

    [Header("Push Attack")]
    

    [SerializeField] private float minPushXForce = 800f;
    [SerializeField] private float maxPushXForce = 1200f;
    [SerializeField] private float minPushYForce = 300f;
    [SerializeField] private float maxPushYForce = 500f;
    [SerializeField] private int damage = 25;

    [Header("Earthquake")]
    [SerializeField] private GameObject forEarthquakeGround;
    [SerializeField] private float groundSpeed;
    private Animator myAnimator;
    enum AttackTypes
    {
        Push,
        Earthquake
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
        CheckDistanceToAttack();
        Attack();
    }

    private void CheckDistanceToAttack()
    {
        isPlayerInAttackZoneToPush = (Mathf.Abs(transform.position.x - player.position.x) < distanceToPush);
    }

    private void Attack()
    {
        if (currentAttackType == AttackTypes.Push)
        {
            if (isPlayerInAttackZoneToPush)
            {
                myAnimator.SetTrigger("Push Attack");
            }
        }

        if (currentAttackType == AttackTypes.Earthquake)
        {
            myAnimator.SetTrigger("Earthquake Attack");
        }
    }

    public void PushAttack()
    {
            float pushXForce = UnityEngine.Random.Range(minPushXForce, maxPushXForce);
            float pushYForce = UnityEngine.Random.Range(minPushYForce, maxPushYForce);
            playerMovement.GetPunch(pushXForce * Mathf.Sign(transform.localScale.x), pushYForce);
            player.gameObject.GetComponent<HealthUI>().TakeAwayHelath(damage);
    }

    
}
