using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BatPathing : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] int batDamage = 15;
    [SerializeField] PlayerMovement player;
    [SerializeField] float attackSpeed = 200f;
    [SerializeField] float distanceToAttack = 10f;
    [SerializeField] float attackRadius = 1f;
    bool reachedEndOfPath = false;
    float nextWaypointDistance = 3f;
    List<Transform> waypoints;
    int waypointIndex;
    float startXScale;
    Path path;
    int currentWayPoint;
    Seeker seeker;
    Rigidbody2D rb;
    Animator myAnimator;

    


    private void Start()
    {
        //GetComponent + FindObjectOfType
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerMovement>();

        //For Pathfinding
        InvokeRepeating("UpdatePath", 0f, .5f);

        //For Moving
        waypoints = gameObject.GetComponentInParent<BatSpawn>().GetWaypoints();
        waypointIndex = Random.Range(0, waypoints.Count);
        transform.position = waypoints[waypointIndex].transform.position;
        startXScale = transform.localScale.x;
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(transform.position, player.transform.position, OnPathComplite);
    }


    // Update is called once per frame
    void FixedUpdate()
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

    void MoveTowardPlayer()
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
        Vector2 force = direction * attackSpeed * Time.deltaTime;

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

    void OnPathComplite(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWayPoint = 0;
        }
    }

    void Attack()
    {
       if(Mathf.Abs(player.transform.position.x - transform.position.x) < attackRadius
             && Mathf.Abs(player.transform.position.y - transform.position.y) < attackRadius)
        {
            player.GetComponent<PlayerHealth>().TakeAwayHelath(batDamage);
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
