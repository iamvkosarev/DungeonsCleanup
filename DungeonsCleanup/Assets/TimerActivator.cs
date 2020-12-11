using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimerActivator : MonoBehaviour
{
    public EventHandler OnActivateTimerEvent;
    public EventHandler OnDeactivateTimerEvent;
    public EventHandler OnStopTimerEvent;


    public void ActivateTimer()
    {
        OnActivateTimerEvent.Invoke(this, EventArgs.Empty);
    }
    public void DeactivateTimer()
    {
        OnDeactivateTimerEvent.Invoke(this, EventArgs.Empty);
    }

    public void StopTimer()
    {
        OnStopTimerEvent.Invoke(this, EventArgs.Empty);
    }
}
