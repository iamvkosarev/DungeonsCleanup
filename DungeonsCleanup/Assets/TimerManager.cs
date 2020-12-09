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
    private int seconds = 0;
    private int minuts = 0;
    private int miliseconds = 0;
    private CountPortalsManager countPortalsManager;
    float timeOnStart;

    private void OnEnable()
    {
        if (wasActivated) { ActivateTimer(); }
    }
    private void Start()
    {
        countPortalsManager = GetComponent<CountPortalsManager>();
        foreach (TimerActivator timerActivator in timerActivators)
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
            timeOnStart = Time.time;
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
            yield return new WaitForSeconds(periodOfCount);
            DisplayTimer();
        }
    }
    private void DisplayTimer()
    {
        //textMeshProUGUI.text = $"{minuts}:{seconds}:{miliseconds}";
        miliseconds = (int)(Time.time * 100 - timeOnStart * 100)  % 100;
        seconds = (int)(Time.time - timeOnStart) % 60;
        minuts = (int)(Time.time - timeOnStart ) / 60;
        textMeshProUGUI.text = $"{minuts}:{seconds}:{miliseconds}";
    }
    private void OnDeactivateTimer(object obj, EventArgs e)
    {
        wasActivated = false;
        DeactivateTimer();
        if (countPortalsManager)
        {
            countPortalsManager.ZeroPortals();
        }
    }

    private void DeactivateTimer()
    {
        timeOnStart = Time.time;
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
