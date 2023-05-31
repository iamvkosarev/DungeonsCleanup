using System.Threading.Tasks;

namespace Heatmap.Scripts.Recorder
{
    public interface IRecorder
    {
        void Play();
        void Pause();
        void Complete();
        void Break();
    }
}