using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAlphaChannel : MonoBehaviour
{
    [SerializeField] private float timeOnChange = -1;
    [SerializeField] private float startAlphaChannel = 1;
    [SerializeField] private float endAlphaChannel = 0;
    private SpriteRenderer mySR;
    private float timeSicneStart;
    public void  SetTimeOnChange(float newTime)
    {
        timeOnChange = newTime;
    }
    private void Start()
    {
        mySR = GetComponent<SpriteRenderer>();
        mySR.color = new Color(1,1,1,startAlphaChannel);
        timeSicneStart = Time.time;
    }
    private void Update()
    {
        if (timeOnChange == -1) { return; }
        if (timeOnChange  + timeSicneStart < Time.deltaTime)
        {
            mySR.color = new Color(1, 1, 1, endAlphaChannel);
        }
        else
        {
            float changeParam = (Time.time - timeSicneStart) / timeOnChange;
            mySR.color = new Color(1, 1, 1, startAlphaChannel - (startAlphaChannel - endAlphaChannel) * changeParam);
        }
    }

}
