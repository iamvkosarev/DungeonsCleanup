using Heatmap.Readers;

namespace Heatmap.Controller
{
    public class FirebaseHeatmapController : FileHeatmapController
    {
        protected override IEventReader GetEventReader(string savePathFilePath) => new FirebaseStorageEventReader(savePathFilePath);
    }
}