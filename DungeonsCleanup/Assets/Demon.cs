using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    Vector3 startPos;
    Player player;
    Rigidbody2D myRigidbody;
    Transform myTransform;
    [SerializeField] float speed = 10f;
    [SerializeField] float distanceToAttack = 15f;
    Vector3 direction;

    void Awake()
    {
        startPos = gameObject.transform.position;
        player = FindObjectOfType<Player>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(player.transform.position.x - transform.position.x) < distanceToAttack)
        {
            MoveTowardPlayer();
            return;
        }
        BackToStartPlace();
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
}
