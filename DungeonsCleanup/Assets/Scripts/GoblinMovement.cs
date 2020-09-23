using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float radiusOfMoving = 5f;
    bool movingRight = true;
    Vector3 startPos, firstDot, secondDot;
    PlayerMovement player;
    Animator myAnimator;
    Rigidbody2D myRigidbody;
    Transform myTransform;
    Health health;

    void Start()
    {
        startPos = transform.position;
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        firstDot = new Vector2(startPos.x + radiusOfMoving, transform.position.y);
        secondDot = new Vector2(startPos.x - radiusOfMoving, transform.position.y);

        

    }

    

    void Update()
    {
        //Moving();
    }

    private void Moving()
    {
        
        if(startPos.x <= firstDot.x && movingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(firstDot.x, startPos.y), moveSpeed * Time.deltaTime);
        }

        

        else if(transform.position.x > secondDot.x)
        {
            movingRight = false;
            transform.position = Vector3.MoveTowards(transform.position, new Vector2(secondDot.x, startPos.y), moveSpeed * Time.deltaTime);
            Debug.Log("im here");
        }

        else
        {
            movingRight = true;
        }
    }
    private bool IsFacingOnAHero()
    {
        if(startPos.x >= player.transform.position.x)
        {
            return Mathf.Sign(transform.localScale.x) <= 0;
        }

        else
        {
            return Mathf.Sign(transform.localScale.x) > 0;
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(transform.localScale.x)), 1f);
    }
}
