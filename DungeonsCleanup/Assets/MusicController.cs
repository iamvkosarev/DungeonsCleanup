using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioClip buttonPressSFX;
    [SerializeField] private float audioBoost;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = PlayerPrefsController.GetMasterVolume();
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void SpawnPressButtonSVF()
    {
        audioSource.PlayOneShot(buttonPressSFX, audioBoost);
    }
}
