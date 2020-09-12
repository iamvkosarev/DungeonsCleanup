using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] StabbingWeapon stabbingWeaponInfo;
    [SerializeField] LayerMask playerLayerMask;
    [SerializeField] float checkRadius;
    PlayerActionControls playerActionControls;
    SwtichWeapon swtichWeaponButton;
    SpriteRenderer mySpriteRender;
    PlayerAttackManager playerAttackManager;
    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        playerAttackManager = FindObjectOfType<PlayerAttackManager>();
        playerActionControls.Land.SwitchWeapon.started +=_ => SwitchWeapon();
    }

    private void SwitchWeapon()
    {
        stabbingWeaponInfo = playerAttackManager.SwitchCurrentStabbingWeapon(stabbingWeaponInfo);
        SetIconSprite();
    }

    private void Start() { 
        mySpriteRender = GetComponentInChildren<SpriteRenderer>();
        swtichWeaponButton = FindObjectOfType<SwtichWeapon>();
        SetIconSprite();
    }
    private void OnEnable()
    {
        playerActionControls.Enable();
    }
    private void OnDisable()
    {
        playerActionControls.Disable();
    }
    private void Update()
    {
        checkPlayerEnter();
    }

    private void checkPlayerEnter()
    {
        bool isPlayerTouckWeapon = Physics2D.OverlapCircle(transform.position, checkRadius, playerLayerMask);
        if (isPlayerTouckWeapon)
        {
            swtichWeaponButton.SwitchOn();
        }
        else
        {
            swtichWeaponButton.SwitchOff();
        }
    }

    private void SetIconSprite()
    {
        mySpriteRender.sprite = stabbingWeaponInfo.GetIcon();
    }

}
