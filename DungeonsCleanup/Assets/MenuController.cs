using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject settingsMenuCanvas;
    [SerializeField] private GameObject sessionsMenuCanvas;


    private void Start() 
    {
        SetMenuCanvas();
    }

    public void SetMenuCanvas()
    {
        SwitchOffAllCanvases();
        mainMenuCanvas.SetActive(true);
    }

    public void SetSettingsCanvas()
    {
        SwitchOffAllCanvases();
        settingsMenuCanvas.SetActive(true);
    }
    public void SetSessionCanvas()
    {
        SwitchOffAllCanvases();
        sessionsMenuCanvas.SetActive(true);
    }
    
    public void SwitchOffAllCanvases()
    {
        mainMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(false);
        sessionsMenuCanvas.SetActive(false);
    }
}
