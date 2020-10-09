using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActivationButton : MonoBehaviour
{
    [SerializeField] float checkRadius;
    [SerializeField] LayerMask weaponLayer;
    [SerializeField] LayerMask elevatorLayer;
    [SerializeField] ActivateSomeThingButton activateSomeThingButton;

    [Header("OpenDoor")]
    [SerializeField] Transform doorCheckPoint;
    [SerializeField] Vector2 doorCheckSize;
    [SerializeField] LayerMask doorLayer;

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
        TransferPlayer();
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
        bool isPlayerTouchDoor = Physics2D.OverlapBox(doorCheckPoint.position, doorCheckSize, 0, doorLayer);
        bool isPlayerTouchElevator = Physics2D.OverlapCircle(transform.position, checkRadius, elevatorLayer);

        canPlayerActivateSomeThing = (isPlayerTouchDoor || isPlayerTouchWeapon || isPlayerTouchElevator);
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
        Collider2D doorCollider = Physics2D.OverlapBox(doorCheckPoint.position, doorCheckSize, 0, doorLayer);
        if (doorCollider != null)
        {
            doorCollider.gameObject.GetComponent<Door>().Open();
        }
    }

    private void TransferPlayer()
    {
        Collider2D elevatorCollider =  Physics2D.OverlapCircle(transform.position, checkRadius, elevatorLayer);
        if (elevatorCollider != null)
        {
            elevatorCollider.gameObject.GetComponent<Elevator>().Transfer(gameObject.transform);
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
