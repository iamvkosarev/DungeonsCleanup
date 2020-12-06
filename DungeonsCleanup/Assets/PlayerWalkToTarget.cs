using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerWalkToTarget : MonoBehaviour
{
    [SerializeField] private Transform pointToGo;
    private PlayerMovement playerMovement;
    public event EventHandler OnReadyForTalk;
    private float playerSpeed;
    private Rigidbody2D myRigidbody2D;
    private Animator myAnimator;
    private PlayerAnimation playerAnimation;
    private bool isStoped = false;
    public void StartWalk()
    {
        playerMovement = GetComponent<PlayerMovement>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        myAnimator = GetComponent<Animator>();
        playerMovement.enabled = false;
        playerAnimation.enabled = false;
        myAnimator.Play("Run");

        playerSpeed = playerMovement.runSpeed;
        myRigidbody2D.velocity = new Vector2(playerSpeed, 0);
    }

    private void Update()
    {
        if (pointToGo.position.x < transform.position.x)
        {
            playerMovement.enabled = true;
            myRigidbody2D.velocity = new Vector2(0, 0);
            playerAnimation.enabled = true;
            if (OnReadyForTalk != null)
            {
                OnReadyForTalk.Invoke(this, EventArgs.Empty);
            }
            Destroy(pointToGo.gameObject);
            Destroy(GetComponent<PlayerWalkToTarget>());
            return;
        }
    }

}
