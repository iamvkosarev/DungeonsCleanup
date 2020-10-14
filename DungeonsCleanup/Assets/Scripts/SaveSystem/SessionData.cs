using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SessionData
{
    public bool[] sessionActivity;
    public bool[] createdSessions;

    public SessionData(bool[] sessionActivity, bool[] createdSessions)
    {
        this.sessionActivity = sessionActivity;
        this.createdSessions = createdSessions;
    }

    public int GetActiveSessionNum()
    {
        int activeSessionNum = -1;
        for(int i = 0; i < sessionActivity.Length; i++)
        {
            if (sessionActivity[i])
            {
                activeSessionNum = i;
                break;
            }
        }
        return activeSessionNum;
    }
}
