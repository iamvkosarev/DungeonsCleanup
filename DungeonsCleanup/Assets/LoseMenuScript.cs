using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class LoseMenuScript : PauseMenu
{
    [SerializeField] private float delayBeforeRelife = 1.5f;
    PlayerHealth playerHealth;
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
        Debug.Log("WHERE IS A MISTAKE???");
        if(Advertisement.IsReady())
        {
            Advertisement.Show("rewardedVideo");
            //Time.timeScale = 0;
        }

        Debug.Log("YOU HAVE FULL HP NOW!");

        playerHealth.GiveHalfHP();
        playerAnimation.StartReLife(delayBeforeRelife);
        CloseLoseCanvas();
    }

    public void UpdateLastCheckpoint()
    {
        playerHealth.gameObject.GetComponent<PlayerDataManager>().SetCheckPointSessionData();
    }




    
}
