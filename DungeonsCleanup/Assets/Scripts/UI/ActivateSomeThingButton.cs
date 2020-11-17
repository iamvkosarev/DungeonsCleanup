using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using TMPro;

public class ActivateSomeThingButton : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Image myImage;
    [SerializeField] GameObject activateText;
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
        activateText.SetActive(false);
        onScreenButton.enabled = false;
    }

    public void SwitchOn()
    {

        myImage.color = onColor;
        activateText.SetActive(true);
        onScreenButton.enabled = true;
    }
}
