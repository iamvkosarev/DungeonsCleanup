﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpScript : MonoBehaviour
{

    // config
    [Header("Game Settings")]
    [SerializeField] bool canPlayerDoDoubleJump = false;
    [SerializeField] float jumpForce;
    [SerializeField] float stopingOnStairsParametr = 2f;

    [Header("Development Settings")]
    [SerializeField] float checkRadius;
    [SerializeField] float checkRadiusForLanding;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] LayerMask whatIsStairs;
    [SerializeField] LayerMask whatIsWall;


    [Header("VFX")]
    [SerializeField] GameObject hazePrefab;


    //catching files
    Rigidbody2D playerRigidbody2D;
    Player playerScript;

    // param
    bool isPlayerUsedSecondJump;
    private void Start()
    {
        playerRigidbody2D = GetComponentInParent<Rigidbody2D>();
        playerScript = GetComponentInParent<Player>();
    }

    private void Update()
    {
        RefreshJumpParametrs();
        CheckPlayerFlyVelocity();
        CheckPlayerOnStairs();
    }

    private void CheckPlayerOnStairs()
    {
        if (IsPlayerOnStairs())
        {
            playerRigidbody2D.velocity = new Vector2(playerRigidbody2D.velocity.x / stopingOnStairsParametr, playerRigidbody2D.velocity.y);
            playerRigidbody2D.gravityScale = 0f;
        }
        else
        {
            playerRigidbody2D.gravityScale = 1f;
        }
    }

    public void CheckPlayerFlyVelocity()
    {

        if (IsPlayerNearbyGround())
        {
            playerScript.StartLandAnimation();
        }

        if (playerRigidbody2D.velocity.y < 0 && !IsPlayerOnGroundOrStairs())
        {
            playerScript.StartFallAnimation();
        }
    }


    private void RefreshJumpParametrs()
    {
        if (IsPlayerOnGroundOrStairs() && canPlayerDoDoubleJump)
        {
            isPlayerUsedSecondJump = false;
        }
    }

    public bool IsPlayerOnGroundOrStairs()
    {
        return Physics2D.OverlapCircle(transform.position, checkRadius, whatIsGround) ||
            Physics2D.OverlapCircle(transform.position, checkRadius, whatIsStairs);
    }
    public Collider2D IsPlayerOnStairs()
    {
        return Physics2D.OverlapCircle(transform.position, checkRadius, whatIsStairs);
    }
    private bool IsPlayerNearbyGround()
    {
        return Physics2D.OverlapCircle(transform.position, checkRadiusForLanding, whatIsGround);
    }


    public void SpawnHaze()
    {
        GameObject haze = Instantiate(hazePrefab, transform.position, Quaternion.identity);
        Haze hazeScript = haze.GetComponent<Haze>();
        Collider2D stairsCheckCollider = IsPlayerOnStairs();
        if (stairsCheckCollider)
        {
            if (stairsCheckCollider.gameObject.tag == "RightStairs")
            {
                hazeScript.RotateHaze();
            }
            hazeScript.SetHazeOnStairs();
        }
        hazeScript.StartAnimation();
    }
    public void Jump()
    {
        // Single Jump
        if (IsPlayerOnGroundOrStairs())
        {
            // Animation
            playerScript.StartJumpingAnimation();
            // Jump
            SpawnHaze();
            playerRigidbody2D.velocity = Vector2.up * jumpForce;
        }
        // Double Jump
        if (canPlayerDoDoubleJump && !IsPlayerOnGroundOrStairs())
        {
            if (!isPlayerUsedSecondJump)
            {
                // Animation
                playerScript.DoSecondJumpAnimation();
                // Jump
                SpawnHaze();
                playerRigidbody2D.velocity = Vector2.up * jumpForce;
                isPlayerUsedSecondJump = true;
            }
        }
    }
}
