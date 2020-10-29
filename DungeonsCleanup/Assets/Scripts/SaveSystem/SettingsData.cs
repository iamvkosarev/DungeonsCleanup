using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public bool useJoystick;
    public float scaleParam;
    public float posXParam;
    public float posYParam;
    public float alphaChannelParam;

    public SettingsData(bool useJoystick, float scaleParam, float posXParam, float posYParam, float alphaChannelParam)
    {
        this.useJoystick = useJoystick;
        this.scaleParam = scaleParam;
        this.posXParam = posXParam;
        this.posYParam = posYParam;
        this.alphaChannelParam = alphaChannelParam;
    }
}
