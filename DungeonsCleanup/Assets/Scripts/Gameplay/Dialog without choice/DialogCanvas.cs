using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class DialogCanvas : MonoBehaviour
{
    [Header("Manage Properties")]
    [SerializeField] private bool playByCollider = true;
    [SerializeField] private bool startCreatingPhrasesAuto = true;
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
    public event EventHandler OnReadyForCreatPhrases;
    public event EventHandler OnCloseDialogCanvas;
    private Animator animator;
    private PlayerMovement playerMovement;
    private LoseMenuScript loseMenuScript;
    private int currentPhraseNum = 0;
    private TextInDialog currentTextInDialog;
    private void Start()
    {
        blackLinesForm.SetActive(false);
        closeWindow.SetActive(false);
        completionWindow.SetActive(false);
        if (playByCollider)
        {
            animator = GetComponent<Animator>();
            playerMovement = player.GetComponent<PlayerMovement>();
            loseMenuScript = player.GetComponent<PlayerHealth>().GetLoseCanvasScripts();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (playByCollider)
        {
            OpenDialogWindow();
        }
    }

    public void OpenDialogWindow()
    {
        if (!playByCollider)
        {
            animator = GetComponent<Animator>();
            playerMovement = player.GetComponent<PlayerMovement>();
            loseMenuScript = player.GetComponent<PlayerHealth>().GetLoseCanvasScripts();
        }
        cvCamera.Follow = transform;
        blackLinesForm.SetActive(true);
        playerMovement.StopHorizontalMovement();
        loseMenuScript.ManagePlayerBarsAndGamepad(false);
        animator.Play("Open Dialog Canvas");

    }
    public void StartCreatingPhrases(int fromDialogOnStart = -1)
    {
        if (startCreatingPhrasesAuto || fromDialogOnStart != -1)
        {
            CreatPhrase();
        }
        if (OnReadyForCreatPhrases != null && fromDialogOnStart == -1)
        {
            OnReadyForCreatPhrases.Invoke(this, EventArgs.Empty);
        }
    }
    public void CreatPhrase()
    {
        if (currentPhraseNum == dialogs.Length) { CloseDialogWindow(); return; }
        GameObject newTextObject = Instantiate(textFormPrefab);
        RectTransform rectTransform = newTextObject.GetComponent<RectTransform>();
        rectTransform.SetParent(speakerPoint[dialogs[currentPhraseNum].speaker]);
        rectTransform.localPosition = new Vector2(0f, 0f);
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
        if (OnCloseDialogCanvas != null)
        {
            OnCloseDialogCanvas.Invoke(this, EventArgs.Empty);
        }
        playerMovement.StartHorizontalMovement();
        loseMenuScript.ManagePlayerBarsAndGamepad(true);
        cvCamera.Follow = player.transform;
        Destroy(gameObject);
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
