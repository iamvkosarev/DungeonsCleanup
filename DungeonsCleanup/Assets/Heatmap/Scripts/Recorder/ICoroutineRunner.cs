using System.Collections;
using UnityEngine;

namespace Heatmap.Scripts.Recorder
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine routine);
    }
}