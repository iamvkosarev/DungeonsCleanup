using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionForm : MonoBehaviour
{
    [SerializeField] GameObject selectSessionFormButton;
    [SerializeField] GameObject startGameButton;
    [SerializeField] GameObject creatButton;
    bool isSessionSelected;
    bool isCreated;
    private void Start()
    {
        DeselectSession();
    }
    public void SelectSession()
    {
        isSessionSelected = true;
        SwitchOffAllElement();
        if (isCreated)
        {
            startGameButton.SetActive(true);
        }
        else
        {
            creatButton.SetActive(true);
        }
    }
    public void SetSessionCreated()
    {
        isCreated = true;
    }
    public void DeselectSession()
    {
        isSessionSelected = false;
        SwitchOffAllElement();
        selectSessionFormButton.SetActive(true);
    }
    public bool IsSessionSelected()
    {
        return isSessionSelected;
    }
    public void SwitchOffAllElement()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
