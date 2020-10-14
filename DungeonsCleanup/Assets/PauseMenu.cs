using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject gamepadUI;
    private void Start()
    {
        ResumeGame();
    }
    public void PauseGame()
    {
        gamepadUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        gamepadUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
