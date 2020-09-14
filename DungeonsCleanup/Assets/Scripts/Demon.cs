using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    Vector3 startPos;
    Player player;
    Rigidbody2D myRigidbody;
    Transform myTransform;
    [SerializeField] float speed = 3f;
    [SerializeField] float distanceToAttack = 10f;
    [SerializeField] float radiusOfAttack = 1f;
    [SerializeField] int demonDamage = 1;
    [SerializeField] float delayBeforeAttack = 1f;
    [SerializeField] LayerMask playerMask;
    Vector3 direction;
    Animator myAnimator;
    Health health;

    void Awake()
    {
        health = GetComponent<Health>();
        startPos = gameObject.transform.position;
        player = FindObjectOfType<Player>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(health.health > 0)
        {
            if(Mathf.Abs(player.transform.position.x - transform.position.x) < radiusOfAttack)
            {
                myAnimator.SetBool("Attack", true);
                return;
            }
            else if(Mathf.Abs(player.transform.position.x - transform.position.x) < distanceToAttack)
            {
                MoveTowardPlayer();
                return;
            }
            BackToStartPlace();

        }
    }

    private void MoveTowardPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if(!IsFacingOnAHero())
            Flip();
    }

    private void BackToStartPlace()
    {
        if(transform.position.x != startPos.x)
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
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
        if(Mathf.Abs(player.transform.position.x - transform.position.x) < radiusOfAttack)
        {
            player.gameObject.GetComponent<PlayerHealth>().TakeAwayHelath(demonDamage);
            myAnimator.SetBool("Attack", false);

        }
    }
}
