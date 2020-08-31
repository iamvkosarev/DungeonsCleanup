using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    // config
    [Header("Movement")]
    [SerializeField] float playerHorizontalSpeed = 5f;

    //catching files
    PlayerActionControls playerActionControls;
    Rigidbody2D myRigitbody2D;
    Animator myAnimator;

    // param

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }
    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    void Start()
    {
        myRigitbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        HorizontalMove();
        HorizontalRotate();
    }

    private void HorizontalRotate()
    {
        float joystickXAxis = playerActionControls.Land.Move.ReadValue<Vector2>().x;
        if (joystickXAxis == 0) { return; }
        float joystickXAxisSign = Mathf.Sign(joystickXAxis);
        transform.localScale = new Vector2(joystickXAxisSign, 1);
    }

    private void HorizontalMove()
    {
        float joystickXAxis = playerActionControls.Land.Move.ReadValue<Vector2>().x;
        float walkLimit = 0.5f;
        float runLimit = 1f;
        float joystickSign = Mathf.Sign(joystickXAxis);
        if (joystickXAxis == 0) {
            // Animation
            myAnimator.SetBool("isWalking", false);
            myAnimator.SetBool("isRunning", false);
            // Stoping Animation
            return;
        }
        else if (Mathf.Abs(joystickXAxis) <= walkLimit) // Walk
        {
            // Moving
            myRigitbody2D.velocity = new Vector2(playerHorizontalSpeed * walkLimit* joystickSign, myRigitbody2D.velocity.y);
            // Animation
            myAnimator.SetBool("isWalking", true);
            myAnimator.SetBool("isRunning", false);
        }
        else if (Mathf.Abs(joystickXAxis) > walkLimit && Mathf.Abs(joystickXAxis) <= runLimit) // Run
        {
            // Moving
            myRigitbody2D.velocity = new Vector2(playerHorizontalSpeed * runLimit* joystickSign, myRigitbody2D.velocity.y);
            // Animation
            myAnimator.SetBool("isWalking", false);
            myAnimator.SetBool("isRunning", true);
        }
    }
}
