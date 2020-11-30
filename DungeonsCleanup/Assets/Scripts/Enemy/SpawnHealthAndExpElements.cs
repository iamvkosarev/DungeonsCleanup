using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHealthAndExpElements : MonoBehaviour
{
    [SerializeField] private GameObject elementPrefab;
    [SerializeField] private int amountOfElement;
    [SerializeField] private int maxPointsToSpawn = 5;
    [SerializeField] private bool hasRandom = false;

    public void Spawn()
    {
        if (hasRandom)
        {
            amountOfElement = Random.Range(1, amountOfElement);
        }
        int numOfSpawnPoints = Random.Range(1, maxPointsToSpawn);
        int minElementInPoint = (amountOfElement - amountOfElement % numOfSpawnPoints) / numOfSpawnPoints;
        int numOfMaxPoints = amountOfElement % numOfSpawnPoints;
        int numOfMinPoints = numOfSpawnPoints - numOfMaxPoints;
        for (int i = 0; i < numOfMinPoints; i++)
        {
            GameObject point = Instantiate(elementPrefab, transform.position, Quaternion.identity);
            point.GetComponent<ExperienceAndHP>().SetAmount(minElementInPoint);
        }
        for (int i = 0; i < numOfMaxPoints; i++)
        {
            GameObject point = Instantiate(elementPrefab, transform.position, Quaternion.identity);
            point.GetComponent<ExperienceAndHP>().SetAmount(minElementInPoint + 1);
        }
    }

}
