using System;
using System.IO;
using Firebase.Storage;
using UnityEngine;

namespace Heatmap.Scripts.Recorder
{
    internal class FirebaseStorageRecorder : JSONRecorder
    {
        private readonly string _storageFilePath;

        public override void Complete()
        {
            base.Complete();
            SendDataToStorage();
        }

        private void SendDataToStorage()
        {
            Debug.Log($"Send date: {SavePath}");
            var storage = FirebaseStorage.DefaultInstance;
            var storageRef = storage.RootReference;
            var riversRef = storageRef.Child(_storageFilePath);
            riversRef.PutFileAsync(SavePath)
                .ContinueWith(task =>
                {
                    if (task.IsFaulted || task.IsCanceled)
                    {
                        Debug.Log(task.Exception.ToString());
                    }
                    else
                    {
                        var metadata = task.Result;
                        var md5Hash = metadata.Md5Hash;
                        Debug.Log("Finished uploading...");
                        Debug.Log("md5 hash = " + md5Hash);
                        File.Delete(SavePath);
                        Debug.Log($"Deleted local file: {SavePath}");
                    }
                });
            
        }

        public FirebaseStorageRecorder(RecordeSettingContainer recordeSettingContainer, string savePath,
            string storageFilePath, ICoroutineRunner coroutineRunner) : base(recordeSettingContainer, savePath,
            coroutineRunner)
        {
            _storageFilePath = storageFilePath;
        }
    }
}