using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsSounds : MonoBehaviour
{
    [SerializeField] private AudioClip pressButtonSound;
    [SerializeField] float audioBoost = 1;

    public void PlayPressButtonSFX()
    {
        GameObject newGO = new GameObject();
        AudioSource audioSource = newGO.AddComponent<AudioSource>();
        audioSource.PlayOneShot(pressButtonSound, audioBoost);
        Destroy(newGO, pressButtonSound.length);
    }
}
