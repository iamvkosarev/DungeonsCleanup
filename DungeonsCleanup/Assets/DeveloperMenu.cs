using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DeveloperMenu : MonoBehaviour
{
    [Header("Manage with Canvases")] 
    [SerializeField] private GameObject developMenuUI;
    [SerializeField] private GameObject gamepadUI;
    [SerializeField] private GameObject playerBarsUI;
    [Header("Load Player's Data")]
    [SerializeField] private GameObject player;
    [SerializeField] private TextMeshProUGUI currentExpTextForm;
    [SerializeField] private TextMeshProUGUI lvlForm;
    [SerializeField] private Slider expSlider;
    [SerializeField] private TextMeshProUGUI needExpForm;
    [SerializeField] private TextMeshProUGUI hpForm;
    [SerializeField] private TextMeshProUGUI damageForm;


    PlayerMovement playerMovement;
    PlayerDevelopmentManager playerDevManager;
    PlayerHealth playerHealth;
    private void Start()
    {
        CloseDevelopMenu();
    }
    public void OpenDevelopMenu()
    {
        SwitchOtherCanvases(false);
        LoadDataInForms();
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

    #region Load Data

    private void LoadDataInForms()
    {
        if (!playerMovement)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
        if (!playerDevManager)
        {
            playerDevManager = player.GetComponent<PlayerDevelopmentManager>();
        }
        if (!playerHealth)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
        LoadDataAboutLvl();
        LoadDataAboutExp();
        LoadDataAboutHealthAndDamage();
    }


    private void LoadDataAboutLvl()
    {
        lvlForm.text = $"Lvl: {playerDevManager.GetCurrentLvl()}";
    }
    private void LoadDataAboutExp()
    {
        currentExpTextForm.text = playerDevManager.GetCurrentExp().ToString();
        needExpForm.text = playerDevManager.GetNeedExp().ToString();
        expSlider.maxValue = playerDevManager.GetNeedExp();
        expSlider.value = playerDevManager.GetCurrentExp();
    }
    private void LoadDataAboutHealthAndDamage()
    {
        hpForm.text = $"{playerHealth.GetHealth()} / {playerDevManager.GetMaxHealthAccordingLvl()}";
        damageForm.text = playerDevManager.GetDamageAccordingLvl().ToString();
    }
    #endregion
}
