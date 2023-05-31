using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Test Heatmap/Player Recorde Data")]
public class PlayerHeatmapRecordeData : ScriptableObject
{
    [SerializeField] private string playerName;
    [SerializeField] private List<int> locationsRecordeNumber = new();

    public string PlayerName => playerName;

    private List<int> LocationsRecordeNumber => locationsRecordeNumber;

    public int GetRecordeNumber(int levelIndex)
    {
        AddLevelIfDoesntExist(levelIndex);
        return LocationsRecordeNumber[levelIndex];
    }

    private void AddLevelIfDoesntExist(int levelIndex)
    {
        while (LocationsRecordeNumber.Count <= levelIndex)
        {
            LocationsRecordeNumber.Add(0);
        }
    }

    public void AddRecordeNumber(int levelIndex)
    {
        AddLevelIfDoesntExist(levelIndex);
        LocationsRecordeNumber[levelIndex]++;
    }
}