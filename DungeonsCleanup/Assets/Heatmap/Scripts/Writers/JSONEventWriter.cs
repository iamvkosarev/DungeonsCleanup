using System.IO;
using UnityEngine;

namespace Heatmap.Writers
{
    using Events;
    public class JSONEventWriter : FileEventWriter
    {
        
        public override void SaveEvent(BaseEvent baseEvent)
        {
            base.SaveEvent(baseEvent);

            using var writer = new StreamWriter(Path, true);
            try
            {
                writer.WriteLine(JsonUtility.ToJson(baseEvent));

            }
            catch (System.Exception e)
            {
                Debug.Log(e.ToString());

            }
        }

        public JSONEventWriter(string path) : base(path)
        {
        }
    }
}