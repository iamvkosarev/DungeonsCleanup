using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawExperience : MonoBehaviour
{
    [SerializeField] private GameObject expPrefab;
    [SerializeField] private int amountOfExperience;

    public void SpawnExp()
    {
        int numOfSpawnPoints = Random.Range(1, amountOfExperience);
        int minExpInPoint = (amountOfExperience - amountOfExperience % numOfSpawnPoints) / numOfSpawnPoints;
        int numOfMaxPoints = amountOfExperience % numOfSpawnPoints;
        int numOfMinPoints = numOfSpawnPoints - numOfMaxPoints;
        for(int i = 0; i < numOfMinPoints; i++)
        {
            GameObject point =  Instantiate(expPrefab, transform.position, Quaternion.identity);
            point.GetComponent<Experience>().SetExpAmount(minExpInPoint);
        }
        for (int i = 0; i < numOfMaxPoints; i++)
        {
            GameObject point = Instantiate(expPrefab, transform.position, Quaternion.identity);
            point.GetComponent<Experience>().SetExpAmount(minExpInPoint+1);
        }
    }
}
