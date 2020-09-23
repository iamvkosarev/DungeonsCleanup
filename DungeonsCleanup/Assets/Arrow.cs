using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] int arrowDamage = 25;
    PlayerMovement player;
    int playerLayer;
    Rigidbody2D myRigidbody;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        playerLayer = player.gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        myRigidbody.velocity = new Vector2(-speed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == playerLayer)
        {
            other.gameObject.GetComponentInParent<PlayerHealth>().TakeAwayHelath(arrowDamage);
        }
        
        Destroy(gameObject);
    }
}
