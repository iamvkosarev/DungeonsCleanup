using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Heatmap.Scripts.Controller.SavePath
{
    [Serializable, CreateAssetMenu(menuName = "Heatmap/Settings/Other/SavePath/SceneDependentSavePath")]
    public class SceneDependentSavePath : BaseSavePath
    {
        [SerializeField] private string filePath;

        public override string FilePath => $"{SceneManager.GetActiveScene().name}/{filePath}";
    }
}