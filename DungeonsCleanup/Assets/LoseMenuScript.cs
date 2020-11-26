using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class LoseMenuScript : PauseMenu
{
    PlayerHealth playerHealth;
    private void Start()
    {
        playerHealth = player.GetComponent<PlayerHealth>();

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

    public void PlayAdvirtisement()
    {
        Debug.Log("WHERE IS A MISTAKE???");
        if(Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo");
            //Time.timeScale = 0;
        }

        Debug.Log("YOU HAVE FULL HP NOW!");
        playerHealth.gameObject.GetComponent<PlayerDataManager>().SetCheckPointSessionData();
    }

    public void UpdateLastCheckpoint()
    {
        playerHealth.gameObject.GetComponent<PlayerDataManager>().SetCheckPointSessionData();
    }




    
}
