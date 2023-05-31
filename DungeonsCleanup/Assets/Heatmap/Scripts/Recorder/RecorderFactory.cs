using UnityEngine;

namespace Heatmap.Scripts.Recorder
{
    public class RecorderFactory : MonoBehaviour, ICoroutineRunner
    {
        private static RecorderFactory _instance;

        public static RecorderFactory Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = FindObjectOfType<RecorderFactory>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    _instance = singletonObject.AddComponent<RecorderFactory>();
                    singletonObject.name = typeof(RecorderFactory) + " (Singleton)";
                    DontDestroyOnLoad(singletonObject);
                }

                return _instance;
            }
        }

        public IRecorder GetJSONRecorder(RecordeSettingContainer recordeSettingContainer, string savePath) =>
            new JSONRecorder(recordeSettingContainer, savePath, this);

        public IRecorder GetFirebaseRecorder(RecordeSettingContainer recordeSettingContainer, string jsonLocalSavePath,
            string storageFilePath) =>
            new FirebaseStorageRecorder(recordeSettingContainer, jsonLocalSavePath, storageFilePath, this);
    }
}