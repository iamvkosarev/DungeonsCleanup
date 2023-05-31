using Heatmap.Controller;
using UnityEngine;

namespace Heatmap.Visualisation
{
    using Events;

    public class HeatmapVisualisation
    {
        private readonly Settings settings;
        private readonly HeatmapParticleSystem heatmapParticleSystem;
        private float[,,] particleColorValues;

        public HeatmapVisualisation(Settings settings)
        {
            this.settings = settings;

            heatmapParticleSystem = new HeatmapParticleSystem();
        }

        /// <param name="particleSystemBox">Object that limiting particles area and contains it</param>
        public void InitializeParticleSystem(BoxCollider particleSystemBox)
        {
            if (particleSystemBox.GetComponent<ParticleSystem>() != null)
            {
                Debug.Log("There is particle system present on parent object already!");
                return;
            }

            if (settings.ParticleMaterial == null)
            {
                Debug.LogError("Particle material doesn't exist in settings!");

                return;
            }

            heatmapParticleSystem.InitializeParticleSystem(particleSystemBox, settings);
        }

        public void DestroyParticleSystem()
        {
            heatmapParticleSystem.DestroyParticleSystem();
        }

        public void InitializeParticleArray()
        {
             heatmapParticleSystem.CreateParticleArray(settings);

            ResetParticlesColor();
            UpdateParticlesInParticleSystem();
        }

        public void AddEventToHeatMap(EventsContainer eventContainerOld)
        {
            foreach (var eventPosition in eventContainerOld.Positions)
            {
                AddOnePositionToHeatmap(eventPosition);
            }
        }
        public void ResetParticlesColor()
        {
            var sizeInParticles = heatmapParticleSystem.ParticleGridSize;
            particleColorValues = new float[sizeInParticles.x, sizeInParticles.y, sizeInParticles.z];
        }
        public void UpdateParticlesInParticleSystem()
        {
            heatmapParticleSystem.UpdateParticlesInParticleSystem(particleColorValues);
        }
        private void AddOnePositionToHeatmap(MultipliedEventPosition eventPosition)
        {
            var eventPositionInParticleGrid =
                heatmapParticleSystem.ConvertGlobalPositionToParticleGrid(eventPosition.Position);
            var particleGridSize = heatmapParticleSystem.ParticleGridSize;

            var minBound = CalculateMinBound(eventPositionInParticleGrid);
            var maxBound = CalculateMaxBound(eventPositionInParticleGrid);

            for (int x = minBound.x; x <= maxBound.x; x += 1)
            {
                for (int y = minBound.y; y <= maxBound.y; y += 1)
                {
                    for (int z = minBound.z; z <= maxBound.z; z += 1)
                    {
                        if (IsInBoundsOfParticleArray(x, y, z, particleGridSize))
                        {
                            UpdateColorAddValue(new Vector3Int(x, y, z), eventPositionInParticleGrid, eventPosition);
                        }
                    }
                }
            }
        }

        private void UpdateColorAddValue(Vector3Int particlePositionInGrid, Vector3Int eventPositionInParticleGrid, MultipliedEventPosition eventPosition)
        {
            var distance = Vector3IntDistance(particlePositionInGrid, eventPositionInParticleGrid, settings.ShowColoringInYAxis, settings.ParticleDistance);

            if (!(distance < settings.ColoringRadius)) return;
            // calculate colorAddValue, depending on how close is distance to maxColoringDistance
            var colorAddValue = settings.ColorMultiplier * (1 - distance / settings.ColoringRadius);

            particleColorValues[particlePositionInGrid.x, particlePositionInGrid.y, particlePositionInGrid.z] += colorAddValue * eventPosition.Multiplier;
        }

        private static float Vector3IntDistance(Vector3Int point1, Vector3Int point2, bool showColoringInYAxis, float particleDistance)
        {
            float distanceSquare = (point1.x - point2.x) * (point1.x - point2.x) + (point1.z - point2.z) * (point1.z - point2.z);

            if (showColoringInYAxis)
            {
                distanceSquare += (point1.y - point2.y) * (point1.y - point2.y);
            }

            return Mathf.Sqrt(distanceSquare) * particleDistance;
        }


        private bool IsInBoundsOfParticleArray(int x, int y, int z, Vector3Int sizeInParticles)
        {
            return x >= 0 && z >= 0 && y >= 0 && x < sizeInParticles.x && y < sizeInParticles.y & z < sizeInParticles.z;
        }
        private Vector3Int CalculateMinBound(Vector3Int positionInParticleGrid)
        {
            Vector3Int min = new()
            {
                x = (int)(positionInParticleGrid.x - settings.ColoringRadius / settings.ParticleDistance - 1),
                z = (int)(positionInParticleGrid.z - settings.ColoringRadius / settings.ParticleDistance - 1)
            };

            if (settings.ShowColoringInYAxis)
            {
                min.y = (int)(positionInParticleGrid.y - settings.ColoringRadius / settings.ParticleDistance - 1);
            }
            else
            {
                min.y = 0;
            }

            return min;
        }

        private Vector3Int CalculateMaxBound(Vector3Int positionInParticleGrid)
        {
            Vector3Int max = new()
            {
                x = (int)(positionInParticleGrid.x + settings.ColoringRadius / settings.ParticleDistance + 1),
                z = (int)(positionInParticleGrid.z + settings.ColoringRadius / settings.ParticleDistance + 1)
            };

            if (!settings.ShowColoringInYAxis)
            {
                max.y = (int)(positionInParticleGrid.y + settings.ColoringRadius / settings.ParticleDistance + 1);
            }
            else
            {
                max.y = positionInParticleGrid.y - 1;
            }

            return max;
        }

    }
}