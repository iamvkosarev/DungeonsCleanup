using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private int nextSceneBuildIndex;
    [SerializeField] private float dalayBeforeStart = 1f;
    [SerializeField] private bool isCheckPoint = false;
    [SerializeField] private GameObject canvas;
    [SerializeField] private PlayerDataManager playerDataManager;
    private Animator myAnimator;
    private AudioSource myAudioSource;
    private int currentSceneIndex;
    enum FollowingState
    {
        NextScene,
        MainMenu
    }
    private FollowingState followingState;
    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        canvas.active = true;
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
        canvas.active = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetFollowingState(FollowingState.NextScene);
        canvas.active = true;
        myAudioSource.Play();
        myAnimator.SetTrigger("StartExitCrossfade");
    }
    public void LoadMainMenuFromGameScene()
    {
        SetFollowingState(FollowingState.MainMenu);
        canvas.active = true;
        myAnimator.SetTrigger("StartExitCrossfade");
    }
    private void SetFollowingState(FollowingState followingState)
    {
        this.followingState = followingState;
    }

    public void LoadScene()
    {
        Debug.Log(followingState);
        if (followingState == FollowingState.NextScene)
        {
            SceneManager.LoadScene(nextSceneBuildIndex);
        }
        else if(followingState == FollowingState.MainMenu)
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
    public void RefreshCurrentSessionData()
    {
        if (playerDataManager != null && followingState == FollowingState.NextScene)
        {

            playerDataManager.RefreshLastSessionData(setNewSceneNum: true, newStartSceneNum: nextSceneBuildIndex);
            if (isCheckPoint)
            {
                playerDataManager.RefreshCheckPointSessionData(setNewSceneNum: true, newStartSceneNum: nextSceneBuildIndex);
            }
        }
        else if (playerDataManager != null && followingState == FollowingState.MainMenu)
        {
            //playerDataManager.RefreshLastSessionData(setNewSceneNum: true, newStartSceneNum: SceneManager.GetActiveScene().buildIndex);
        }
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

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadSavingMenu()
    {
        SceneManager.LoadScene("SavingMenu");
    }

    public void LoadLastScene()
    {
        SceneManager.LoadScene("The End");
    }
}
