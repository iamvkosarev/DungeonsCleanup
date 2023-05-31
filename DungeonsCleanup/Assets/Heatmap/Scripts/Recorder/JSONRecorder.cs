using System;
using Heatmap.Events;
using Heatmap.Writers;
using UnityEngine;

namespace Heatmap.Scripts.Recorder
{
    internal class JSONRecorder : AbstractFileRecorder
    {
        private IEventWriter eventWriter;

        protected override IEventWriter EventWriter
        {
            get { return eventWriter ??= new JSONEventWriter(SavePath); }
        }


        public JSONRecorder(RecordeSettingContainer recordeSettingContainer, string savePath, ICoroutineRunner coroutineRunner) : base(recordeSettingContainer, savePath, coroutineRunner)
        {
        }
    }
}