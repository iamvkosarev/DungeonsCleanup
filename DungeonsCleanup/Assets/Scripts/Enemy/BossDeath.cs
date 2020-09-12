using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    [SerializeField] GameObject notificationWindowPrefab;
    [SerializeField] GameObject playerCanvas;
    [SerializeField] string name;
    private void OnDestroy()
    {
        GameObject newNotificationWindow = Instantiate(notificationWindowPrefab, playerCanvas.transform);
        newNotificationWindow.GetComponent<NotificationWindow>().SetButtomPartText($"[{name}]");
        newNotificationWindow.GetComponent<NotificationWindow>().SetTopPartText($"Внимание");
    }
}
