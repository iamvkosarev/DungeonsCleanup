using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassSounds : MonoBehaviour
{
    [SerializeField] private AudioClip[] grassSounds;
    [SerializeField] private float audioBoost = 0.3f;
    private AudioSource myAudioSource;
    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int randomSound = UnityEngine.Random.Range(0, grassSounds.Length);
        myAudioSource.PlayOneShot(grassSounds[randomSound], audioBoost);
    }
}
