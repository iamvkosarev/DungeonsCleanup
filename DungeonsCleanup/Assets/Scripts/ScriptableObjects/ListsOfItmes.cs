using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Lists Of Items")]
public class ListsOfItmes : ScriptableObject
{
    [SerializeField] private TimeCrystalData[] timeCrystalDatas;
    [SerializeField] private ArtifactData[] artifactDatas;

    public ArtifactData GetArtifactData(int id)
    {
        if (id >= artifactDatas.Length || id < 0)
        {
            return null;
        }
        return artifactDatas[id];
    }
    public TimeCrystalData GetTimeCrystalData(int id)
    {
        if (id >= timeCrystalDatas.Length)
        {
            return null;
        }
        return timeCrystalDatas[id];
    }
}
