using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] private Toggle useJoystickToggle;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider gamepadScaleSlider;
    [SerializeField] private Slider gamepadXPosSlider;
    [SerializeField] private Slider gamepadYPosSlider;
    [SerializeField] private Slider gamepadAlphaSlider;
    [SerializeField] private float defaultVolume = 0.7f;
    private SettingsData settingsData;
    private void Start()
    {
        //settingsData = SaveSystem.LoadSettings();
        //gamepadAlphaSlider.value = settingsData.alphaChannelParam;
        volumeSlider.value = defaultVolume;

    }

    // Update is called once per frame
    private void Update()
    {
        //settingsData = SaveSystem.LoadSettings();
        var musicPlayer = FindObjectOfType<MusicController>();
        if(musicPlayer)
        {
            //musicPlayer.SetVolume(volumeSlider.value);
        }

        else
        {
            Debug.LogWarning("some problems with musicPlayer");
        }
        //CheckTypeJoystickToggle();
        /*CheckScaleGamepadSlider();
        CheckAlphaGamepadSlider();
        CheckPosXGamepadSlider();
        CheckPosYGamepadSlider();*/
    }

    /*private void CheckScaleGamepadSlider()
    {
        float currentValue = settingsData.scaleParam;
        if(gamepadScaleSlider.value != currentValue)
        {
            SaveSystem.SaveSettings(settingsData.useJoystick, gamepadScaleSlider.value, settingsData.posXParam, settingsData.posYParam, settingsData.alphaChannelParam);
        }
    }
    private void CheckPosXGamepadSlider()
    {
        float currentValue = settingsData.posXParam;
        if (gamepadXPosSlider.value != currentValue)
        {
            SaveSystem.SaveSettings(settingsData.useJoystick, settingsData.scaleParam, gamepadXPosSlider.value, settingsData.posYParam, settingsData.alphaChannelParam);
        }
    }
    private void CheckPosYGamepadSlider()
    {
        float currentValue = settingsData.posYParam;
        if (gamepadYPosSlider.value != currentValue)
        {
            SaveSystem.SaveSettings(settingsData.useJoystick, settingsData.scaleParam, settingsData.posXParam, gamepadYPosSlider.value, settingsData.alphaChannelParam);
        }
    }
    private void CheckAlphaGamepadSlider()
    {
        float currentValue = settingsData.alphaChannelParam;
        if (gamepadAlphaSlider.value != currentValue)
        {
            SaveSystem.SaveSettings(settingsData.useJoystick, settingsData.scaleParam, settingsData.posXParam, settingsData.posYParam, gamepadAlphaSlider.value);
        }
    }

    private void CheckTypeJoystickToggle()
    {
        bool currentMode = settingsData.useJoystick;
        if (useJoystickToggle.isOn != currentMode)
        {
            SaveSystem.SaveSettings(useJoystickToggle.isOn, settingsData.scaleParam, settingsData.posXParam, settingsData.posYParam, settingsData.alphaChannelParam);
        }
    }

    public void SetDefaultSettings()
    {
        volumeSlider.value = defaultVolume;
        settingsData = SaveSystem.SetDefaultSettings();
        useJoystickToggle.isOn = settingsData.useJoystick;
        gamepadScaleSlider.value = settingsData.scaleParam;
        gamepadXPosSlider.value = settingsData.posXParam;
        gamepadYPosSlider.value = settingsData.posYParam;
        gamepadAlphaSlider.value = settingsData.alphaChannelParam;
        volumeSlider.value = defaultVolume;
    }*/
}
