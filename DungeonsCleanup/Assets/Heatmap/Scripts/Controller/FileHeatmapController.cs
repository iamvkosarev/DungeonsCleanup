using Heatmap.Readers;
using Heatmap.Scripts.Controller.SavePath;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Heatmap.Controller
{
    public abstract class FileHeatmapController : BaseHeatmapController
    {
        [SerializeField] private bool _useSavePathFile = true;
        
        [SerializeField, ShowIf("_useSavePathFile")] private BaseSavePath _savePath;
        [SerializeField, HideIf("_useSavePathFile")] private string _path;
        protected override IEventReader GetEventReader() => GetEventReader(_useSavePathFile ? _savePath.FilePath : _path);

        protected abstract IEventReader GetEventReader(string savePathFilePath);
    }
}