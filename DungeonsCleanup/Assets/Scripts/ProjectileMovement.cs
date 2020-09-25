using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    PlayerMovement player;
    int playerLayer;
    Rigidbody2D myRigidbody;
    public void SetSpeed(float speed)
    {
        this.speed = speed; 
    }
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

}
