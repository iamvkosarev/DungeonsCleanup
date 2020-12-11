using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MovementType
{
    Run,
    Walk,
    Stand
}

public class CharacterNavigatorController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float stopDistance;

    MovementType movementType = MovementType.Walk;

    public Vector3 destination;
    public bool reachedDestination;
    private Rigidbody2D rigidbody2D;

    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;
            float destinationDistance = destinationDirection.magnitude;
            if(destinationDistance >= stopDistance)
            {
                reachedDestination = false;
                // Rotation
                float speed = movementType != MovementType.Run ? (movementType != MovementType.Walk ? 0f : walkSpeed) : runSpeed;
                Vector2 velocity = new Vector2(speed * Mathf.Sign(destinationDirection.x), 0f);
                rigidbody2D.velocity = velocity;
            }
            else
            {
                reachedDestination = true;
            }
        }
    }

    public void SetDestination(Vector3 destination, MovementType movementType)
    {
        this.movementType = movementType;
        this.destination = destination;
        reachedDestination = false;
    }

}
