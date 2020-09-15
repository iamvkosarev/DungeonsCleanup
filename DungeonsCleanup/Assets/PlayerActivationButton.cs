using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivationButton : MonoBehaviour
{
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask weaponLayer;
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
        bool isPlayerTouckWeapon = Physics2D.OverlapCircle(transform.position, checkRadius, weaponLayer);
        if (isPlayerTouckWeapon)
        {
            canPlayerActivateSomeThing = true;
        }
        else
        {
            canPlayerActivateSomeThing = false;
        }
    }

    private void SwitchCurrentWeapon()
    {
        Collider2D weaponCollider = Physics2D.OverlapCircle(transform.position, checkRadius, weaponLayer);
        if (weaponCollider != null && weaponNotificationWindow == null)
        {
            weaponNotificationWindow = weaponCollider.GetComponent<Weapon>().ShowWeaponInfo(playerAttackManager);
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
