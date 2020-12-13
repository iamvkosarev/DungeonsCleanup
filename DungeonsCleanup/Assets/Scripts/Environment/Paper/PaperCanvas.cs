using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaperCanvas : MonoBehaviour
{
    [SerializeField] private GameObject closeButton;
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI mainText;
    Animator myAnimator;



    private void Start()
    {
        myAnimator= GetComponent<Animator>();
        closeButton.SetActive(false);
    }
    public void StopGame()
    {
        Time.timeScale = 0f;
        ShowCloseButton();
    }
    public void PlayCloseAnimation()
    {
        
        Time.timeScale = 1f;
        myAnimator.SetBool("Show Paper", false);
        closeButton.SetActive(false);
    }
    public void DestroyCanvas()
    {
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
    public void SetHeaderText(string text, int fontSize)
    {
        headerText.text = text;
        headerText.fontSize = fontSize;
    }
    public void ShowCloseButton()
    {
        closeButton.SetActive(true);
    }
    public void SetProperties(string text, int fontSize)
    {
        mainText.text = text;
        mainText.fontSize = fontSize;
    }
}
