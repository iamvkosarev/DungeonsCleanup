using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    // config
    [Header("Characteristics")]
    public int maxHealth = 100;
    int currentHealth;

    [Header("Movement")]
    [SerializeField] float playerHorizontalSpeed = 5f;
    [SerializeField] float timeOnStoping = 0.3f;

    [Header("Jump Activation ")]
    [SerializeField] float jumpJoystickStartLevel = 0.7f;
    [SerializeField] float delayBeforeNextJump = 1f;

    [Header("Player Elements")]
    [SerializeField] GameObject bodyChild;
    [SerializeField] GameObject feetChild;
    [SerializeField] HealthBar healthBar;

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
    bool isJumpButtonPressed;
    bool isStoping;
    bool canPlayerJump = true;
    bool isPlayerOnGround;
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
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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
        UpdatePlayerGroundInfo();
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
        if (joystickYAxis >= jumpJoystickStartLevel && canPlayerJump)
        { 
            feetChild.GetComponent<JumpScript>().Jump();
            StartCoroutine(WaitingJump());
            TakeDamage(15); // for test!
        }
    }

    IEnumerator WaitingJump()
    {
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


        if (absJpystickXAxis < walkLimit && !myAnimator.GetBool("isAttacking")) {
            // Animation
            myAnimator.SetBool("isWalking", false);
            myAnimator.SetBool("isRunning", false);
            isStoping = true;
            return;
        }
        else if (myAnimator.GetBool("isAttacking"))
        {
            myAnimator.SetBool("isWalking", false);
            myAnimator.SetBool("isRunning", false);
        }
        else if (absJpystickXAxis >= walkLimit && absJpystickXAxis < runLimit) // Walk
        {
            // Moving
            myRigitbody2D.velocity = new Vector2(playerHorizontalSpeed * absJpystickXAxis * joystickXAxisSign, myRigitbody2D.velocity.y);
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

        }
        isStoping = false;
    }

    private void StopingMoving()
    {
        if (!myAnimator.GetBool("isPlayerOnGround")) { return; }
            if (Mathf.Abs(myRigitbody2D.velocity.x) > walkLimit * playerHorizontalSpeed && !isStoping)
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
        if (!myAnimator.GetBool("isPlayerOnGround"))
        {
            attackJerkForce *= 3f;
        }
        myRigitbody2D.velocity = new Vector2(myRigitbody2D.velocity.x / 1.4f + attackJerkForce * playerDirection, playerVertVelocity);
    }
    // Animation

    private void UpdatePlayerGroundInfo()
    {
        isPlayerOnGround = feetsJumpingScript.IsPlayerOnGround();
        myAnimator.SetBool("isPlayerOnGround", isPlayerOnGround);
    }
    public void StartJumpingAnimation()
    {
        myAnimator.SetBool("isWalking", false);
        myAnimator.SetBool("isRunning", false);
        myAnimator.SetBool("isStopping", false);
        myAnimator.SetBool("isJumping", true);
    }

    public void SetJumpingFalseInAnimation()
    {
        myAnimator.SetBool("isJumping", false);
    }

    public void StartFallAnimation()
    {
        myAnimator.SetBool("isWalking", false);
        myAnimator.SetBool("isRunning", false);
        myAnimator.SetBool("isStopping", false);
        myAnimator.SetBool("isFalling", true);
    }

    public void StartLandAnimation()
    {
        myAnimator.SetBool("isFalling", false);
    }

    public void DoSecondJumpAnimation()
    {
        myAnimator.SetBool("isJumping", true);
    }

    private void TakeDamage(int damage) // for test
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

}
