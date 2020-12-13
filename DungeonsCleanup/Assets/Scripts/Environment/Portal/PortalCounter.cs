using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PortalCounter : MonoBehaviour
{
    public EventHandler OnPortalCountEvent;

    public void CountPortal()
    {
        if (OnPortalCountEvent != null)
        {
            OnPortalCountEvent.Invoke(this, EventArgs.Empty);
        }
    }
}
