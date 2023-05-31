using System.Collections.Generic;
using UnityEngine;

namespace Heatmap.Visualisation
{
    using static ParticleSystem;
    using Controller;

    public class HeatmapParticleSystem
    {
        private Bounds particleSystemBounds;
        private ParticleSystem particleSystem;
        private Vector3Int particleGridSize;
        private Particle[,,] particlesArray;
        private Settings settings;

        public Vector3Int ParticleGridSize => particleGridSize;

        /// <param name="particleSystemBox">Object that limiting particles area and contains it</param>
        public void InitializeParticleSystem(BoxCollider particleSystemBox, Settings settings)
        {
            particleSystemBounds = particleSystemBox.bounds;
            this.settings = settings;
            particleSystem = CreateAndConfigureParticleSystem(particleSystemBox.gameObject);
        }

        public void DestroyParticleSystem()
        {
            Object.DestroyImmediate(particleSystem);
        }

        private ParticleSystem CreateAndConfigureParticleSystem(GameObject parent)
        {
            var newParticleSystem = parent.AddComponent<ParticleSystem>();

            var emission = newParticleSystem.emission;
            emission.enabled = false;

            var shape = newParticleSystem.shape;
            shape.enabled = false;

            var renderer = parent.GetComponent<ParticleSystemRenderer>();
            renderer.sortMode = ParticleSystemSortMode.Distance;
            renderer.allowRoll = false;
            renderer.alignment = ParticleSystemRenderSpace.Facing;

            var main = newParticleSystem.main;
            main.loop = false;
            main.simulationSpace = ParticleSystemSimulationSpace.World;
            main.maxParticles = settings.MaxParticleNumber;
            main.playOnAwake = false;


            renderer.material = settings.ParticleMaterial;

            return newParticleSystem;
        }

        public void CreateParticleArray(Settings settings)
        {
            particleGridSize = CalculateParticleGridSize();

            particlesArray = new Particle[particleGridSize.x, particleGridSize.y, particleGridSize.z];
            Debug.Log(particleGridSize);
            for (var x = 0; x < particleGridSize.x; x++)
            {
                for (var y = 0; y < particleGridSize.y; y++)
                {
                    for (var z = 0; z < particleGridSize.z; z++)
                    {
                        Particle particle = new();

                        var position = ConvertParticleGridPositionToGlobal(new Vector3Int(x, y, z));
                        particle.position = position;

                        particle.startSize = settings.ParticleSize;
                        particle.startColor = settings.Gradient.Evaluate(0);

                        particle.remainingLifetime = 1000;
                        particle.startLifetime = 1000;

                        particlesArray[x, y, z] = particle;
                    }
                }
            }
        }

        public void UpdateParticlesInParticleSystem(float[,,] particleColorValues)
        {
            List<Particle> particleList = new();

            for (var x = 0; x < particleGridSize.x; x++)
            {
                for (var y = 0; y < particleGridSize.y; y++)
                {
                    for (var z = 0; z < particleGridSize.z; z++)
                    {
                        if (!(settings.StartValueToShowColor <= particleColorValues[x, y, z]))
                        {
                            continue;
                        }

                        var particleColor = settings.Gradient.Evaluate(particleColorValues[x, y, z]);
                        if (!(particleColor.a > 0.001f))
                        {
                            continue;
                        }

                        particlesArray[x, y, z].startColor = particleColor;
                        particleList.Add(particlesArray[x, y, z]);
                    }
                }
            }

            particleSystem.SetParticles(particleList.ToArray());
        }

        private Vector3 ConvertParticleGridPositionToGlobal(Vector3Int positionInParticleGrid)
        {
            Vector3 convertedPosition;
            convertedPosition.x = (positionInParticleGrid.x * settings.ParticleDistance) + particleSystemBounds.min.x;
            convertedPosition.y = (positionInParticleGrid.y * settings.ParticleDistance) + particleSystemBounds.min.y;
            convertedPosition.z = (positionInParticleGrid.z * settings.ParticleDistance) + particleSystemBounds.min.z;

            return convertedPosition;
        }

        public Vector3Int ConvertGlobalPositionToParticleGrid(Vector3 globalPosition)
        {
             var convertedPosition =
                Vector3Int.RoundToInt((globalPosition - particleSystemBounds.min) / settings.ParticleDistance);

            return convertedPosition;
        }

        private Vector3Int CalculateParticleGridSize()
        {
            var particleGridSize = Vector3Int.FloorToInt((particleSystemBounds.max - particleSystemBounds.min) /
                                                         settings.ParticleDistance);

            return particleGridSize;
        }
    }
}