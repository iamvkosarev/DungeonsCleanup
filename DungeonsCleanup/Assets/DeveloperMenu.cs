using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperMenu : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject developMenuUI;
    [SerializeField] private GameObject gamepadUI;
    [SerializeField] private GameObject playerBarsUI;

    PlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        CloseDevelopMenu();
    }
    public void OpenDevelopMenu()
    {
        SwitchOtherCanvases(false);
        developMenuUI.SetActive(true);
        if (playerMovement)
        {
            playerMovement.StopRotating();
        }
        Time.timeScale = 0f;
    }

    public void CloseDevelopMenu()
    {
        SwitchOtherCanvases(true);
        developMenuUI.SetActive(false);
        if (playerMovement)
        {
            playerMovement.StartRotaing();
        }
        Time.timeScale = 1f;
    }

    private void SwitchOtherCanvases(bool mode)
    {
        gamepadUI.SetActive(mode);
        playerBarsUI.SetActive(mode);
    }
}
