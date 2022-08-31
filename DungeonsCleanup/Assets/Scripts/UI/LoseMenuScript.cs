using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Advertisements;

public class LoseMenuScript : PauseMenu
{
    PlayerHealth playerHealth;
    public EventHandler OnPlayerRelife;
    private PlayerAnimation playerAnimation;
    private void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();
        playerAnimation = player.GetComponent<PlayerAnimation>();

        //ads
        if(Advertisement.isSupported)
        {
            Advertisement.Initialize("3912667", false);
        }
    }

    public void SetLoseCanvas()
    {
        gamepadUI.SetActive(false);
        playerBarsUI.SetActive(false);
        pauseMenuUI.SetActive(false);

        loseUI.SetActive(true);
    }
    public void CloseLoseCanvas()
    {
        gamepadUI.SetActive(true);
        playerBarsUI.SetActive(true);

        loseUI.SetActive(false);

    }

    public void PlayAdvirtisement()
    {
        if(Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo");
            if(OnPlayerRelife != null)
            {
                OnPlayerRelife.Invoke(this, EventArgs.Empty);
            }
            CloseLoseCanvas();
        }

        else
        {
            Debug.Log("There isn't internet connection...");
        }
    }

    public void UpdateLastCheckpoint()
    {
        playerHealth.gameObject.GetComponent<PlayerDataManager>().SetLastSessionData();
    }


    public void ManagePlayerBarsAndGamepad(bool mood)
    {
        playerBarsUI.SetActive(mood);
        gamepadUI.SetActive(mood);
    }

    public void ManagePlayerGamepad(bool mood)
    {
        gamepadUI.SetActive(mood);
    }

}
