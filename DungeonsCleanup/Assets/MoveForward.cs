using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private bool moveHorisontal = true;
    [SerializeField] private float speed;
    [SerializeField] private bool updateSpeed = false;
    [SerializeField] private bool hasStopPosition = false;
    [SerializeField] private float stopPosition;
    Rigidbody2D myRigitBody2d;
    void Start()
    {
        myRigitBody2d = GetComponent<Rigidbody2D>();
        myRigitBody2d.velocity = new Vector2(0, 0);
        AddMove(true);

    }

    private void AddMove(bool mood)
    {
        if (!mood) { return; }
        if (moveHorisontal)
        {
            myRigitBody2d.velocity = new Vector2(speed, myRigitBody2d.velocity.y);
        }
        else
        {
            myRigitBody2d.velocity = new Vector2(myRigitBody2d.velocity.x, speed);
        }
    }

    private void FixedUpdate()
    {
        AddMove(updateSpeed);
        StopOnPos();
    }

    private void StopOnPos()
    {
        if (!hasStopPosition) { return; }
        if (transform.position.y > stopPosition) { 
            updateSpeed = false;
            myRigitBody2d.velocity = new Vector2(0, 0);
        }
    }
}
