using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Bat Wave Config")]
public class WaveConfig : ScriptableObject
{
    //[SerializeField] GameObject batPrefab;
    [SerializeField] GameObject pathPrefab;
    //[SerializeField] float moveSpeed = 2f;

    //public GameObject GetEnemyPrefab() { return batPrefab; }

    public List<Transform> GetWaypoints() 
    { 
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in pathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }

    //public float GetMoveSpeed() { return moveSpeed; }

}
