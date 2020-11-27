using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="List Of Shadows")]
public class ListOfAllShadows : ScriptableObject
{
    [SerializeField] private GameObject[] shadowsPrefabs;

    public GameObject GetShadow(int shadowIndex)
    {
        if (shadowsPrefabs.Length >= shadowIndex || shadowIndex < 0)
        {
            return null;
        }
        else
        {
            return shadowsPrefabs[shadowIndex];
        }
    }
}
