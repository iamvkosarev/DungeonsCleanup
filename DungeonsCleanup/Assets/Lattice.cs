using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lattice : MonoBehaviour
{
    [SerializeField] private Transform activationPoint;
    [SerializeField] private Vector2 activationZone;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private bool openLatticeOnBossDeath = false;
    [SerializeField] private HealthUI bossHealth;
    [SerializeField] private AudioClip moveSFX;
    [SerializeField] private float audioBoost = 0.8f;
    private AudioSource audioSource;
    private bool wasOpened = true;
    private bool checkActivationZone = true;
    private Animator myAnimator;
    private bool wasPlayedFirst;
    private bool wasPlayedSecond;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckActivationZone();
        ChechBossDeath();
    }

    private void ChechBossDeath()
    {
        if(!openLatticeOnBossDeath) { return; }
        if(bossHealth.health == 0 && !wasOpened)
        {
            wasOpened = true;
            PlaySFX(2);
            wasPlayedSecond = true;
            myAnimator.Play("Opening Animation");
            checkActivationZone = false;
        }
    }
    private void PlaySFX(int num)
    {
        
        if (wasPlayedSecond && num == 2)
        {
            return;
        }
        if (wasPlayedFirst && num == 1)
        {
            return;
        }

        if (moveSFX)
        {
            audioSource.PlayOneShot(moveSFX, audioBoost);
        }
    }
    private void CheckActivationZone()
    {
        if (!checkActivationZone) { return; } 
        Collider2D collider2D = Physics2D.OverlapBox(activationPoint.position, activationZone, 0, playerLayer);
        if (collider2D)
        {
            PlaySFX(1);
            wasPlayedFirst = true;
            myAnimator.Play("Closing Animation");
            wasOpened = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(activationPoint.position, activationZone);
    }
}
