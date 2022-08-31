using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public bool useJoystick;
    public float[] posAndScaleHorisontalLeftButton;
    public float[] posAndScaleHorisontalRightButton;
    public float[] posAndScaleJoystick;
    public float[] posAndScaleAttackButton;
    public float[] posAndScaleJumpButton;
    public float[] posAndScaleAbilityButton;
    public float[] posAndScaleOpenButton;
    public float alphaChannelParam;

    public SettingsData(bool useJoystick, float[] posAndScaleHorisontalLeftButton,
        float[] posAndScaleHorisontalRightButton, float[] posAndScaleJoystick,
        float[] posAndScaleAttackButton, float[] posAndScaleJumpButton,
        float[] posAndScaleAbilityButton, float[] posAndScaleOpenButton,
        float alphaChannelParam)
    {
        this.useJoystick = useJoystick;
        this.posAndScaleHorisontalLeftButton = posAndScaleHorisontalLeftButton;
        this.posAndScaleHorisontalRightButton = posAndScaleHorisontalRightButton;
        this.posAndScaleJoystick = posAndScaleJoystick;
        this.posAndScaleAttackButton = posAndScaleAttackButton;
        this.posAndScaleJumpButton = posAndScaleJumpButton;
        this.posAndScaleAbilityButton = posAndScaleAbilityButton;
        this.posAndScaleOpenButton = posAndScaleOpenButton;
        this.alphaChannelParam = alphaChannelParam;
    }
}
