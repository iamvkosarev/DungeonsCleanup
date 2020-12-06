using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class DialogCanvas : MonoBehaviour
{
    [Header("Manage Properties")]
    [SerializeField] private GameObject closeWindow;
    [SerializeField] private GameObject completionWindow;
    [SerializeField] private GameObject textFormPrefab;
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
    private int currentPhraseNum = 0;
    private TextInDialog currentTextInDialog;
    private void Start()
    {
        closeWindow.SetActive(false);
        completionWindow.SetActive(false);
        blackLinesForm.SetActive(false);
        animator = GetComponent<Animator>();
        playerMovement = player.GetComponent<PlayerMovement>();
        loseMenuScript = player.GetComponent<PlayerHealth>().GetLoseCanvasScripts();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        OpenDialogWindow();
    }

    private void OpenDialogWindow()
    {
        cvCamera.Follow = transform;
        blackLinesForm.SetActive(true);
        playerMovement.StopHorizontalMovement();
        loseMenuScript.ManagePlayerBarsAndGamepad(false);
        animator.Play("Open Dialog Canvas");

    }
    public void CreatPhrase()
    {
        if (currentPhraseNum == dialogs.Length) { CloseDialogWindow(); }
        GameObject newTextObject = Instantiate(textFormPrefab, speakerPoint[dialogs[currentPhraseNum].speaker]);
        newTextObject.transform.parent = transform;
        currentTextInDialog = newTextObject.GetComponent<TextInDialog>();
        currentTextInDialog.SetPropertes(dialogs[currentPhraseNum].phrase, true, dialogs[currentPhraseNum].time);
        currentTextInDialog.OnTextCompletion += CanClosePhrase;
        completionWindow.SetActive(true);

    }
    private void CanClosePhrase(object obj, EventArgs e)
    {
        completionWindow.SetActive(false);
        closeWindow.SetActive(true);
    }
    public void CloseText()
    {
        if (currentTextInDialog)
        {
            Destroy(currentTextInDialog.gameObject);
            currentPhraseNum += 1;
            CreatPhrase();
        }
    }
    public void CompletionPhrase()
    {
        if (currentTextInDialog)
        {
            currentTextInDialog.AutoCompletionText();
        }
    }
    public void CloseDialogWindow()
    {
        if (currentTextInDialog)
        {
            Destroy(currentTextInDialog.gameObject);
        }
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
    [SerializeField] public float time;
}
