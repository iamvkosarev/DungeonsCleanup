using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawn : MonoBehaviour
{
    [SerializeField] GameObject pathPrefab;
    [SerializeField] GameObject batPrefab;
    [SerializeField] int numberOfBats;


    private void Start()
    {
        int i = 0;
        while(i < numberOfBats)
        {
            SpawnBat();
            i++;
        }  
    }
    public List<Transform> GetWaypoints()
    { 
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }

    private void SpawnBat()
    {
        Instantiate(batPrefab, gameObject.transform);
    }
}
