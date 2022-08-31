using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.OnScreen;

public class MovementGamepad : MonoBehaviour
{
    [SerializeField] private float minScale = 1f;
    [SerializeField] private float maxScale = 1.3f;
    [SerializeField] private Vector2 minPos = new Vector2(30, 30);
    [SerializeField] private Vector2 maxPos = new Vector2(200, 200);

    enum ModeOfGamepad
    {
        Joystick,
        Buttons
    }
    [SerializeField] private ModeOfGamepad modeOfGamepad;
    [SerializeField] private Image[] images;

    public void SetNewScale(float parametr)
    {
        if (parametr <= 1f && parametr >= 0f)
        {
            transform.localScale = new Vector3((maxScale - minScale) * parametr + minScale,
                (maxScale - minScale) * parametr + minScale, (maxScale - minScale) * parametr + minScale);
            if (modeOfGamepad == ModeOfGamepad.Joystick)
            {
                // Менять зону стика
            }
        }
    }

    public void SetNewPos(float xParametr, float yParametr)
    {
        if (xParametr <= 1f && yParametr >= 0f && yParametr <= 1f && yParametr >= 0f)
        {
            transform.position = new Vector2((maxPos.x - minPos.x) * xParametr + minPos.x, (maxPos.y - minPos.y) * yParametr + minPos.y);
        }
    }
    public void SetNewAlphaChannel(float parametr)
    {
        if (parametr <= 1f && parametr >= 0f)
        {
            foreach(Image image in images)
            {
                image.color = new Color(1, 1, 1, parametr);
            }
        }
    }
}
