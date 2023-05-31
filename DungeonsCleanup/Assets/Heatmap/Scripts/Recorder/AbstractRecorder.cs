using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Heatmap.Events;
using Heatmap.Writers;
using UnityEngine;

namespace Heatmap.Scripts.Recorder
{
    public class RecordeSettingContainer
    {
        public float RecordInterval { get; }
        public string EventName { get; }
        public Func<Vector3> GetData { get; }


        public RecordeSettingContainer(string eventName, float recordInterval, Func<Vector3> getData)
        {
            RecordInterval = recordInterval;
            EventName = eventName;
            GetData = getData;
        }
    }

    public abstract class AbstractRecorder : IRecorder
    {
        protected abstract IEventWriter EventWriter { get; }

        private Coroutine _recording;
        private bool _isRecording;
        private ICoroutineRunner _coroutineRunner;
        private readonly RecordeSettingContainer _recordeSettingContainer;
        private List<BaseEvent> _recordeEvents = new();


        protected AbstractRecorder(RecordeSettingContainer recordeSettingContainer,
            ICoroutineRunner coroutineRunner)
        {
            _recordeSettingContainer = recordeSettingContainer;
            _coroutineRunner = coroutineRunner;
        }

        public void Play()
        {
            StartCoroutine();
        }

        public void Pause()
        {
            StopCoroutine();
        }

        public void Break()
        {
            StopCoroutine();
            ClearRecordeEvents();
        }

        public virtual void Complete()
        {
            StopCoroutine();
            SaveRecordeEvents();
            ClearRecordeEvents();
        }

        private void ClearRecordeEvents()
        {
            _recordeEvents.Clear();
        }

        private void StartCoroutine()
        {
            _recording = _coroutineRunner.StartCoroutine(Recorde());
        }

        private void StopCoroutine()
        {
            _coroutineRunner?.StopCoroutine(_recording);
        }


        private async Task SaveRecordeEvents()
        {
            foreach (var recordeEvent in _recordeEvents)
            {
                EventWriter.SaveEvent(recordeEvent);
            }
        }

        private IEnumerator Recorde()
        {
            while (true)
            {
                RecordEvent();
                yield return new WaitForSeconds(_recordeSettingContainer.RecordInterval);
            }
        }

        private void RecordEvent()
        {
            var baseEvent = PrepareData(_recordeSettingContainer.GetData());

            if (baseEvent == null) return;
            
            _recordeEvents.Add(baseEvent);

        }

        private BaseEvent PrepareData(Vector3 getData) => new(_recordeSettingContainer.EventName, getData);
    }
}