using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class OwnButtonCheck : Button
{
    [SerializeField] public string buttonName;
    public PressButtonEvent pressButtonEvent;

    public override void OnPointerDown(PointerEventData eventData)
    {
        pressButtonEvent.Invoke(buttonName,true);
    }


    public override void OnPointerUp(PointerEventData eventData)
    {
        pressButtonEvent.Invoke(buttonName, false);
    }
}

[System.Serializable]
public class PressButtonEvent: UnityEvent<string, bool> { }
