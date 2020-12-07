using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class LoseMenuScript : PauseMenu
{
    [SerializeField] private float delayBeforeRelife = 1.5f;
    [SerializeField] private GameObject adsIsntReady;
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
        Debug.Log("im here");
        if(Advertisement.IsReady())
        {
            Debug.Log("isReady");
            Advertisement.Show("rewardedVideo");
            playerHealth.GiveMaxHP();
            playerAnimation.StartReLife(delayBeforeRelife);
            CloseLoseCanvas();
        }

        else
        {
            Instantiate(adsIsntReady);
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
