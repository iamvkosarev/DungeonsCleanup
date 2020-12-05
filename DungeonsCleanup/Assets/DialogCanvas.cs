using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class DialogCanvas : MonoBehaviour
{
    [Header("Manage Properties")]
    [SerializeField] private GameObject blackLinesForm;
    [SerializeField] private CinemachineVirtualCamera cvCamera;
    [SerializeField] private GameObject player;
    [Space]
    [Header("Dialog")]
    public Transform[] speakerPoint;
    public PhraseInDialog[] dialogs;

    private Animator animator;
    private PlayerMovement playerMovement;
    private LoseMenuScript loseMenuScript;

    private void Start()
    {
        blackLinesForm.SetActive(false);
        animator = GetComponent<Animator>();
        playerMovement = player.GetComponent<PlayerMovement>();
        loseMenuScript = player.GetComponent<PlayerHealth>().GetLoseCanvasScripts();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        StartDialogWindow();
    }

    private void StartDialogWindow()
    {
        cvCamera.Follow = transform;
        blackLinesForm.SetActive(true);
        playerMovement.StopHorizontalMovement();
        loseMenuScript.ManagePlayerBarsAndGamepad(false);
        animator.Play("Open Dialog Canvas");

    }

    public void CloseDialogWindow()
    {
        
        loseMenuScript.ManagePlayerBarsAndGamepad(true);
        animator.Play("Close Dialog Canvas");
    }
    public void LetPlayerMove()
    {
        playerMovement.StartHorizontalMovement();
        loseMenuScript.ManagePlayerBarsAndGamepad(true);
        cvCamera.Follow = player.transform;
    }
    public void SwitchOffActiveBlackLines(){
        blackLinesForm.SetActive(false);
    }
}

    
[System.Serializable]
public class PhraseInDialog
{
    [SerializeField] public string phrase;
    [SerializeField] public int speaker;
}
