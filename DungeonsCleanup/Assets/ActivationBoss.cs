using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationBoss : MonoBehaviour
{
    [SerializeField] private GoblinBossMovement goblinBoss;
    [SerializeField] private GameObject bossCanvas;
    [SerializeField] private PlayerSoundManager playerSoundManager;
    [SerializeField] private AudioClip newBackStartTheme;
    [SerializeField] private AudioClip newBackIdleTheme;
    [SerializeField] private AudioClip baseBackTheme;
    private void Start()
    {
        bossCanvas.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            goblinBoss.GetComponent<Animator>().SetTrigger("Start");
            bossCanvas.SetActive(true);
            StartCoroutine(SwitchingBackTheme());
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator  SwitchingBackTheme()
    {
        playerSoundManager.SetBackTheme(newBackStartTheme);
        yield return new WaitForSeconds(newBackStartTheme.length);
        playerSoundManager.SetBackTheme(newBackIdleTheme);
    }

    public void BossDeath()
    {
        playerSoundManager.SetBackTheme(baseBackTheme);
        bossCanvas.SetActive(false);
    }
}
