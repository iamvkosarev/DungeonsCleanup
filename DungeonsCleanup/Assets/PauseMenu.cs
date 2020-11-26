using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] public GameObject pauseMenuUI;
    [SerializeField] public GameObject gamepadUI;
    [SerializeField] public GameObject playerBarsUI;
    [SerializeField] public GameObject loseUI;
    [SerializeField] public GameObject player;
    PlayerMovement playerMovement;
    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        ResumeGame();
    }
    public void PauseGame()
    {
        SwitchOtherCanvases(false);
        pauseMenuUI.SetActive(true);
        if (playerMovement)
        {
            playerMovement.StopRotating();
        }
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        SwitchOtherCanvases(true);
        pauseMenuUI.SetActive(false);
        loseUI.SetActive(false);
        if (playerMovement)
        {
            playerMovement.StartRotaing();
        }
        Time.timeScale = 1f;
    }

    public void SwitchOtherCanvases(bool mode)
    {
        gamepadUI.SetActive(mode);
        playerBarsUI.SetActive(mode);
    }
}
