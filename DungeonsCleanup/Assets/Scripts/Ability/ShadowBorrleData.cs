using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShadowBorrleData
{
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

    public int GetShadowId()
    {
        for (int i = 0; i < listOfShadows.Length; i++)
        {
            if (listOfShadows[i] != -1)
            {
                int result = listOfShadows[i];
                listOfShadows[i] = -1;
                return result;
            }
        }
        return -1;
    }

}
