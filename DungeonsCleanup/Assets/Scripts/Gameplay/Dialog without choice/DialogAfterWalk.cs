using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogAfterWalk : MonoBehaviour
{
    [SerializeField] private bool playOnStart = true;
    [SerializeField] private PlayerWalkToTarget playerDialogMovement;
    private DialogCanvas dialogCanvas;
    private bool isCanvasOpened = false;
    private bool isPlayerOnPlace = false;
    IEnumerator Start()
    {
        if (playOnStart)
        {
            playerDialogMovement.StartWalk();
            yield return new WaitForSeconds(0.1f);
            dialogCanvas = GetComponent<DialogCanvas>();
            dialogCanvas.OpenDialogWindow();
            dialogCanvas.OnReadyForCreatPhrases += IsReadyForPhrases;
            playerDialogMovement.OnReadyForTalk += IsPlayerOnPlace;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!playOnStart)
        {
            playerDialogMovement.StartWalk();
            dialogCanvas = GetComponent<DialogCanvas>();
            dialogCanvas.OpenDialogWindow();
            dialogCanvas.OnReadyForCreatPhrases += IsReadyForPhrases;
            playerDialogMovement.OnReadyForTalk += IsPlayerOnPlace;
        }
    }
    private void IsReadyForPhrases(object obj, EventArgs e)
    {
        isCanvasOpened = true;
        StartMakingPhrases();
    }
    private void IsPlayerOnPlace(object obj, EventArgs e)
    {
        isPlayerOnPlace = true;
        StartMakingPhrases();
    }
    private void StartMakingPhrases()
    {
        if(isCanvasOpened && isPlayerOnPlace)
        {
            dialogCanvas.StartCreatingPhrases(1);
        }
    }
}
