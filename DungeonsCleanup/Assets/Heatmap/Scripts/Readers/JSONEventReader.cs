using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace Heatmap.Readers
{
    using Events;
    
    public class JSONEventReader : IEventReader
    {
        private readonly string path;

        public JSONEventReader(string path)
        {
            this.path = path;
            if (!File.Exists(path))
            {
                Debug.LogError("Invalid path, no file found: " + path);
            }
        }
        public async Task<List<EventsContainer>> ReadEvents()
        {
            Dictionary<string, EventsContainer> containersDict = new();

            using (var fileStream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (BufferedStream bs = new(fileStream))
            using (StreamReader sr = new(bs))
            {
                while (sr.ReadLine() is { } line)
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
            }

            return new List<EventsContainer>(containersDict.Values);
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