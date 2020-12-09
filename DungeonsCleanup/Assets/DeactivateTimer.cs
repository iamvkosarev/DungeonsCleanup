using UnityEngine;

public class DeactivateTimer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<TimerActivator>().DeactivateTimer();
    }
}
