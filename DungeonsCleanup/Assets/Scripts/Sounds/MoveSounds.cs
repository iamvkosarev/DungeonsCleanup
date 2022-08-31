using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSounds : MonoBehaviour
{
    [SerializeField] float timeOnUpdatePos = 0.2f;
    [SerializeField] AudioClip moveSound;
    [SerializeField] float timeOnCutEnd = 0.2f;
    private Vector3 lastPos;
    private bool startedPlay = false;
    private AudioSource myAudioSource;

    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        if(myAudioSource && moveSound)
        {
            myAudioSource.clip = moveSound;
            StartCoroutine(UpdateCheckPos());
        }
    }
    IEnumerator UpdateCheckPos()
    {
        while (true)
        {
            lastPos = transform.position;
            yield return new WaitForSeconds(timeOnUpdatePos);
        }
    }
    void Update()
    {
        CheckMoving();
    }
    private void CheckMoving()
    {
        if (!myAudioSource) { return; }
        if (lastPos != transform.position)
        {
            if (!startedPlay)
            {
                myAudioSource.Play();
                startedPlay = true;
                StartCoroutine(CuttingAfterDelay());
            }
        }
        else
        {
            startedPlay = false;
            myAudioSource.Stop();
        }
        }
    IEnumerator CuttingAfterDelay()
    {
        yield return new WaitForSeconds(moveSound.length - timeOnCutEnd);
        myAudioSource.Stop();
        startedPlay = false;
    }
}
