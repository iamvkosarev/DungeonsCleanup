using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArrowMovement : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    PlayerMovement player;
    Vector2 velocityDirection;
    Rigidbody2D myRigidbody;
    float direction;
    public void SetVelocityDirection(Vector2 velocityDirection)
    {
        this.velocityDirection = velocityDirection; 
    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        direction = SetDirectionOfShooting();
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = new Vector2(direction * speed, 0);
    }

    private float SetDirectionOfShooting()
    {
        if(transform.position.x <= player.transform.position.x)
        {
            return 1;
        }

        else
        {
            return -1;
        }
    }
}
