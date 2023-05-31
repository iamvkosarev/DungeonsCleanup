using System;
using UnityEngine;

namespace Heatmap.Events
{
    [Serializable]
    public class BaseEvent
    {
        public Vector3 Position;
        public string EventName;

        public BaseEvent(string EventName, Vector3 Position)
        {
            this.Position = Position;
            this.EventName = EventName;
        }
    }
}