using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionsManager : MonoBehaviour
{
    [SerializeField] SessionForm[] sessionForms;
    private int selectedSessionForm = -1;
    private void Start()
    {
        DeactivateAllSessions();
    }
    private void Update()
    {
        CheckFormsOnSelected();
        LoadCreatedAndDoesntSession();
    }
    public void DeactivateAllSessions()
    {
        SessionData data =  SaveSystem.LoadSession();
        for(int i = 0; i< data.sessionActivity.Length;i++)
        {
            data.sessionActivity[i] = false;
        }
        SaveSystem.SaveSession(data.sessionActivity, data.createdSessions);
    }

    public void LoadCreatedAndDoesntSession()
    {
        bool[] createdSessions = SaveSystem.LoadSession().createdSessions;
        for (int i = 0; i < sessionForms.Length; i++)
        {
            if (createdSessions[i])
            {
                sessionForms[i].SetSessionCreated(true);
            }
            else
            {
                sessionForms[i].SetSessionCreated(false);
            }
        }
    }
    private void CheckFormsOnSelected()
    {
        for(int i = 0; i < sessionForms.Length; i++)
        {
            if (sessionForms[i].IsSessionSelected())
            {
                if (selectedSessionForm >= 0 && selectedSessionForm!=i) {
                    sessionForms[selectedSessionForm].DeselectSession();
                    selectedSessionForm = i;
                }
                else if(selectedSessionForm < 0)
                {
                    selectedSessionForm = i;
                }
            }

        }
    }

    public void DeselectAllSessionForms()
    {
        foreach(SessionForm sessionForm in sessionForms)
        {
            sessionForm.DeselectSession();
        }
    }
}
