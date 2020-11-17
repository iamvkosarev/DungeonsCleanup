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
    private PlatformEffector2D myPlatformEffector2D;
    private PlayerActivationButton playerActivationButton;

    private void Start()
    {
        myPlatformEffector2D = GetComponent<PlatformEffector2D>();
        CloseBody();
        
    }

    private void Update()
    {
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapBox(checkPlayerPoint.position, checkPlayerSize, 0, playerLayer);
        if (playerCollider != null)
        {
            if (playerActivationButton == null)
            {
                Debug.Log("Player Movement detected");
                playerActivationButton = playerCollider.GetComponent<PlayerActivationButton>();
                playerActivationButton.CanActivateHatch(true);
            }
            else
            {
                if (playerActivationButton.IsReadyForActivation())
                {
                    OpenBody();
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
            CloseBody();
        }
    }
    private void OpenBody()
    {
        openBody.SetActive(true);
        closeBody.SetActive(false);
        myPlatformEffector2D.rotationalOffset = 180f;
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
    
}
