﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    
    [SerializeField] float destroyDalay;
    [SerializeField] float moveSpeed;
    [SerializeField] float lastPointSpeed;
    [SerializeField] LayerMask playerLayer;

    Rigidbody2D myRigidbody2D;
    Animator myAnimator;
    SpriteRenderer mySpriteRenderer;
    BoxCollider2D myBoxCollider2D;
    private Transform[] motionCoordinates;
    private int lastMotionCoordinatesIndex;
    private bool hasMoveStoped;
    private bool isGoingToLastPoint;
    private float lastPointDistant;

    private void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        myRigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        myBoxCollider2D.enabled = false;
    }
    private void FixedUpdate()
    {
        MovingToPoints();
    }

    private void MovingToPoints()
    {
        if (hasMoveStoped) { return; }
        if (motionCoordinates == null) { return; }

        float step;
        if (motionCoordinates.Length - 1 == lastMotionCoordinatesIndex)
        {
            if (!isGoingToLastPoint)
            {
                lastPointDistant = Mathf.Sqrt(Mathf.Pow(transform.position.x - motionCoordinates[lastMotionCoordinatesIndex].position.x, 2) +
                Mathf.Pow(transform.position.y - motionCoordinates[lastMotionCoordinatesIndex].position.y, 2));
                isGoingToLastPoint = true;
                if(lastPointSpeed < moveSpeed)
                {
                    lastPointSpeed = moveSpeed;
                }
            }
            float transmissionСoefficient = Mathf.Sqrt(Mathf.Pow(transform.position.x - motionCoordinates[lastMotionCoordinatesIndex].position.x, 2) +
                Mathf.Pow(transform.position.y - motionCoordinates[lastMotionCoordinatesIndex].position.y, 2)) / lastPointDistant;
            step = (lastPointSpeed - transmissionСoefficient * (lastPointSpeed - moveSpeed)) * Time.deltaTime;
        }
        else
        {
            step = Time.deltaTime * moveSpeed;
        }
        transform.position = Vector3.MoveTowards(transform.position, motionCoordinates[lastMotionCoordinatesIndex].position, step);

        if (transform.position == motionCoordinates[motionCoordinates.Length - 1].position)
        {
            hasMoveStoped = true;
            StartFalling();
        }
        if (transform.position == motionCoordinates[lastMotionCoordinatesIndex].position)
        {
            lastMotionCoordinatesIndex++;
        }
    }

    private void StartFalling()
    {
        myRigidbody2D.bodyType = RigidbodyType2D.Dynamic;
        myBoxCollider2D.enabled = true;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public void SetCoordinates(Transform[] coordinates)
    {
        motionCoordinates = coordinates;
        lastMotionCoordinatesIndex = 1;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Sound
        if (collision.gameObject.layer == playerLayer)
        {
            Destroy(gameObject, destroyDalay);
        }
        myBoxCollider2D.isTrigger = false;
        myAnimator.SetTrigger("Drop");
    }
    public void Destroy()
    {
        mySpriteRenderer.sprite = null;
        Destroy(gameObject, destroyDalay);
    }
}
