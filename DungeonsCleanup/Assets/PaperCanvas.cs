﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaperCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI headerText;
    [SerializeField] private TextMeshProUGUI mainText;
    Animator myAnimator;
    

    private void Start()
    {
        myAnimator= GetComponent<Animator>();
    }
    public void StopGame()
    {
        Time.timeScale = 0f;
    }
    public void PlayCloseAnimation()
    {
        
        Time.timeScale = 1f;
        myAnimator.SetBool("Show Paper", false);
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
    public void SetMainText(string text, int fontSize)
    {
        mainText.text = text;
        mainText.fontSize = fontSize;
    }
}
