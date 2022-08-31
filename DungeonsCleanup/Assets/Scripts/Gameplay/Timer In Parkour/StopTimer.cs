using UnityEngine;

public class StopTimer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<TimerActivator>().StopTimer();
    }
}
