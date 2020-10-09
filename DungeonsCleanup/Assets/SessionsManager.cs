using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionsManager : MonoBehaviour
{
    [SerializeField] SessionForm[] sessionForms;
    private int selectedSessionForm = -1;

    private void Update()
    {
        CheckFormsOnSelected();
        LoadCreatedAndDoesntSession();
    }

    public void LoadCreatedAndDoesntSession()
    {
        bool[] createdSessions = SaveSystem.LoadSession().createdSessions;
        for (int i = 0; i < sessionForms.Length; i++)
        {
            if (createdSessions[i])
            {
                sessionForms[i].SetSessionCreated();
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
