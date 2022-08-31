using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] backThemes;
    private AudioSource audioSource;
    private int currentSceneIndex;
    private void Start()
    {

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SwitchBackTheme();
    }

    private void SwitchBackTheme()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = ChooseBackTheme();
        if (currentSceneIndex != 0)
        {
            audioSource.Play();
        }
    }
    public void SetBackTheme(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    } 

    private AudioClip ChooseBackTheme()
    {
        if (currentSceneIndex >= 1)
        {
            return backThemes[0];
        }
        return null;
    }
}
