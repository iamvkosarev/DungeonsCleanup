using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGamepad : MonoBehaviour
{
    [SerializeField] private Transform joystick, attackButton, activateButton;
    private PlayerPrefsController playerPrefsController;
    public bool Reverse;
    [SerializeField] private Vector3 newJoystickPosition;
    [SerializeField] private Vector3 newAttackButtonPosition;
    [SerializeField] private Vector3 newActivateButtonPosition;
    void Start()
    {
        joystick = GetComponent<Transform>();
        attackButton = GetComponent<Transform>();
        activateButton = GetComponent<RectTransform>();

        if(Reverse)
        {
            Debug.Log("im here");
            joystick.position = newJoystickPosition;
            attackButton.position = newAttackButtonPosition;
            activateButton.position = newActivateButtonPosition;
        }
    }

    void SwapButtonPosition()
    {

    }
}
