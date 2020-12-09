using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.InputSystem.OnScreen;
using UnityEngine.Events;
using System;

public class GamepadManager : MonoBehaviour
{
    [Header("Manage Player")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private bool isDesktopWork;
    private bool switchOffButton;


    [Header("Manage Buttons")]
    [SerializeField] private GameObject activateAbilityButtons;
    private PlayerDevelopmentManager playerDevelopmentManager;
    
    private void Start()
    {
        playerDevelopmentManager = playerMovement.gameObject.GetComponent<PlayerDevelopmentManager>();
        playerDevelopmentManager.OnSettingNewItem += SwitchActivateButton;
    }
    private void SwitchActivateButton(object sender, PlayerDevelopmentManager.OnSettingNewItemAsArtifactEventArgs e)
    {
        activateAbilityButtons.SetActive(e.canItemBeActivated);
    }

    private void FixedUpdate()
    {
       switchOffButton = Input.touchCount == 0 && !isDesktopWork;
    }
}