using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] MovingJoystickProperties movingJoystickProperties;

    [Header("For Horizontal Movement")]
    [SerializeField] float playerHorizontalSpeed = 5f;
    [SerializeField] float timeOnStoping = 0.3f;
    [SerializeField] float slowingOnStairsParametr = 2f;

    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask stairsLayer;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] GameObject hazePrefab;
    private bool isStandingOnGround;
    private bool isStandingOnStairs;
    private bool canJump;

    [Header("Slide")]
    [SerializeField] float wallSlideSpeed = 0;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Vector2 wallCheckSize;
    private bool isTouchingWall;
    private bool isWallSliding;

    [Header("WallJump")]
    [SerializeField] float wallJumpForce = 18f;
    [SerializeField] float wallJumpDirection = -1f;
    [SerializeField] Vector2 wallJumpAngle;


    [Header("Player Elements")]
    [SerializeField] GameObject bodyChild;

    [Header("VFX")]
    [SerializeField] GameObject runParticlesPrefab;
    [SerializeField] float particlesDestroyDelay = 0.1f;

    //catching files
    PlayerActionControls playerActionControls;
    Rigidbody2D myRigitbody2D;
    Animator myAnimator;
    PlayerAttackManager myAttackManager;

    // param
    bool facingRight = true;
    bool isAttackButtonPressed;
    bool isStoping;
    float timeSinceStartStoping;
    float startVelocityXAxis;
    float joystickXAxis;
    float joystickYAxis;

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
    }
    void Update()
    {
        Inputs();
        CheckTouching();

        UpdateColliderInBody();
        StopingMoving();
        Attack();
    }

    private void CheckTouching()
    {
        isStandingOnGround = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
        isStandingOnStairs = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, stairsLayer);
        isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0, wallLayer);
    }
    public bool IsPlyerStanding()
    {
        return isStandingOnGround || isStandingOnStairs;
    }

    void FixedUpdate()
    {
        HorizontalMove();
        Jump();
        WallSlide();
        WallJump();
        SlowingOnStairs();
    }

    private void SlowingOnStairs()
    {
        myRigitbody2D.gravityScale = 1;
        if (isStandingOnStairs)
        {
            myRigitbody2D.velocity = new Vector2(myRigitbody2D.velocity.x / slowingOnStairsParametr, myRigitbody2D.velocity.y);
            if (myRigitbody2D.velocity.x == 0)
            {
                myRigitbody2D.gravityScale = 0;
            }
        }
    }

    private void Inputs()
    {
        Vector2 joystickVector = playerActionControls.Land.Move.ReadValue<Vector2>();
        joystickXAxis = joystickVector.x;
        joystickYAxis = joystickVector.y;
        canJump = (joystickYAxis >= movingJoystickProperties.GetJumpLimit()) ? true : false;
    }

    private void Attack()
    {
        if (isAttackButtonPressed)
        {
            myAttackManager.StartStabbingAttackAnimation();
        }
    }
    private void WallSlide()
    {
        if (isTouchingWall && !(isStandingOnGround || isStandingOnStairs) && myRigitbody2D.velocity.y < 0f)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            myRigitbody2D.velocity = new Vector2(myRigitbody2D.velocity.x, -wallSlideSpeed);
        }
    }

    private void WallJump()
    {
        if ((isWallSliding || isTouchingWall) && canJump)
        {
            //myRigitbody2D.AddForce(new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.x), ForceMode2D.Impulse );
            myRigitbody2D.velocity = new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.x);
            canJump = false;
        }
    }

    private void Jump()
    {
        if (canJump && (isStandingOnGround || isStandingOnStairs))
        {
            SpawnHaze();
            myRigitbody2D.velocity = new Vector2(myRigitbody2D.velocity.x, jumpForce);
            canJump = false;
        }
    }


    private void UpdateColliderInBody()
    {
        Destroy(bodyChild.GetComponent<PolygonCollider2D>());
        bodyChild.AddComponent<PolygonCollider2D>();
    }


    private void HorizontalMove()
    {
        float joystickXAxisSign = Mathf.Sign(joystickXAxis);
        float absJpystickXAxis = Mathf.Abs(joystickXAxis);

        if (joystickXAxis < 0 && facingRight)
        {
            Flip();
        }
        else if (joystickXAxis > 0 && !facingRight)
        {
            Flip();
        }

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

    private void Flip()
    {
        wallJumpDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
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
        GameObject particles = Instantiate(runParticlesPrefab, transform.position + runParticlesPrefab.transform.position, Quaternion.Euler(0, angleOfRotate, 0));
        Destroy(particles, particlesDestroyDelay);
    }

    public void Jerk(float attackJerkForce)
    {
        timeSinceStartStoping = Time.time;
        startVelocityXAxis = myRigitbody2D.velocity.x;
        float playerDirection = Mathf.Sign(transform.localScale.x);
        if (!myAnimator.GetBool("IsOnGround"))
        {
            attackJerkForce *= 3f;
        }
        myRigitbody2D.velocity = new Vector2(myRigitbody2D.velocity.x / 1.4f + attackJerkForce * playerDirection, myRigitbody2D.velocity.y);
    }
    public void SpawnHaze()
    {
        GameObject haze = Instantiate(hazePrefab, transform.position + hazePrefab.transform.position, Quaternion.identity);
        Haze hazeScript = haze.GetComponent<Haze>();
        Collider2D stairsCollider = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, stairsLayer);
        if (isStandingOnStairs)
        {
            if (stairsCollider.gameObject.tag == "RightStairs")
            {
                hazeScript.RotateHaze();
            }
            hazeScript.SetHazeOnStairs();
        }
        hazeScript.StartAnimation();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);

        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);
    }
}
