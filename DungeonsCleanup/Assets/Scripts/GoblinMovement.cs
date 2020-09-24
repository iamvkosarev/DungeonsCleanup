using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    private Vector3 startPos;
    private PlayerMovement player;
    private Animator myAnimator;
    private Rigidbody2D myRigidbody;
    private Transform myTransform;
    private Health health;
    private PlayerDetector detector;
    private bool movingRight = true;

    void Start()
    {
        startPos = transform.position;
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        detector = gameObject.GetComponent<PlayerDetector>();     

    }

    void Update()
    {
        if(detector.GetResultOfDetected() && !detector.GetResultOfAttacking())
        {
            MoveTowardPlayer();
            return;
        }

        BackToStartPlace();
    }

    private void MoveTowardPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        // if(!IsFacingOnAHero())
        //     Flip();
    }

    private void BackToStartPlace()
    {
        if(transform.position.x != startPos.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
            // if(IsFacingOnAHero())
            //     Flip();
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
