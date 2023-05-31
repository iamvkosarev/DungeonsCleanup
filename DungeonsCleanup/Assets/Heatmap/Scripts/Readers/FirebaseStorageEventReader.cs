using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Firebase.Extensions;
using Firebase.Storage;
using Heatmap.Events;
using UnityEngine;

namespace Heatmap.Readers
{
    public class FirebaseStorageEventReader : IEventReader
    {
        private readonly string _path;

        public FirebaseStorageEventReader(string path)
        {
            _path = path;
        }

        public async Task<List<EventsContainer>> ReadEvents()
        {
            Dictionary<string, EventsContainer> containersDict = new();

            var storage = FirebaseStorage.DefaultInstance;
            var storageRef = storage.RootReference;
            var jsonRef = storageRef.Child(_path);
            const long maxAllowedSize = 100 * 1024 * 1024;
            var isLoaded = false;
            var result = false;
            jsonRef.GetBytesAsync(maxAllowedSize).ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted || task.IsCanceled)
                {
                    Debug.LogError($"Failed to download JSON file from Firebase Storage path: {_path}");
                    isLoaded = true;
                    return;
                }

                var jsonContent = System.Text.Encoding.Default.GetString(task.Result);

                var lines = jsonContent.Split('\n').ToList();
                lines.RemoveAt(lines.Count-1);

                foreach (var line in lines)
                {
                    var baseEvent = JsonUtility.FromJson<BaseEvent>(line);

                    if (baseEvent.EventName != null)
                    {
                        AddBaseEventToContainer(baseEvent, containersDict);
                    }
                    else
                    {
                        Debug.Log("line is invalid : " + line);
                    }
                }

                result = true;
                isLoaded = true;
            });
            while (!isLoaded)
            {
                await Task.Delay(10);
            }

            return result ? new List<EventsContainer>(containersDict.Values) :new List<EventsContainer>();
        }

        private void AddBaseEventToContainer(BaseEvent baseEvent, IDictionary<string, EventsContainer> containersDict)
        {
            if (!containersDict.TryGetValue(baseEvent.EventName, out var container))
            {
                container = new EventsContainer(baseEvent.EventName);
                containersDict.Add(baseEvent.EventName, container);
            }

            MultipliedEventPosition eventPosition = new(baseEvent.Position);

            container.Positions.Add(eventPosition);
        }
    }
}