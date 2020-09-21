using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    Vector3 startPos;
    PlayerMovement player;
    Rigidbody2D myRigidbody;
    Transform myTransform;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float distanceToAttack = 10f;
    [SerializeField] float attackRadius = 1f;
    [SerializeField] int demonDamage = 10;
    [SerializeField] float delayBeforeAttack = 1f;
    [SerializeField] LayerMask playerMask;
    Vector3 direction;
    Animator myAnimator;
    Health health;

    void Awake()
    {
        health = GetComponent<Health>();
        startPos = gameObject.transform.position;
        player = FindObjectOfType<PlayerMovement>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.health > 0)
        {
            if(Mathf.Abs(player.transform.position.x - transform.position.x) < attackRadius
                 && Mathf.Abs(player.transform.position.y - transform.position.y) < 1)
            {
                myAnimator.SetBool("Attack", true);
                return;
            }
            else if(Mathf.Abs(player.transform.position.x - transform.position.x) < distanceToAttack
                    && Mathf.Abs(player.transform.position.y - transform.position.y) < 1)
            {
                MoveTowardPlayer();
                return;
            }

            BackToStartPlace();

        }
    }

    private void MoveTowardPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        if(!IsFacingOnAHero())
            Flip();
    }

    private void BackToStartPlace()
    {
        if(transform.position.x != startPos.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
            if(IsFacingOnAHero())
                Flip();
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

    public void Attack()
    {
        if(Mathf.Abs(player.transform.position.x - transform.position.x) < attackRadius
                && Mathf.Abs(player.transform.position.y - transform.position.y) < 1)
        {
            player.gameObject.GetComponent<PlayerHealth>().TakeAwayHelath(demonDamage);

        }
        myAnimator.SetBool("Attack", false);
    }
}
