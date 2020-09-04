using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="FrameList")]
public class FramesList : ScriptableObject
{
    [SerializeField] List<Sprite> frames;

    public Sprite GetFrame(int numOfFrame)
    {
        if (frames.Count <= numOfFrame)
        {
            Debug.Log($"Frame list hasn't {numOfFrame} num of frame");
        }
        return frames[numOfFrame];
    }
}
