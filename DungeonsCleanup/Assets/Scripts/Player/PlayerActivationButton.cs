﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivationButton : MonoBehaviour
{
    [SerializeField] float checkRadius;
    [Header("Layers")]
    [SerializeField] LayerMask weaponLayer;
    [SerializeField] LayerMask elevatorLayer;
    [SerializeField] LayerMask doorLayer;
    [SerializeField] LayerMask tabletLayer;
    [SerializeField] LayerMask itemLayer;
    [SerializeField] ActivateSomeThingButton activateSomeThingButton;

    [Header("OpenDoor")]
    [SerializeField] Transform doorCheckPoint;
    [SerializeField] Vector2 doorCheckSize;

    PlayerActionControls playerActionControls;
    PlayerAttackManager playerAttackManager;
    bool canPlayerActivateSomeThing;
    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        playerActionControls.Land.SwitchWeapon.started += _ => ActivateSomeThing();
    }
    private void Start()
    {
        playerAttackManager = GetComponent<PlayerAttackManager>();
    }

    private void ActivateSomeThing()
    {
        if (!canPlayerActivateSomeThing) { return; }
        OpenDoor();
        TransferPlayer();
        ShowTabletText();
        ShowItmeCanvas();
    }
    private void Update()
    {
        CheckPossibilityToActivateSomeThing();
        SwitchingActivateButton();
    }

    private void SwitchingActivateButton()
    {
        if (canPlayerActivateSomeThing)
        {
            activateSomeThingButton.SwitchOn();
        }
        else
        {
            activateSomeThingButton.SwitchOff();
        }
    }

    private void CheckPossibilityToActivateSomeThing()
    {
        bool isPlayerTouchDoor = Physics2D.OverlapBox(doorCheckPoint.position, doorCheckSize, 0, doorLayer);
        bool isPlayerTouchElevator = Physics2D.OverlapCircle(transform.position, checkRadius, elevatorLayer);
        bool isPlayerTouchTablet = Physics2D.OverlapCircle(transform.position, checkRadius, tabletLayer);
        bool isPlayerTouchItem = Physics2D.OverlapCircle(transform.position, checkRadius, itemLayer);

        canPlayerActivateSomeThing = (isPlayerTouchDoor || isPlayerTouchElevator || isPlayerTouchTablet || isPlayerTouchItem);
    }



    private void OpenDoor()
    {
        Collider2D doorCollider = Physics2D.OverlapBox(doorCheckPoint.position, doorCheckSize, 0, doorLayer);
        if (doorCollider != null)
        {
            Debug.Log("Open door!");
            doorCollider.gameObject.GetComponent<Door>().Open();
        }
    }

    private void TransferPlayer()
    {
        Collider2D elevatorCollider =  Physics2D.OverlapCircle(transform.position, checkRadius, elevatorLayer);
        if (elevatorCollider != null)
        {
            Debug.Log("Teleportation!");
            elevatorCollider.gameObject.GetComponent<Elevator>().Transfer(gameObject.transform);
        }
    }
    private void ShowItmeCanvas()
    {
        Collider2D itemCollider = Physics2D.OverlapCircle(transform.position, checkRadius, itemLayer);
        if (itemCollider != null)
        {
            Debug.Log("Item!");
            itemCollider.GetComponent<Item>().InstansiateItemInfoCanvas();
        }
    }
    private void ShowTabletText()
    {
        Collider2D paperCollider = Physics2D.OverlapCircle(transform.position, checkRadius, tabletLayer);
        if (paperCollider != null)
        {
            Debug.Log("Tablet Text!");
            paperCollider.GetComponent<Paper>().InstansiateTabletCanvas();
        }
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }
    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(doorCheckPoint.position, doorCheckSize);
    }
}
