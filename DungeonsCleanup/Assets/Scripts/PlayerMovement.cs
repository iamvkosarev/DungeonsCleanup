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
    [SerializeField] float horizontalMovingDelay;
    private bool areHorizontalMovingSuspended= false;

    [Header("Ground Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask stairsLayer;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] GameObject hazePrefab;
    [SerializeField] float groundJumpsDelay;
    private bool isStandingOnGround;
    private bool isStandingOnStairs;
    private bool canJump;
    private bool areGroundJumpsSuspended = false;

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
    [SerializeField] float wallJumpsDelay;
    private bool areWallJumpsSuspended = false;


    [Header("Player Elements")]
    [SerializeField] GameObject bodyChild;

    [Header("VFX")]
    [SerializeField] GameObject runParticlesPrefab;
    [SerializeField] float particlesDestroyDelay = 0.1f;

    //catching files
    PlayerActionControls playerActionControls;
    Rigidbody2D myRigidbody2D;
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
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myAttackManager = GetComponent<PlayerAttackManager>();
    }
    void Update()
    {
        Inputs();
        CheckTouching();
        CheckAttacking();

        UpdateColliderInBody();
        StopingMoving();
    }

    private void CheckAttacking()
    {
        if (isAttackButtonPressed)
        {
            areHorizontalMovingSuspended = true;
            isStoping = true;
        }
        else
        {
            areHorizontalMovingSuspended = false;
        }
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
        if (isStandingOnStairs)
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x / slowingOnStairsParametr, myRigidbody2D.velocity.y);
            if (myRigidbody2D.velocity.x == 0f && !areGroundJumpsSuspended)
            {
                
                myRigidbody2D.gravityScale = 0f;
                myRigidbody2D.velocity = new Vector2(0f, 0f);

            }
            else
            {
                myRigidbody2D.gravityScale = 1f;
            }
        }
        else
        {
            myRigidbody2D.gravityScale = 1f;
        }
    }

    private void Inputs()
    {
        Vector2 joystickVector = playerActionControls.Land.Move.ReadValue<Vector2>();
        joystickXAxis = joystickVector.x;
        joystickYAxis = joystickVector.y;
        canJump = (joystickYAxis >= movingJoystickProperties.GetJumpLimit()) ? true : false;
    }

    public bool IsAttackButtonPressed()
    {
        return isAttackButtonPressed;
    }
    private void WallSlide()
    {
        if (isTouchingWall && !(isStandingOnGround || isStandingOnStairs) && myRigidbody2D.velocity.y < 0f)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, -wallSlideSpeed);
        }
    }

    private void WallJump()
    {
        if ((isWallSliding || isTouchingWall) && canJump && !areWallJumpsSuspended)
        {
            //myRigitbody2D.AddForce(new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.x), ForceMode2D.Impulse );
            myRigidbody2D.velocity = new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.x);
            canJump = false;
            StartCoroutine(SuspendWallJumps());
            StartCoroutine(SuspendHorizontalMoving());
        }
    }

    private void Jump()
    {
        if (canJump && (isStandingOnGround || isStandingOnStairs) && !areGroundJumpsSuspended)
        {
            SpawnHaze();
            myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x, jumpForce);
            canJump = false;
            StartCoroutine(SuspendGroundJumps());
        }
    }
    IEnumerator SuspendGroundJumps()
    {
        areGroundJumpsSuspended = true;
        yield return new WaitForSeconds(groundJumpsDelay);
        areGroundJumpsSuspended = false;
    }
    IEnumerator SuspendWallJumps()
    {
        areWallJumpsSuspended = true;
        yield return new WaitForSeconds(wallJumpsDelay);
        areWallJumpsSuspended = false;
    }

    private void UpdateColliderInBody()
    {
        Destroy(bodyChild.GetComponent<PolygonCollider2D>());
        bodyChild.AddComponent<PolygonCollider2D>();
    }
    IEnumerator SuspendHorizontalMoving()
    {
        areHorizontalMovingSuspended = true;
        yield return new WaitForSeconds(horizontalMovingDelay);
        areHorizontalMovingSuspended = false;

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

        if (areHorizontalMovingSuspended) { return; }

        if (absJpystickXAxis < movingJoystickProperties.GetWalkLimit() && !myAnimator.GetBool("IsAttacking")) {
            isStoping = true;
            return;
        }
        else if (absJpystickXAxis >= movingJoystickProperties.GetWalkLimit() 
            && absJpystickXAxis < movingJoystickProperties.GetRunLimit()) // Walk
        {
            myRigidbody2D.velocity = new Vector2(playerHorizontalSpeed * absJpystickXAxis * joystickXAxisSign, myRigidbody2D.velocity.y);
        }
        else if (absJpystickXAxis >= movingJoystickProperties.GetRunLimit()) // Run
        {
            myRigidbody2D.velocity = new Vector2(playerHorizontalSpeed * (movingJoystickProperties.GetRunLimit() + 0.1f) * joystickXAxisSign, myRigidbody2D.velocity.y);

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
        if (Mathf.Abs(myRigidbody2D.velocity.x) > movingJoystickProperties.GetWalkLimit() * playerHorizontalSpeed && !isStoping)
        {
            timeSinceStartStoping = Time.time;
            startVelocityXAxis = myRigidbody2D.velocity.x;
        }

        if (isStoping && Time.time - timeSinceStartStoping <= timeOnStoping)
        {
            float coefficientOfStoping = (Time.time - timeSinceStartStoping) / timeOnStoping;
            myRigidbody2D.velocity = new Vector2(startVelocityXAxis * (1 - coefficientOfStoping), myRigidbody2D.velocity.y);
        }
        else if (isStoping && Time.time - timeSinceStartStoping > timeOnStoping)
        {
            myRigidbody2D.velocity = new Vector2(0, myRigidbody2D.velocity.y);
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
        startVelocityXAxis = myRigidbody2D.velocity.x;
        float playerDirection = Mathf.Sign(transform.localScale.x);
        if (!myAnimator.GetBool("IsOnGround"))
        {
            attackJerkForce *= 3f;
        }
        myRigidbody2D.velocity = new Vector2(myRigidbody2D.velocity.x / 1.4f + attackJerkForce * playerDirection, myRigidbody2D.velocity.y);
    }
    public void SpawnHaze()
    {
        GameObject haze = Instantiate(hazePrefab, transform.position + hazePrefab.transform.position, Quaternion.identity);
        Haze hazeScript = haze.GetComponent<Haze>();
        Collider2D stairsCollider = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, stairsLayer);
        if (stairsCollider == null) { return; }
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
