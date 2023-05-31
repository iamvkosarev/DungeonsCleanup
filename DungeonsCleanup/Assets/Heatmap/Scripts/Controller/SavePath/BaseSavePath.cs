using UnityEngine;

namespace Heatmap.Scripts.Controller.SavePath
{
    public abstract class BaseSavePath : ScriptableObject
    {
        public abstract string FilePath { get; }
    }
}