using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

public class SwtichWeapon : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image myImage;
    [SerializeField] UnityEngine.UI.Image childImage;
    [SerializeField] Color onColor;
    [SerializeField] Color offColor;

    OnScreenButton onScreenButton;
    private void Start()
    {
        onScreenButton = GetComponent<OnScreenButton>();
    }
    public void SwitchOff()
    {
        myImage.color = offColor;
        childImage.color = offColor;
        onScreenButton.enabled = false;
    }

    public void SwitchOn()
    {

        myImage.color = onColor;
        childImage.color = onColor;
        onScreenButton.enabled = true;
    }
}
