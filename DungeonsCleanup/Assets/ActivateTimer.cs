using UnityEngine;

public class ActivateTimer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<TimerActivator>().ActivateTimer();
    }
}
