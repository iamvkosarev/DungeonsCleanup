using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivationBoss : MonoBehaviour
{
    [SerializeField] private GoblinBossMovement goblinBoss;
    [SerializeField] private GameObject bossCanvas;
    [SerializeField] private PlayerSoundManager playerSoundManager;
    [SerializeField] private AudioClip newBackTheme;
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
            playerSoundManager.SetBackTheme(newBackTheme);
            Destroy(gameObject);
        }
    }
}
