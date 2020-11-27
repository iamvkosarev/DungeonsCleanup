using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShadowBorrleData
{
    [SerializeField] private ListOfAllShadows listOfAllShadows;
    public int[] listOfShadows;

    public ShadowBorrleData(int[] listOfShadows, bool setNullShadown = true)
    {
        this.listOfShadows = listOfShadows;
        if (setNullShadown)
        {
            SetNullShadows();
        }
    }
    public void SetNullShadows()
    {
        for(int i = 0; i < listOfShadows.Length; i++)
        {
            listOfShadows[i] = -1;
        }
    }
    public bool AddShadow(int newShadowId)
    {
        bool wasAdded = false;
        for (int i = 0; i < listOfShadows.Length; i++)
        {
            if (listOfShadows[i] == -1)
            {
                listOfShadows[i] = newShadowId;
                wasAdded = true;
                break;
            }
        }
        return wasAdded;
    }
    public bool HasShadows()
    {
        bool hasShadows = false;
        for (int i = 0; i < listOfShadows.Length; i++)
        {
            if (listOfShadows[i] != -1)
            {
                hasShadows = true;
                break;
            }
        }
        return hasShadows;
    }

    public GameObject GetShadow()
    {
        for (int i = 0; i < listOfShadows.Length; i++)
        {
            if (listOfShadows[i] != -1)
            {
                listOfAllShadows.GetShadow(listOfShadows[i]);
                listOfShadows[i] = -1;
                break;
            }
        }
        return null;
    }

}
