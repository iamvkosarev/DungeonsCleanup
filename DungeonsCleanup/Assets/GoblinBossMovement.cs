using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBossMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 5f;
    public bool shouldGoToPlayer = true;
    private float startXScale;
    private Vector2 playerPoistion;
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    void Start()
    {
        startXScale = transform.localScale.x;
        playerPoistion = player.position;
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shouldGoToPlayer)
        {
            float xScale = transform.localScale.x;
            myRigidbody.velocity = new Vector2(-Mathf.Sign(xScale) * speed, transform.position.y);
            //myAnimator.SetBool("isWalking", true);
        }
        
        if(IsNotFacingOnAHero())
        {
            Flip();
        }
    }

    private bool IsNotFacingOnAHero()
    {
        Vector2 startPos = transform.position;
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
        transform.localScale = new Vector2(-(Mathf.Sign(transform.localScale.x)) * Mathf.Abs(startXScale), transform.localScale.y);
    }
}
