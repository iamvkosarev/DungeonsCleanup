using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSpawn : MonoBehaviour
{
    [SerializeField] private GameObject pathPrefab;
    [Header("Bats")]
    [SerializeField] private GameObject batPrefab;
    [SerializeField] private int numberOfBats;
    [SerializeField] private float distanceToAttack = 10f;


    private void Start()
    {
        int i = 0;
        while(i < numberOfBats)
        {
            SpawnBat();
            i++;
        }  
    }

    private void SpawnBat()
    {
        Instantiate(batPrefab, gameObject.transform);
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

    public float GetDistanceToAttack()
    {
        return distanceToAttack;
    }
}
