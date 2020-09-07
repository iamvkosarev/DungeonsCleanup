using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : MonoBehaviour
{
    Vector3 startPos;
    Player player;
    Rigidbody2D myRigidbody;
    [SerializeField] float speed = 10f;
    [SerializeField] float distanceToAttack = 15f;
    Vector3 direction;

    void Awake()
    {
        startPos = gameObject.transform.position;
        player = FindObjectOfType<Player>();
        myRigidbody = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        direction =  - transform.position;
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
    }

    private void BackToStartPlace()
    {
        if(transform.position.x != startPos.x)
            transform.position = Vector3.MoveTowards(transform.position, startPos, speed * Time.deltaTime);
    }
}
