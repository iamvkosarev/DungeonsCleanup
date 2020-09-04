using System;
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

    [Header("Development Settings")]
    [SerializeField] float checkRadius;
    [SerializeField] float checkRadiusForLanding;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] LayerMask whatIsWall;

    [Header("UI")]
    [SerializeField] JumpIconsScript jumpUIImage;


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
        RefreshUIJumpIcon();
        CheckPlayerFlyVelocity();
    }

    public void CheckPlayerFlyVelocity()
    {

        if (IsPlayerNearbyGround())
        {
            playerScript.StartLandAnimation();
        }

        if (playerRigidbody2D.velocity.y < 0 && !IsPlayerOnGround())
        {
            playerScript.StartFallAnimation();
        }
    }

    private void RefreshUIJumpIcon()
    {
        // Single Jump Active
        if (IsPlayerOnGround() && !canPlayerDoDoubleJump)
        {
            jumpUIImage.SetJumpIconMode(JumpIconsScript.JumpIconMode.singleJumpActive);
        }
        // Single Jump Not Active
        else if (!IsPlayerOnGround() && !canPlayerDoDoubleJump)
        {
            jumpUIImage.SetJumpIconMode(JumpIconsScript.JumpIconMode.singleJumpNotActive);
        }
        // Double Jump Active
        else if (canPlayerDoDoubleJump && IsPlayerOnGround())
        {
            jumpUIImage.SetJumpIconMode(JumpIconsScript.JumpIconMode.doubleJumpActive);
        }
        // Double Jump Half Active
        else if (canPlayerDoDoubleJump && !IsPlayerOnGround() && !isPlayerUsedSecondJump)
        {
            jumpUIImage.SetJumpIconMode(JumpIconsScript.JumpIconMode.doubleJumpHalfActive);
        }
        else if (canPlayerDoDoubleJump && !IsPlayerOnGround() && isPlayerUsedSecondJump)
        {
            jumpUIImage.SetJumpIconMode(JumpIconsScript.JumpIconMode.doubleJumpNotActive);
        }
    }

    private void RefreshJumpParametrs()
    {
        if (IsPlayerOnGround() && canPlayerDoDoubleJump)
        {
            isPlayerUsedSecondJump = false;
        }
    }

    public bool IsPlayerOnGround()
    {
        return Physics2D.OverlapCircle(transform.position, checkRadius, whatIsGround);
    }
    private bool IsPlayerNearbyGround()
    {
        return Physics2D.OverlapCircle(transform.position, checkRadiusForLanding, whatIsGround);
    }

    public void Jump()
    {
        // Single Jump
        if (IsPlayerOnGround())
        {
            // Animation
            playerScript.StartJumpingAnimation();
            // Jump
            playerRigidbody2D.velocity = Vector2.up * jumpForce;
        }
        // Double Jump
        if (canPlayerDoDoubleJump && !IsPlayerOnGround())
        {
            if (!isPlayerUsedSecondJump)
            {
                // Animation
                playerScript.DoSecondJumpAnimation();
                // Jump
                playerRigidbody2D.velocity = Vector2.up * jumpForce;
                isPlayerUsedSecondJump = true;
            }
        }
    }
}
