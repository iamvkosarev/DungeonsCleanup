using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuCanvas;
    [SerializeField] private GameObject settingsCanvas;


    private void Start() 
    {
        menuCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
    }

    public void SetMenuCanvas()
    {
        settingsCanvas.SetActive(false);
        menuCanvas.SetActive(true);
    }

    public void SetSettingsCanvas()
    {
        settingsCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }
    
    
}
