using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponNotificationWindow : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image iconImage;
    [SerializeField] UnityEngine.UI.Image iconImage_2;
    [SerializeField] UnityEngine.UI.Image playerIconImage;
    [SerializeField] TextMeshProUGUI nameTextForm;
    [SerializeField] TextMeshProUGUI classTextForm;
    [SerializeField] TextMeshProUGUI typeTextForm;
    [SerializeField] TextMeshProUGUI damageTextForm;
    [SerializeField] TextMeshProUGUI descriptionTextForm;
    [SerializeField] Transform beforeSwitching;
    [SerializeField] Transform inSwitching;

    PlayerAttackManager playerAttackManager;
    Weapon weapon;
    Animator myAnimator;
    private void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void SetWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }

    public void SetPlayerAttackManager(PlayerAttackManager playerAttackManager)
    {
        this.playerAttackManager = playerAttackManager;
        playerIconImage.sprite = playerAttackManager.GetCurrentStabbingWeapon().GetIcon();
    }

    public void CloseNotificationWindow()
    {
        myAnimator.SetTrigger("CloseWindow");
    }

    public void GoToSwitching()
    {
        myAnimator.SetTrigger("GoToChanging");
    }
    public void GoToProperties()
    {
        myAnimator.SetTrigger("GoToProperties");
    }
    public void SwitchWeapons()
    {
        StabbingWeapon playerCurrentWeapon = playerAttackManager.GetCurrentStabbingWeapon();
        StabbingWeapon weaponOnFloor = weapon.GetStabbingWeapon();
        weapon.SwitchStabbingWeapon(playerCurrentWeapon);
        playerAttackManager.SwitchCurrentStabbingWeapon(weaponOnFloor);
        CloseNotificationWindow();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
    public void SwitchChildrenActiveMode(int activeMode)
    {
        if (activeMode == 0)
        {
            foreach (Transform child in beforeSwitching)
            {
                child.gameObject.active = false;
            }
            foreach (Transform child in inSwitching)
            {
                child.gameObject.active = false;
            }
        }
        else if(activeMode == 1)
        {
            foreach (Transform child in beforeSwitching)
            {
                child.gameObject.active = true;
            }
            foreach (Transform child in inSwitching)
            {
                child.gameObject.active = false;
            }
        }
        else if(activeMode == 2)
        {
            foreach (Transform child in beforeSwitching)
            {
                child.gameObject.active = false;
            }
            foreach (Transform child in inSwitching)
            {
                child.gameObject.active = true;
            }
        }
    }
    public void SetStabbingWeaponTextForms(StabbingWeapon stabbingWeapon)
    {
        SetNameForm(stabbingWeapon.GetWeaponName());
        SetTypeForm(stabbingWeapon.GetTypeOfStebbing());
        SetClassForm(stabbingWeapon.GetClass());
        SetDamageForm(stabbingWeapon.GetDamage());
        SetDescriptionForm(stabbingWeapon.GetDescription());
        SetIconImage(stabbingWeapon.GetIcon());
    }
    public void SetNameForm(string weaponName)
    {
        nameTextForm.text = weaponName;
    }
    public void SetIconImage(Sprite newIcon)
    {
        iconImage.sprite = newIcon;
        iconImage_2.sprite = newIcon;
    }
    public void SetClassForm(string weaponClass)
    {
        classTextForm.text = weaponClass;
    }
    public void SetTypeForm(string weaponType)
    {
        typeTextForm.text = weaponType;
    }
    public void SetDamageForm(int weaponDamage)
    {
        damageTextForm.text = weaponDamage.ToString();
    }
    public void SetDescriptionForm(string weaponDescription)
    {
        descriptionTextForm.text = weaponDescription;
    }
}
