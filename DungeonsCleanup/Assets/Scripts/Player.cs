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
    [SerializeField] float timeOnStoping = 0.3f;

    [Header("Player Elements")]
    [SerializeField] GameObject bodyChild;
    [SerializeField] GameObject feetChild;

    [Header("VFX")]
    //[SerializeField] GameObject runVFX;

    //catching files
    PlayerActionControls playerActionControls;
    Rigidbody2D myRigitbody2D;
    Animator myAnimator;

    // param
    bool isAttackButtonPressed;
    bool isJumpButtonPressed;
    bool isStoping;
    float timeSinceStartStoping;
    float startVelocityXAxis;
    float walkLimit = 0.2f;
    float runLimit = 0.7f;

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        // Attack
        playerActionControls.Land.Attack.started += _ => isAttackButtonPressed = true;
        playerActionControls.Land.Attack.canceled += _ => isAttackButtonPressed = false;
        // Moving
        playerActionControls.Land.Move.started += _ => isStoping = false;
        playerActionControls.Land.Move.canceled += _ => isStoping = true;
        // Jump
        playerActionControls.Land.Jump.started += _ => Jump();
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
        UpdateColliderInBody();
        StopMoving();
    }


    private void Jump()
    {
        feetChild.GetComponent<JumpScript>().Jump();
    }

    private void UpdateColliderInBody()
    {
        Destroy(bodyChild.GetComponent<PolygonCollider2D>());
        bodyChild.AddComponent<PolygonCollider2D>();
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
        float joystickXAxisSign = Mathf.Sign(joystickXAxis);
        float absJpystickXAxis = Mathf.Abs(joystickXAxis);


        if (absJpystickXAxis < walkLimit) {
            // Animation
            myAnimator.SetBool("isWalking", false);
            myAnimator.SetBool("isRunning", false);
            // Stoping Animation
            return;
        }
        else if (absJpystickXAxis >= walkLimit && absJpystickXAxis < runLimit) // Walk
        {
            // Moving
            myRigitbody2D.velocity = new Vector2(playerHorizontalSpeed * (walkLimit + 0.2f) * joystickXAxisSign, myRigitbody2D.velocity.y);
            // Animation
            myAnimator.SetBool("isWalking", true);
            myAnimator.SetBool("isRunning", false);
        }
        else if (absJpystickXAxis >= runLimit) // Run
        {
            // Moving
            myRigitbody2D.velocity = new Vector2(playerHorizontalSpeed * (runLimit + 0.1f) * joystickXAxisSign, myRigitbody2D.velocity.y);
            // Animation
            myAnimator.SetBool("isWalking", false);
            myAnimator.SetBool("isRunning", true);

            //GameObject explosion = Instantiate(runVFX, transform.position, transform.rotation);
            //Destroy(explosion, 1f);
        }
    }

    private void StopMoving()
    {
        if (Mathf.Abs(myRigitbody2D.velocity.x ) > walkLimit * playerHorizontalSpeed && !isStoping)
        {
            timeSinceStartStoping = Time.time;
            startVelocityXAxis = myRigitbody2D.velocity.x;
        }

        if (isStoping && Time.time - timeSinceStartStoping <= timeOnStoping)
        {
            float coefficientOfStoping = (Time.time - timeSinceStartStoping) / timeOnStoping;
            myRigitbody2D.velocity = new Vector2(startVelocityXAxis * (1 - coefficientOfStoping), myRigitbody2D.velocity.y);
        }
        else if(isStoping && Time.time - timeSinceStartStoping > timeOnStoping)
        {
            myRigitbody2D.velocity = new Vector2(0, myRigitbody2D.velocity.y);
        }
    }

    public void StopStoppingAnimation()
    {
        myAnimator.SetTrigger("StopStopping");
    }
}
