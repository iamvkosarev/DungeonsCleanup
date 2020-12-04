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
    private float horizontalMoveData = 0f;
    private bool horizontalData;
    private bool jumpData;
    private bool attackData;


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
    public void ManageEvent(string buttonName, bool pressedParam)
    {
        if (buttonName == "moveButton_left")
        {
            if (pressedParam)
            {
                horizontalMoveData += -1f;
            }
            else
            {
                horizontalMoveData += 1f;
            }
            horizontalData = pressedParam;
        }
        else if (buttonName == "moveButton_right")
        {
            if (pressedParam)
            {
                horizontalMoveData += 1f;
            }
            else
            {
                horizontalMoveData += -1f;
            }
            horizontalData = pressedParam;
        }
        else if (buttonName == "attackButton")
        {
            attackData = pressedParam;
        }
        else if (buttonName == "jumpButton")
        {
            jumpData = pressedParam;
        }

        if (playerMovement)
        {
            playerMovement.SetDataFromGamePad(horizontalMoveData, horizontalData, jumpData, attackData);
            playerMovement.SetHorizontalMoveDataFromGamePad(horizontalMoveData);
        }
    }
    private void FixedUpdate()
    {
        if (horizontalData)
        {
            if (playerMovement)
            {
                playerMovement.SetHorizontalMoveDataFromGamePad(horizontalMoveData);
            }
        }
    }
    private void Update()
    {
       if (Input.touchCount == 0 && !isDesktopWork)
        {
            horizontalMoveData = 0f;
            horizontalData = false;
            jumpData = false;
            attackData = false;
            playerMovement.SetDataFromGamePad(horizontalMoveData, horizontalData, jumpData, attackData);
            playerMovement.SetHorizontalMoveDataFromGamePad(horizontalMoveData);
        }
       if (isDesktopWork)
        {
            if (Input.GetKeyDown(KeyCode.W)){

                ManageEvent("jumpButton", true);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                ManageEvent("jumpButton", false);
            }


            if (Input.GetKeyDown(KeyCode.A))
            {
                ManageEvent("moveButton_left", true);
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                ManageEvent("moveButton_left", false);
            }


            if (Input.GetKeyDown(KeyCode.D))
            {
                ManageEvent("moveButton_right", true);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                ManageEvent("moveButton_right", false);
            }



            if (Input.GetKeyDown(KeyCode.E))
            {
                ManageEvent("attackButton", true);
            }
            if (Input.GetKeyUp(KeyCode.E))
            {
                ManageEvent("attackButton", false);
            }
        }
    }
}