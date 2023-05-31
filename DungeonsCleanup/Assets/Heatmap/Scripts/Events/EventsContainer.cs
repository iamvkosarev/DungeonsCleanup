using System;
using System.Collections.Generic;
using UnityEngine;

namespace Heatmap.Events
{
    [Serializable]
    public class EventsContainer
    {
        [SerializeField] private List<MultipliedEventPosition> positions = new List<MultipliedEventPosition>();
        [SerializeField] private string eventName;
        [SerializeField] private bool isCurrentlyDisplayedOnHeatmap;

        public EventsContainer(string eventName)
        {
            this.eventName = eventName;
        }

        public List<MultipliedEventPosition> Positions => positions;

        public string EventName => eventName;

        public bool IsCurrentlyDisplayedOnHeatmap => isCurrentlyDisplayedOnHeatmap;
    }
}