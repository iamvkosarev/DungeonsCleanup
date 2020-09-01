using Microsoft.Unity.VisualStudio.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class JumpIconsScript : MonoBehaviour
{
    // config
    [SerializeField] Sprite[] jumpIcons;
    [SerializeField] JumpIconMode jumpIconMode;

    
    //catching files
    UnityEngine.UI.Image myImage;

    private void Start()
    {
        myImage = GetComponent<UnityEngine.UI.Image>();
    }
    public enum JumpIconMode
    {
        singleJumpActive,
        singleJumpNotActive,
        doubleJumpActive,
        doubleJumpHalfActive,
        doubleJumpNotActive,
    }
    
    public void SetJumpIconMode(JumpIconMode jumpIconMode)
    {
        this.jumpIconMode = jumpIconMode;
    }

    // Update is called once per frame
    void Update()
    {
        RefreshIcon();
    }

    private void RefreshIcon()
    {
        myImage.sprite = jumpIcons[(int)jumpIconMode];
    }
}
