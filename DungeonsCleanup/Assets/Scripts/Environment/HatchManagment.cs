using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatchManagment : MonoBehaviour
{
    [SerializeField] private float timeOnWait = 1f;
    [SerializeField] private float playerOperatingThreshold;
    [SerializeField] private Transform checkPlayerPoint;
    [SerializeField] private Vector2 checkPlayerSize;
    [SerializeField] private Vector2 checkPlayerInWitchSideSize;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Color checkPlayerColor;
    [SerializeField] private Color checkPlayerInWitchSideColor;
    [SerializeField] private GameObject openBody;
    [SerializeField] private GameObject closeBody;
    [SerializeField] private AudioClip hatchSFX;
    [SerializeField] private float audioBoost =1f;
    private bool wasOpened;
    private AudioSource myAudioSource;
    private PlatformEffector2D myPlatformEffector2D;
    private PlayerActivationButton playerActivationButton;

    private void Start()
    {
        myAudioSource = GetComponentInChildren<AudioSource>();
        myPlatformEffector2D = GetComponent<PlatformEffector2D>();
        CloseBody();
        
    }

    private void FixedUpdate()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapBox(checkPlayerPoint.position, checkPlayerSize, 0, playerLayer);
        Collider2D playerColliderInWitchSide = Physics2D.OverlapBox(new Vector2(checkPlayerPoint.position.x, checkPlayerPoint.position.y + checkPlayerSize.y / 2f - checkPlayerInWitchSideSize.y / 2f), checkPlayerInWitchSideSize, 0, playerLayer);
        if (playerCollider != null)
        {
            if (playerActivationButton == null)
            {
                playerActivationButton = playerCollider.GetComponent<PlayerActivationButton>();
                if (playerActivationButton)
                {

                    playerActivationButton.CanActivateHatch(true);
                }
                else
                {
                    playerActivationButton = null;
                }
            }
            else
            {
                if (playerActivationButton.IsReadyForActivationHatch())
                {
                    if (!wasOpened)
                    {
                        wasOpened = true;
                        PlaySFX();
                        OpenBody();
                    }
                }
                else if (!playerColliderInWitchSide)
                {
                    if (!wasOpened)
                    {
                        wasOpened = true;
                        PlaySFX();
                        OpenBody(true);
                    }
                }
                else
                {
                    CloseBody();
                }
            }
        }
        else
        {
            if (playerActivationButton != null)
            {
                playerActivationButton.CanActivateHatch(false);
                playerActivationButton = null;
            }
            if (wasOpened)
            {
                wasOpened = false;
                PlaySFX();
            }
            CloseBody();
        }
    }
    private void OpenBody(bool fromBottom = false)
    {
        if (fromBottom)
        {
            myPlatformEffector2D.rotationalOffset = 0f;

        }
        else
        {

            myPlatformEffector2D.rotationalOffset = 180f;
        }
        openBody.SetActive(true);
        closeBody.SetActive(false);
    }
    private void CloseBody()
    {
        openBody.SetActive(false);
        closeBody.SetActive(true);
        myPlatformEffector2D.rotationalOffset = 0f;
    }
    private bool IsPlayerHigher()
    {
        Collider2D playerCollider = Physics2D.OverlapBox(new Vector2(checkPlayerPoint.position.x, checkPlayerPoint.position.y + checkPlayerSize.y / 2f - checkPlayerInWitchSideSize.y / 2f), checkPlayerInWitchSideSize, 0, playerLayer);
        if (playerCollider != null)
        {
            return true;
        }
        return false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = checkPlayerColor;
        Gizmos.DrawCube(checkPlayerPoint.position, checkPlayerSize);

        Gizmos.color = checkPlayerInWitchSideColor;
        Gizmos.DrawCube(new Vector2(checkPlayerPoint.position.x, checkPlayerPoint.position.y + checkPlayerSize.y/2f - checkPlayerInWitchSideSize.y/2f), checkPlayerInWitchSideSize);
    }
    private void PlaySFX()
    {
        myAudioSource.clip = hatchSFX;
        myAudioSource.volume = audioBoost;
        myAudioSource.Play();
    }
}
