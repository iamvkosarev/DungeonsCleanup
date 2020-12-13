using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextInDialog : MonoBehaviour
{
    [SerializeField] private float timeOnCompletion = 2f;
    [SerializeField] private TextMeshProUGUI formText;
    [SerializeField] private TextMeshProUGUI visibleText;
    public event EventHandler OnTextCompletion;
    private int currentNumOfLatters = 0;
    private int needNumOfLatters;
    private string text;
    private float timeBetweenAddingLatters;
    private bool wasCompletion = false;
    public void SetPropertes(string text, bool setTime = false, float time = 1f)
    {
        if (setTime)
        {
            timeOnCompletion = time;
        }
        this.text = text;
        this.needNumOfLatters = text.Length;
        timeBetweenAddingLatters = timeOnCompletion / (float)needNumOfLatters;
        StartCoroutine(AddingLaters());
    }
    private void UpadateTextInForms()
    {
        formText.text = this.text.Substring(0, currentNumOfLatters);
        visibleText.text = this.text.Substring(0, currentNumOfLatters);
    }
    public void AutoCompletionText()
    {
        wasCompletion = true;
        currentNumOfLatters = needNumOfLatters;
        UpadateTextInForms();
        OnTextCompletion.Invoke(text, EventArgs.Empty);
    }
    IEnumerator AddingLaters()
    {
        while (!wasCompletion)
        {

            UpadateTextInForms();
            currentNumOfLatters += 1;
            if (currentNumOfLatters == needNumOfLatters +1)
            {
                wasCompletion = true;
                OnTextCompletion.Invoke(text, EventArgs.Empty);
                break;
            }
            yield return new WaitForSeconds(timeBetweenAddingLatters);
        }
    }
}
