using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CountPortalsManager : MonoBehaviour
{
    [SerializeField] private PortalCounter[] portalCounters;
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    private int numOfPortals = 0;

    private void Start()
    {
        foreach(PortalCounter portalCounter in portalCounters)
        {
            portalCounter.OnPortalCountEvent += CountPortal;
        }
    }
    public void ZeroPortals()
    {
        numOfPortals = 0;
        DisplayPortals();
    }
    private void CountPortal(object obj, EventArgs e)
    {
        Debug.Log("Посчитать портал!");
        numOfPortals += 1;
        DisplayPortals();
    }
    private void DisplayPortals()
    {
        textMeshProUGUI.text = $"{numOfPortals}";
    }
}
