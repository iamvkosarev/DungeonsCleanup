using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] string nextSceneName;
    [SerializeField] float dalayBeforeStart = 1f;
    [SerializeField] GameObject canvas;
    Animator myAnimator;
    int currentSceneIndex;
    private void Start()
    {
        canvas.SetActive(true);
        myAnimator = GetComponent<Animator>();
        StartCoroutine(LoadingStartCrossfade());
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    IEnumerator LoadingStartCrossfade()
    {
        yield return new WaitForSeconds(dalayBeforeStart);
        myAnimator.SetTrigger("StartStartCrossfade");
    }
    public void SwitchOffCanvas()
    {
        canvas.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvas.SetActive(true);
        myAnimator.SetTrigger("StartExitCrossfade");
    }
    public void LoadScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void LoadSavingMenu()
    {
        SceneManager.LoadScene("SavingMenu");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
