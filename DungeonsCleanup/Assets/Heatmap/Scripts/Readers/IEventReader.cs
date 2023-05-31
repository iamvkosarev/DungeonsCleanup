using System.Collections.Generic;
using System.Threading.Tasks;

namespace Heatmap.Readers
{
    using Events;
    public interface IEventReader
    {
        Task<List<EventsContainer>> ReadEvents();
    }
}