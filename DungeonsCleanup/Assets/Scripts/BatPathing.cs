using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatPathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;
    List<Transform> waypoints;
    [SerializeField] float moveSpeed = 2f;
    int waypointIndex = 0;
    float startXScale;

    private void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        startXScale = transform.localScale.x;
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }

    private void Move()
    {
        if(waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = moveSpeed * Time.deltaTime;
            if(IsFacingOnAWaypoint())
            {
                Flip();
            }
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
            return Mathf.Sign(transform.localScale.x) > 0;
        }
    }
}
