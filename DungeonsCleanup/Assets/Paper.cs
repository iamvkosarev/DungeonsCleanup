﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paper : MonoBehaviour
{
    [SerializeField] GameObject paperCanvas;
    [Header("Header Text")]
    [SerializeField] private int headerTextFontSize;
    [TextArea] [SerializeField] private string headerText;


    [Header("Main Text")]
    [SerializeField] private int mainTextFontSize;
    [TextArea] [SerializeField] private string mainText;

    private string headerTextName = "Header Text";
    private string mainTextName = "Main Text";

    private void SetText(GameObject canvas, string nameOfTextField, string text, int fontSize)
    {
        canvas.transform.Find(nameOfTextField).GetComponent<Text>().text = text;
        canvas.transform.Find(nameOfTextField).GetComponent<Text>().fontSize = fontSize;
    }

    public void InstansiateTabletCanvas()
    {
        GameObject canvas = Instantiate(paperCanvas);
        PaperCanvas paperCanvasScripts = canvas.GetComponent<PaperCanvas>();
        paperCanvasScripts.SetHeaderText(headerText, headerTextFontSize);
        paperCanvasScripts.SetProperties(mainText, mainTextFontSize);
        canvas.GetComponent<Animator>().SetBool("Show Paper", true);
    }


}
