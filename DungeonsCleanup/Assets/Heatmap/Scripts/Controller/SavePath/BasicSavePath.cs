using System;
using UnityEngine;

namespace Heatmap.Scripts.Controller.SavePath
{
    [Serializable, CreateAssetMenu(menuName = "Heatmap/Settings/Other/SavePath/Basic")]
    public class BasicSavePath : BaseSavePath
    {
        [SerializeField] private string filePath;
        public override string FilePath => filePath;
    }
}