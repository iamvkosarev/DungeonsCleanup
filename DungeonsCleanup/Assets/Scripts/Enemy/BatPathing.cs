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
    private Path path;
    private int currentWayPoint;
    private Seeker seeker;
    private Rigidbody2D rb;
    private Animator myAnimator;

    private void Start()
    {
        //GetComponent + FindObjectOfType
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();
        distanceToAttack = GetComponentInParent<BatSpawn>().GetDistanceToAttack();

        //For Pathfinding
        InvokeRepeating("UpdatePath", 0f, .5f);

        //For Moving
        waypoints = gameObject.GetComponentInParent<BatSpawn>().GetWaypoints();
        waypointIndex = Random.Range(0, waypoints.Count);
        transform.position = waypoints[waypointIndex].transform.position;
        startXScale = transform.localScale.x;
    }

    private void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(transform.position, player.transform.position, OnPathComplite);
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        // Moving();

        if(Mathf.Abs(player.transform.position.x - transform.position.x) < distanceToAttack
             && Mathf.Abs(player.transform.position.y - transform.position.y) < distanceToAttack
             && Mathf.Abs(player.transform.position.x - transform.position.x) > 0.3f
             && Mathf.Abs(player.transform.position.y - transform.position.y) > 0.3f)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            myAnimator.SetBool("Attack", true);
            //MoveTowardPlayer();
        }

        else
        {
            Moving();
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
        if(path == null)
            return;

        if(currentWayPoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }

        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - rb.position).normalized;
        Vector2 force = direction * towardSpeed * Time.deltaTime;

        rb.velocity = force;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }

        if(IsFacingOnAHero())
        {
            Flip();
        }
    }

    private void OnPathComplite(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    private void Attack()
    {
       if(Mathf.Abs(player.transform.position.x - transform.position.x) < attackRadius
             && Mathf.Abs(player.transform.position.y - transform.position.y) < attackRadius)
        {
            player.GetComponent<HealthUI>().TakeAwayHelath(batDamage);
        }

        if(IsFacingOnAHero())
            Flip();

        myAnimator.SetBool("Attack", false);
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
