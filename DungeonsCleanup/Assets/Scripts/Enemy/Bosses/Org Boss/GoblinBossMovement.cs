using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBossMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float stepNoize;
    [SerializeField] private float noiseStepAmplitude;
    [SerializeField] private float noiseStepFrequency;
    private bool goToPlayer = true;
    private bool doingRotate = true;
    private bool facingRight = false;
    private float startXScale;
    private Vector2 playerPoistion;
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    private HealthUI healthUI;
    private GoblinBossAttack goblinBossAttack;
    void Start()
    {
        healthUI = GetComponent<HealthUI>();
        goblinBossAttack = GetComponent<GoblinBossAttack>();
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
        HorizontalMove();
    }
    public void MakeStepNoize()
    {
        StartCoroutine(goblinBossAttack.MakeANoise(stepNoize, noiseStepAmplitude, noiseStepFrequency));
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
        myRigidbody.bodyType = RigidbodyType2D.Static;
    }
    public void StartHorizontalMove()
    {
        goToPlayer = true;
        myRigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
    public void StopBeeingTouchible()
    {
        healthUI.healthCollider.enabled = false;
        myRigidbody.bodyType = RigidbodyType2D.Static;
        healthUI.feetCoolider.enabled = false;
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
