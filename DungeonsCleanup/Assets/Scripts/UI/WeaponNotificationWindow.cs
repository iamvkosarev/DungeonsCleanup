using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponNotificationWindow : MonoBehaviour
{

    public void CloseNotificationWindow()
    {
        GetComponent<Animator>().SetTrigger("CloseWindow");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
    public void SwitchChildrenActive(int active)
    {
        bool isChildrenActive = active != 0 ? true : false;
        foreach (Transform child in transform)
        {
            child.gameObject.active = isChildrenActive;
        }
    }
}
