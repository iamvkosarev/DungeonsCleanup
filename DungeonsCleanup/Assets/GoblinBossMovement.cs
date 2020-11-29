using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBossMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 5f;
    private bool goToPlayer = true;
    private bool doingRotate = true;
    private bool facingRight = false;
    private float startXScale;
    private Vector2 playerPoistion;
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    public bool sittingOnAThrone = true;
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
        BossRotation();
        
    }

    private void FixedUpdate()
    {
        if(!sittingOnAThrone)
            HorizontalMove();
    }
    
    public void StandUp()
    {
        sittingOnAThrone = false;
    }
    
    #region Movement
    private void HorizontalMove()
    {
        if (goToPlayer)
        {
            myRigidbody.velocity = new Vector2(-Mathf.Sign(transform.rotation.y) * speed, myRigidbody.velocity.y);
            //myAnimator.SetBool("isWalking", true);
        }
    }
    public void StopHorizontalMove()
    {
        goToPlayer = false;
    }
    public void StartHorizontalMove()
    {
        goToPlayer = true;
    }
    #endregion
    #region Rotation
    private void BossRotation()
    {
        if (!doingRotate) { return; }
        if (IsNotFacingOnAHero() > 0 && facingRight)
        {
            Flip();
        }
        else if (IsNotFacingOnAHero() < 0 && !facingRight)
        {
            Flip();
        }
    }
    private void StopRotation()
    {
        doingRotate = false;
    }
    private void StartRotation()
    {
        doingRotate = true;
    }
    private float IsNotFacingOnAHero()
    {
        return transform.position.x - player.transform.position.x;
    }

    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    #endregion
}
