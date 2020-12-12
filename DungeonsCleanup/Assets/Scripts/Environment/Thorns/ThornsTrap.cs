﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsTrap : MonoBehaviour
{
    [Header("Frequency")]
    [SerializeField] private float attackingTime = 1f;
    [SerializeField] private float doesntAttackingTime = 2f;
    [SerializeField]private int damage;
    [SerializeField] private AudioClip attackSFX;
    [SerializeField] private float audioBoost = 1f;
    private Animator myAnimator;
    private AudioSource myAudioSource;
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        myAnimator = GetComponent<Animator>();
        StartCoroutine(Attacking());
    }
    
    private IEnumerator Attacking()
    {
        while(true)
        {
            SwitchColliderValue(false);
            myAnimator.Play("ThornsDoesntAttack");
            yield return new WaitForSeconds(doesntAttackingTime);

            SwitchColliderValue(true);
            myAnimator.Play("ThornsAttack");
            myAudioSource.PlayOneShot(attackSFX, audioBoost);
            yield return new WaitForSeconds(attackingTime);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerHealth>())
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeAwayHelath(damage);
        }
    }

    private void SwitchColliderValue(bool value)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = value;
    }
}