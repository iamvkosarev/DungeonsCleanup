using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public enum MovementType
{
    Run,
    Walk,
    Stand
}

public class CharacterNavigatorController : MonoBehaviour
{
    [SerializeField] public float walkSpeed;
    [SerializeField] public float runSpeed;
    [SerializeField] private float stopDistance;

    public MovementType movementType = MovementType.Walk;

    public Vector3 destination;
    public bool reachedDestination;
    private Rigidbody2D rigidbody2D;
    private bool isMovementSuspended = false;
    public bool facingRight = false;
    bool canRotate = true;

    private void Awake()
    {
        destination = transform.position;
    }
    private void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isMovementSuspended) { return; }
        if(transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;
            float destinationDistance = destinationDirection.magnitude;
            if(destinationDistance >= stopDistance)
            {
                reachedDestination = false;
                RotationTowardDestination();
                float speed = movementType != MovementType.Run ? (movementType != MovementType.Walk ? 0f : walkSpeed) : runSpeed;
                Vector2 velocity = new Vector2(speed * Mathf.Sign(destinationDirection.x), rigidbody2D.velocity.y);
                rigidbody2D.velocity = velocity;
            }
            else
            {
                reachedDestination = true;
            }
        }
    }
    private void RotationTowardDestination()
    {
        float destinationXAxisDirection = Mathf.Sign(destination.x - transform.position.x);
        if (destinationXAxisDirection < 0 && facingRight)
        {
            Flip();
        }
        else if (destinationXAxisDirection > 0 && !facingRight)
        {
            Flip();
        }
    }
    private void Flip()
    {
        if (!canRotate) { return; }
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    public void SuspendMovement(int suspendMovement)
    {
        if (suspendMovement == 0)
        {
            this.isMovementSuspended = false;
        }
        else
        {
            this.isMovementSuspended = true;
        }
    }
    public void SetDestination(Vector3 destination, MovementType movementType)
    {
        this.movementType = movementType;
        this.destination = destination;
        reachedDestination = false;
    }

#if UNITY_EDITOR
    [ContextMenu("Position character")]
    public void PositionCharacter()
    {
        RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, direction: Vector3.down, 200, LayerMask.GetMask("Ground"));
        if (raycastHit2D)
        {
            Undo.RecordObject(transform, "position character");
            CapsuleCollider2D feetFromCapsule = GetComponentInChildren<CapsuleCollider2D>();
            if (feetFromCapsule)
            {
                Vector2 feetComponentPos = new Vector2(0, feetFromCapsule.offset.y);
                Vector2 feetComponentSize = new Vector2(0, feetFromCapsule.size.y);
                transform.position = raycastHit2D.point - feetComponentPos + feetComponentSize / 2 + raycastHit2D.normal * .064f;
            }
        }
    }
#endif
}
