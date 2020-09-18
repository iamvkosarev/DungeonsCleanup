using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BatPathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;
    List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] int batDamage = 15;
    int waypointIndex = 0;
    float startXScale;
    [SerializeField] Transform player;

    [SerializeField] float attackSpeed = 200f;
    [SerializeField] float distanceToAttack = 10f;
    [SerializeField] float attackRadius = 1f;
    float nextWaypointDistance = 3f;
    Path path;
    int currentWayPoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    


    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdatePath", 0f, .5f);
        waypoints = waveConfig.GetWaypoints();
        startXScale = transform.localScale.x;
        transform.position = waypoints[waypointIndex].transform.position;
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(transform.position, player.position, OnPathComplite);
    }


    // Update is called once per frame
    void FixedUpdate()
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

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWayPoint]);

        if(distance < nextWaypointDistance)
        {
            currentWayPoint++;
        }

        Attack();

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
    }

    private void Moving()
    {
        if(waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);
            if(transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }

        else
        {
            waypointIndex = 0;
        }
        if(IsFacingOnAWaypoint())
        {
            Flip();
        }  
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
