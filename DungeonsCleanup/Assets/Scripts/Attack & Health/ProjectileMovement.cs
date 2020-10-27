﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    PlayerMovement player;
    Vector2 velocityDirection;
    int playerLayer;
    Rigidbody2D myRigidbody;
    public void SetVelocityDirection(Vector2 velocityDirection)
    {
        this.velocityDirection = velocityDirection; 
    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        playerLayer = player.gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = new Vector2(velocityDirection.x * speed, velocityDirection.y * speed);
    }

}