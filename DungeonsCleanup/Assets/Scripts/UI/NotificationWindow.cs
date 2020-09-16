using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NotificationWindow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI topPartText;
    [SerializeField] TextMeshProUGUI buttomPartText;

    public void CloseNotificationWindow()
    {
        GetComponent<Animator>().SetTrigger("CloseWindow");
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
    public void SetTopPartText(string newText)
    {
        topPartText.text = newText;
    }
    public void SetButtomPartText(string newText)
    {
        buttomPartText.text = newText;
    }

    public void SwitchChildrenActive(int active)
    {
        bool isChildrenActive = active != 0  ? true : false;
        foreach(Transform child in transform)
        {
            child.gameObject.active = isChildrenActive;
        }
    }
}
