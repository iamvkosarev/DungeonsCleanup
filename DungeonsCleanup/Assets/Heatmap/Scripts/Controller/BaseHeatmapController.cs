using System.Collections.Generic;
using System.Diagnostics;
using Heatmap.Readers;
using Sirenix.OdinInspector;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Heatmap.Controller
{
    using Visualisation;
    using Events;

    public abstract class BaseHeatmapController : MonoBehaviour
    {
        [SerializeField] private BoxCollider particleSystemBox;
        [SerializeField] private List<EventsContainer> eventsContainersList = new();
        [SerializeField] private Settings settings;

        private HeatmapVisualisation heatmapVisualisation;
        private bool particleSystemIsInitialized;

        private HeatmapVisualisation HeatmapVisualisation =>
            heatmapVisualisation ??= new HeatmapVisualisation(settings);

        private List<EventsContainer> EventsContainersList => eventsContainersList;

        [Button]
        public void ClearEvents()
        {
            EventsContainersList.Clear();
        }

        [Button]
        public async void LoadEvents()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            var eventReader = GetEventReader();
            AddEvents(await eventReader.ReadEvents());
            
            stopwatch.Stop();
            Debug.Log("Загрузка событий - скорость работы "+ stopwatch.ElapsedMilliseconds + " мс");
        }


        protected abstract IEventReader GetEventReader();

        private void AddEvents(IEnumerable<EventsContainer> readEvents)
        {
            EventsContainersList.AddRange(readEvents);;
        }


        [Button, DisableIf("IsParticlesInitialize"), HorizontalGroup("Initialize")]
        public void InitializeParticlesSystem()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            HeatmapVisualisation.InitializeParticleSystem(particleSystemBox);
            HeatmapVisualisation.InitializeParticleArray();
            particleSystemIsInitialized = true;
            settings.IsParticlesInitialize = particleSystemIsInitialized;
            stopwatch.Stop();
            Debug.Log("Создание зоны из частиц - скорость работы " + stopwatch.ElapsedMilliseconds + " мс");
        }

        [Button, DisableIf("IsNotParticlesInitialize"), HorizontalGroup("Initialize")]
        public void DestroyParticlesSystem()
        {
            HeatmapVisualisation.DestroyParticleSystem();
            heatmapVisualisation = null;
            particleSystemIsInitialized = false;
            settings.IsParticlesInitialize = particleSystemIsInitialized;
        }

        private bool IsNotParticlesInitialize => !particleSystemIsInitialized;
        private bool IsParticlesInitialize => particleSystemIsInitialized;

        [Button, DisableIf("IsNotParticlesInitialize")]
        public void ShowSelectedEvents()
        {
            Stopwatch stopwatch = new();
            stopwatch.Start();
            HeatmapVisualisation.ResetParticlesColor();

            foreach (var eventsContainer in EventsContainersList)
            {
                if (eventsContainer.IsCurrentlyDisplayedOnHeatmap)
                {
                    HeatmapVisualisation.AddEventToHeatMap(eventsContainer);
                }
            }

            HeatmapVisualisation.UpdateParticlesInParticleSystem();
            stopwatch.Stop();
            Debug.Log("Отображение тепловой карты - скорость работы " + stopwatch.ElapsedMilliseconds + " мс");
        }

        [Button, DisableIf("IsNotParticlesInitialize")]
        public void ResetHeatmap()
        {
            HeatmapVisualisation.ResetParticlesColor();
            HeatmapVisualisation.UpdateParticlesInParticleSystem();
        }
    }
}