using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] float defaultVolume = 0.7f;

    private void Start() 
    {

        volumeSlider.value = defaultVolume;

    }

    // Update is called once per frame
    private void Update()
    {
        var musicPlayer = FindObjectOfType<MusicController>();
        if(musicPlayer)
        {
            musicPlayer.SetVolume(volumeSlider.value);
        }

        else
        {
            Debug.LogWarning("some problems with musicPlayer");
        }
    }

    public void SetDefaultVolume()
    {
        volumeSlider.value = defaultVolume;
    }
}
