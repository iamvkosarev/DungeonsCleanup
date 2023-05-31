namespace Heatmap.Writers
{
    using Events;

    public interface IEventWriter
    {
        void SaveEvent(BaseEvent baseEvent);
    }
    
}