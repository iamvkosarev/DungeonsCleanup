using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorptionShadow : MonoBehaviour
{
    [SerializeField] private int shadowId;
    [SerializeField] private Vector2 checkPlayerSize;
    [SerializeField] private LayerMask playerLayer;
    private BoxCollider2D collider2D;
    private PlayerActivationButton playerActivationButton;
    private PlayerDevelopmentManager playerDevelopmentManager;
    private bool canBeAbsorpted = false;

    private void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        CanBeAbsorpted(false);
    }
    public void CanBeAbsorpted(bool mode)
    {
        canBeAbsorpted = mode;
        collider2D.enabled = mode;
    }

    void Update()
    {
        if (!canBeAbsorpted) { return; }
        CheckPlayer();
    }

    private void CheckPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapBox(transform.position, checkPlayerSize, 0, playerLayer);
        if (playerCollider != null)
        {
            if (playerActivationButton == null )
            {
                playerActivationButton = playerCollider.GetComponent<PlayerActivationButton>();
                playerDevelopmentManager = playerCollider.GetComponent<PlayerDevelopmentManager>();
                if (playerActivationButton)
                {
                    playerActivationButton.CanActivateAbsorption(true);
                }
                else
                {
                    playerActivationButton = null;
                    playerDevelopmentManager = null;
                }
            }
            else
            {
                if (playerActivationButton.IsReadyForActivationAbsorption())
                {
                    Debug.Log("Поглатить");
                    playerDevelopmentManager.AddShadow(shadowId);
                    playerActivationButton.CanActivateAbsorption(false);
                    CanBeAbsorpted(false);
                }
            }
        }
    }
    private void OnDestroy()
    {
        if (playerActivationButton)
        {
            playerActivationButton.CanActivateAbsorption(false);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position, checkPlayerSize);
    }
}
