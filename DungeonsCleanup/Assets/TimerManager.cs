using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [SerializeField] TimerActivator[] timerActivators;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    private bool wasActivated = false;
    private float periodOfCount = 0.01f;
    int seconds = 0;
    int minuts = 0;
    int miliseconds = 0;

    private void OnEnable()
    {
        if (wasActivated) { ActivateTimer(); }
    }
    private void Start()
    {
        foreach(TimerActivator timerActivator in timerActivators)
        {
            timerActivator.OnActivateTimerEvent += OnActivateTimer;
            timerActivator.OnDeactivateTimerEvent += OnDeactivateTimer;
            timerActivator.OnStopTimerEvent += OnStopTimer;
        }
        DisplayTimer();
    }

    private void OnActivateTimer(object obj, EventArgs e)
    {
        if (!wasActivated)
        {
            wasActivated = true;
            ActivateTimer();
        }
    }

    private void ActivateTimer()
    {
        StartCoroutine(CountTimer());
    }
    IEnumerator CountTimer()
    {
        while (wasActivated)
        {
            miliseconds += 1;
            if (miliseconds == 100)
            {
                miliseconds = 0;
                seconds += 1;
            }
            if (seconds == 60)
            {
                seconds = 0;
                minuts += 1;
            }
            DisplayTimer();
            yield return new WaitForSeconds(periodOfCount);
        }
    }
    private void DisplayTimer()
    {
        textMeshProUGUI.text = $"{minuts}:{seconds}:{miliseconds}";
    }
    private void OnDeactivateTimer(object obj, EventArgs e)
    {
        wasActivated = false;
        StopTimer();
    }

    private void StopTimer()
    {
        seconds = 0;
        minuts = 0;
        miliseconds = 0;
        DisplayTimer();
    }

    private void OnStopTimer(object obj, EventArgs e)
    {
        if (wasActivated)
        {
            wasActivated = false;
        }
    }
}
