using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSoundManager : MonoBehaviour
{
    AudioSource myAudioSource;

    [Header("Attack")]
    [SerializeField] private AudioClip[] attacksClips;
    [Range(0f,2f)][SerializeField] private float attackClipAudiosBoost = 1f;
    int numOfAttackClip = 0;

    [Header("Movement Sounds")]
    [SerializeField] private AudioClip[] stepsClips;
    [Range(0f, 2f)] [SerializeField] private float stepsClipAudiosBoost = 1f;
    int numOfStepClip = 0;
    [SerializeField] private AudioClip[] jumpClips;
    [Range(0f, 2f)] [SerializeField] private float jumpClipAudiosBoost = 1f;
    int numOfJumpClip= 0;

    [Header("Health Sounds")]
    [SerializeField] private AudioClip[] getDamageClips;
    [Range(0f, 2f)] [SerializeField] private float getDamageClipAudiosBoost = 1f;
    int numOfGetDamgeClip = 0;
    [SerializeField] private AudioClip destroyBodyClip;
    [Range(0f, 2f)] [SerializeField] private float destroyBodyClipAudiosBoost = 1f;
    [SerializeField] private AudioClip deathCryClip;
    [Range(0f, 2f)] [SerializeField] private float deathCryClipAudiosBoost = 1f;

    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    
    public void PlayAttackClip()
    {
        if (attacksClips.Length != 0)
        {
            myAudioSource.PlayOneShot(attacksClips[numOfAttackClip], attackClipAudiosBoost);
            numOfAttackClip = (numOfAttackClip + 1) % attacksClips.Length;
        }
    }

    public void PlayStepClip()
    {
        if (stepsClips.Length != 0)
        {
            myAudioSource.PlayOneShot(stepsClips[numOfStepClip], stepsClipAudiosBoost);
            numOfStepClip = (numOfStepClip + 1) % stepsClips.Length;
        }
    }

    public void PlayJumpClip()
    {
        if (jumpClips.Length != 0)
        {
            myAudioSource.PlayOneShot(jumpClips[numOfJumpClip], jumpClipAudiosBoost);
            numOfJumpClip = (numOfJumpClip + 1) % jumpClips.Length;
        }
    }

    public void PlayGetDamageClip()
    {
        if (getDamageClips.Length != 0)
        {
            myAudioSource.PlayOneShot(getDamageClips[numOfGetDamgeClip], getDamageClipAudiosBoost);
            numOfGetDamgeClip = (numOfGetDamgeClip + 1) % getDamageClips.Length;
        }
    }

    public void PlayDeathSounds()
    {
        if (destroyBodyClip != null)
        {
            myAudioSource.PlayOneShot(destroyBodyClip, destroyBodyClipAudiosBoost);
        }

        if (deathCryClip != null)
        {
            myAudioSource.PlayOneShot(deathCryClip, deathCryClipAudiosBoost);
        }
    }
}
