using System;
using UnityEngine;

namespace Heatmap.Scripts.Recorder
{
    public abstract class AbstractFileRecorder : AbstractRecorder
    {
        protected string SavePath { get; }

        protected AbstractFileRecorder(RecordeSettingContainer recordeSettingContainer, string savePath,
            ICoroutineRunner coroutineRunner) : base(recordeSettingContainer, coroutineRunner)
        {
            SavePath = savePath;
        }
    }
}