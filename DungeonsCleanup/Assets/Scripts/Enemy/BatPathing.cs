using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BatPathing : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float towardSpeed = 200f;
    [Header("Attack")]
    [SerializeField] private int batDamage = 15;
    [SerializeField] private float attackRadius = 1f;
    private PlayerMovement player;
    private float distanceToAttack;
    private bool reachedEndOfPath = false;
    private float nextWaypointDistance = 3f;
    private List<Transform> waypoints;
    private int waypointIndex;
    private float startXScale;
    private int currentWayPoint;
    private Rigidbody2D rb;
    private Animator myAnimator;
    private Health myHealth;
    private Rigidbody2D myRigidBody;

    private void Start()
    {
        //GetComponent + FindObjectOfType
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();
        distanceToAttack = GetComponentInParent<BatSpawn>().GetDistanceToAttack();
        myHealth = GetComponent<Health>();
        myRigidBody = GetComponent<Rigidbody2D>();

        //For Moving
        waypoints = gameObject.GetComponentInParent<BatSpawn>().GetWaypoints();
        waypointIndex = Random.Range(0, waypoints.Count);
        transform.position = waypoints[waypointIndex].transform.position;
        startXScale = transform.localScale.x;
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        //Moving();

        if(Mathf.Abs(player.transform.position.x - transform.position.x) < distanceToAttack
             && Mathf.Abs(player.transform.position.y - transform.position.y) < distanceToAttack)  
        {
            MoveTowardPlayer();
        }

        else
        {
            Moving();
        }
    }

    private void Death()
    {
        if(myHealth.health == 0)
        {
            myAnimator.SetTrigger("Death");
            myRigidBody.gravityScale = 1;
            Destroy(gameObject, 2f);
        }
    }
    private void Moving()
    {
        var targetPosition = waypoints[waypointIndex].transform.position;
        var movementThisFrame = moveSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

        if(transform.position == targetPosition)
        {
            waypointIndex = Random.Range(0, waypoints.Count);
        }
        

        if(IsFacingOnAWaypoint())
            Flip();
    }

    private void MoveTowardPlayer()
    {
        transform.position = Vector3.MoveTowards
            (transform.position, player.transform.position, towardSpeed * Time.deltaTime);


        if(Mathf.Abs(player.transform.position.x - transform.position.x) < attackRadius
            && Mathf.Abs(player.transform.position.y - transform.position.y) < attackRadius)
        {
            myAnimator.SetBool("Attack", true);
        }
        
        //MoveTowardPlayer();
    }

    private void Attack()
    {
       if(Mathf.Abs(player.transform.position.x - transform.position.x) < attackRadius
             && Mathf.Abs(player.transform.position.y - transform.position.y) < attackRadius)
        {
            player.GetComponent<PlayerHealth>().TakeAwayHelath(batDamage);
        }
        myAnimator.SetBool("Attack", false);

        if(IsFacingOnAHero())
            Flip();
    }

    

    private void Flip()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(transform.localScale.x)) * startXScale, transform.localScale.y);
    }

    private bool IsFacingOnAWaypoint()
    {
        if(transform.position.x >= waypoints[waypointIndex].transform.position.x)
        {
            return Mathf.Sign(transform.localScale.x) <= 0;
        }
        else
        {
            return Mathf.Sign(transform.localScale.x) >= 0;
        }
    }

    private bool IsFacingOnAHero()
    {
        Vector2 startPos = transform.position;
        if(startPos.x >= player.transform.position.x)
        {
            return Mathf.Sign(transform.localScale.x) <= 0;
        }

        else
        {
            return Mathf.Sign(transform.localScale.x) > 0;
        }
    }
}
