using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] StabbingWeapon stabbingWeaponInfo;
    [SerializeField] GameObject notificationWindowPrefab;
    [SerializeField] GameObject playerCanvas;
    [SerializeField] LayerMask playerLayerMask;
    SpriteRenderer mySpriteRender;

    public StabbingWeapon SwitchWeapon(StabbingWeapon newStabbingWeapon)
    {
        StabbingWeapon previousStabbingWeapon = stabbingWeaponInfo;
        stabbingWeaponInfo = newStabbingWeapon;
        SetIconSprite();
        return previousStabbingWeapon;
    }

    private void Start() { 
        mySpriteRender = GetComponentInChildren<SpriteRenderer>();
        SetIconSprite();
    }
    public StabbingWeapon GetStabbingWeapon()
    {
        return stabbingWeaponInfo;
    }
    private void SetIconSprite()
    {
        mySpriteRender.sprite = stabbingWeaponInfo.GetIcon();
    }

    public GameObject ShowWeaponInfo(PlayerAttackManager playerAttackManager)
    {
        GameObject newNotificationWindow = Instantiate(notificationWindowPrefab, playerCanvas.transform);
        WeaponNotificationWindow weaponNotificationWindowScript = newNotificationWindow.GetComponent<WeaponNotificationWindow>();
        weaponNotificationWindowScript.SetStabbingWeaponTextForms(stabbingWeaponInfo);
        weaponNotificationWindowScript.SetPlayerAttackManager(playerAttackManager);
        weaponNotificationWindowScript.SetWeapon(this);
        return newNotificationWindow;
    }

    public void SwitchStabbingWeapon(StabbingWeapon newStabbingWeapon)
    {
        stabbingWeaponInfo = newStabbingWeapon;
        SetIconSprite();
    }
}
