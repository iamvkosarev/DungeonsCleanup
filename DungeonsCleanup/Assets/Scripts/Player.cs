﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] MovingJoystickProperties movingJoystickProperties;
    [SerializeField] float playerHorizontalSpeed = 5f;
    [SerializeField] float timeOnStoping = 0.3f;

    [Header("Jump Activation ")]
    [SerializeField] float delayBeforeNextJump = 1f;

    [Header("Player Elements")]
    [SerializeField] GameObject bodyChild;
    [SerializeField] GameObject feetChild;

    [Header("VFX")]
    [SerializeField] GameObject runParticlesPrefab;
    [SerializeField] float particlesDestroyDelay = 0.1f;

    //catching files
    PlayerActionControls playerActionControls;
    Rigidbody2D myRigitbody2D;
    Animator myAnimator;
    PlayerAttackManager myAttackManager;
    JumpScript feetsJumpingScript;

    // param
    bool isAttackButtonPressed;
    bool isStoping;
    bool canPlayerJump = true;
    bool isPlayerOnGround;
    float timeSinceStartStoping;
    float startVelocityXAxis;

    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        // Attack
        playerActionControls.Land.Attack.started += _ => isAttackButtonPressed = true;
        playerActionControls.Land.Attack.canceled += _ => isAttackButtonPressed = false;
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
        //currentHealth = maxHealth;
        myRigitbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAttackManager = GetComponent<PlayerAttackManager>();
        feetsJumpingScript = feetChild.GetComponent<JumpScript>();
    }

    void Update()
    {
        HorizontalMove();
        HorizontalRotate();
        Jump();
        UpdateColliderInBody();
        StopingMoving();
        Attack();
    }


    private void Attack()
    {
        if (isAttackButtonPressed)
        {
            myAttackManager.StartStabbingAttackAnimation();
        }
    }

    private void Jump()
    {
        float joystickYAxis = playerActionControls.Land.Move.ReadValue<Vector2>().y;
        if (joystickYAxis >= movingJoystickProperties.GetJumpLimit() && canPlayerJump)
        { 
            StartCoroutine(WaitingJump());
            //TakeDamage(15); // for test!
        }
    }

    IEnumerator WaitingJump()
    {
        feetChild.GetComponent<JumpScript>().Jump();
        canPlayerJump = false;
        yield return new WaitForSeconds(delayBeforeNextJump);
        canPlayerJump = true;
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


        if (absJpystickXAxis < movingJoystickProperties.GetWalkLimit() && !myAnimator.GetBool("IsAttacking")) {
            isStoping = true;
            return;
        }
        else if (absJpystickXAxis >= movingJoystickProperties.GetWalkLimit() 
            && absJpystickXAxis < movingJoystickProperties.GetRunLimit()) // Walk
        {
            myRigitbody2D.velocity = new Vector2(playerHorizontalSpeed * absJpystickXAxis * joystickXAxisSign, myRigitbody2D.velocity.y);
        }
        else if (absJpystickXAxis >= movingJoystickProperties.GetRunLimit()) // Run
        {
            myRigitbody2D.velocity = new Vector2(playerHorizontalSpeed * (movingJoystickProperties.GetRunLimit() + 0.1f) * joystickXAxisSign, myRigitbody2D.velocity.y);

        }
        isStoping = false;
    }

    private void StopingMoving()
    {
        if (!myAnimator.GetBool("IsOnGround")) { return; }
            if (Mathf.Abs(myRigitbody2D.velocity.x) > movingJoystickProperties.GetWalkLimit() * playerHorizontalSpeed && !isStoping)
        {
            timeSinceStartStoping = Time.time;
            startVelocityXAxis = myRigitbody2D.velocity.x;
        }

        if (isStoping && Time.time - timeSinceStartStoping <= timeOnStoping)
        {
            float coefficientOfStoping = (Time.time - timeSinceStartStoping) / timeOnStoping;
            myRigitbody2D.velocity = new Vector2(startVelocityXAxis * (1 - coefficientOfStoping), myRigitbody2D.velocity.y);
        }
        else if (isStoping && Time.time - timeSinceStartStoping > timeOnStoping)
        {
            myRigitbody2D.velocity = new Vector2(0, myRigitbody2D.velocity.y);
        }
    }
    public PlayerActionControls GetActionControls()
    {
        return playerActionControls;
    }
    public void SpawnRunParticlesVFX(int isDirectionInSameSide)
    {
        int angleOfRotate;
        if (transform.localScale.x >= 0)
        {
            angleOfRotate = isDirectionInSameSide == 1? 0 : 180;
        }
        else
        {
            angleOfRotate = isDirectionInSameSide == 1? 180 : 180;
        }
        GameObject particles = Instantiate(runParticlesPrefab, feetChild.transform.position, Quaternion.Euler(0, angleOfRotate, 0));
        Destroy(particles, particlesDestroyDelay);
    }

    public void Jerk(float attackJerkForce)
    {
        timeSinceStartStoping = Time.time;
        startVelocityXAxis = myRigitbody2D.velocity.x;
        float playerDirection = Mathf.Sign(transform.localScale.x);
        float playerVertVelocity = myRigitbody2D.velocity.y;
        if (!myAnimator.GetBool("IsOnGround"))
        {
            attackJerkForce *= 3f;
        }
        myRigitbody2D.velocity = new Vector2(myRigitbody2D.velocity.x / 1.4f + attackJerkForce * playerDirection, playerVertVelocity);
    }

}
