namespace Heatmap.Controller
{
    using Visualisation;
    using Readers;

    public sealed class JsonHeatmapController : FileHeatmapController
    {
        private HeatmapVisualisation heatmapVisualisation;
        private IEventReader eventReader;
        protected override IEventReader GetEventReader(string savePathFilePath) => new JSONEventReader(savePathFilePath);

    }
}