using System;
using UnityEngine;

namespace Heatmap.Events
{
    [Serializable]
    public class MultipliedEventPosition
    {
        [SerializeField] private Vector3 position;
        [SerializeField] private int multiplier = 1;

        public MultipliedEventPosition(Vector3 position)
        {
            this.position = position;
        }

        public Vector3 Position => position;
        public int Multiplier => multiplier;
    }
}