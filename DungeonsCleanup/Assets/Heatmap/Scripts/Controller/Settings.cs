using System;
using UnityEngine;

namespace Heatmap.Controller
{
    [Serializable, CreateAssetMenu(menuName = "Heatmap/Settings/Base", fileName = "Settings")]
    public class Settings : ScriptableObject
    {
        [SerializeField] private Material particleMaterial;
        [SerializeField] private int maxParticleNumber = 30000;
        [SerializeField, Min(0.05f)] private float particleDistance = 0.2f;
        [SerializeField] private float particleSize = 2.5f;
        [SerializeField] private float startValueToShowColor = 0.05f;
        [SerializeField] private float coloringRadius= 1.2f;
        [SerializeField] private float colorMultiplier = 0.4f;
        [SerializeField] private bool showColoringInYAxis = true;
        [SerializeField] private Gradient gradient;

        public float ParticleSize => particleSize;
        public float StartValueToShowColor => startValueToShowColor;

        public Gradient Gradient => gradient;
        public int MaxParticleNumber => maxParticleNumber;
        public float ColoringRadius => coloringRadius;
        public float ParticleDistance => particleDistance;
        public float ColorMultiplier => colorMultiplier;
        public Material ParticleMaterial => particleMaterial;
        public bool ShowColoringInYAxis => showColoringInYAxis;

        public bool IsParticlesInitialize { set; get; } = false;
    }
}