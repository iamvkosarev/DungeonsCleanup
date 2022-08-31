using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapArrowMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float gravityParam = 3f;
    [SerializeField] private float delayBeforeGravity = 1f;
    private PlayerMovement player;
    private Vector2 velocityDirection;
    private Rigidbody2D myRigidbody;
    private Vector2 direction;
    private bool setVelocity = true;
    public void SetVelocityDirection(Vector2 velocityDirection)
    {
        this.velocityDirection = velocityDirection; 
    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        StartCoroutine(GoingDown());
    }
    IEnumerator GoingDown()
    {
        yield return new WaitForSeconds(delayBeforeGravity);
        myRigidbody.gravityScale = gravityParam;
        setVelocity = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (setVelocity)
        {
            myRigidbody.velocity = new Vector2(direction.x * speed, direction.y * speed);
        }
        CheckRotation();
    }

    private void CheckRotation()
    {
        float angle = (float)Math.Atan2(-myRigidbody.velocity.y, -myRigidbody.velocity.x) * (float)(180 / Math.PI);
        gameObject.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void SetDirectionByAngle(float angle)
    {
        direction = new Vector2(Mathf.Cos(angle * Mathf.PI / 180f), Mathf.Sin(angle * Mathf.PI / 180f));
    }


}
