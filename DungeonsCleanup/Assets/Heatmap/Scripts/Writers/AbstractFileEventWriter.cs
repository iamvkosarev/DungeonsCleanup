
using System.IO;
using UnityEngine;

namespace Heatmap.Writers
{
    using Events;
    public abstract class FileEventWriter : IEventWriter
    {
        private readonly string path;

        protected string Path => path;

        protected FileEventWriter(string path)
        {
            this.path = path;
            Prepare();
        }

        private void Prepare()
        {
            if (File.Exists(path)) return;
            CreateFolderAndFile();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public virtual void SaveEvent(BaseEvent baseEvent)
        {
            if (!File.Exists(Path))
            {
                Debug.LogError($"Path not exist: {Path}");
            }
        }

        private void CreateFolderAndFile()
        {
            var directoryPath = System.IO.Path.GetDirectoryName(path);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            File.Create(path).Close();
        }
    }
}