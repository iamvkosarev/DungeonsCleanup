using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class DropSpawner : MonoBehaviour
{
    [SerializeField] GameObject dropPrfab;
    [SerializeField] Transform[] motionCoordinates;
    [SerializeField] bool repeatDrop = true;
    [SerializeField] float minTimeOnDalay, maxTimeOnDalay;
    
    IEnumerator Start()
    {
        do
        {
            GameObject newDrop = Instantiate(dropPrfab, motionCoordinates[0].position, Quaternion.identity) as GameObject;
            Drop newDropScript = newDrop.GetComponent<Drop>();
            newDropScript.SetCoordinates(motionCoordinates);
            yield return new WaitForSeconds(Random.RandomRange(minTimeOnDalay, maxTimeOnDalay));
        }
        while (repeatDrop);
    }
}
