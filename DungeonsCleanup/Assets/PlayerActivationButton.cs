using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivationButton : MonoBehaviour
{
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask weaponLayer;
    [SerializeField] LayerMask doorLayer;
    [SerializeField] ActivateSomeThingButton activateSomeThingButton;

    GameObject weaponNotificationWindow;
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
        SwitchCurrentWeapon();
        OpenDoor();
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
        bool isPlayerTouchWeapon = Physics2D.OverlapCircle(transform.position, checkRadius, weaponLayer);
        bool isPlayerTouchDoor = Physics2D.OverlapCircle(transform.position, checkRadius, doorLayer);

        canPlayerActivateSomeThing = (isPlayerTouchDoor || isPlayerTouchWeapon);
    }

    private void SwitchCurrentWeapon()
    {
        Collider2D weaponCollider = Physics2D.OverlapCircle(transform.position, checkRadius, weaponLayer);
        if (weaponCollider != null && weaponNotificationWindow == null)
        {
            weaponNotificationWindow = weaponCollider.GetComponent<Weapon>().ShowWeaponInfo(playerAttackManager);
        }
    }

    private void OpenDoor()
    {
        Collider2D doorCollider = Physics2D.OverlapCircle(transform.position, checkRadius, doorLayer);
        if (doorCollider != null)
        {
            doorCollider.gameObject.GetComponent<Door>().Open();
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
}
