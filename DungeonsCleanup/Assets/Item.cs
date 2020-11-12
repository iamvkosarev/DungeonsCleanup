using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private TimeCrystalData timeCrystalData;
    [SerializeField] private ArtifactData artifactData;
    [SerializeField] private GameObject itemInfoCanvas;
    private SpriteRenderer spriteRenderer;
    private Animator myAnimator;

    private void Start()
    {
        LoadShining();
        LoadSprite();
    }

    private void LoadShining()
    {
        if (!myAnimator)
        {
            myAnimator = GetComponentInChildren<Animator>();
        }
        if (timeCrystalData)
        {
            myAnimator.SetBool("isCrystal", true);
        }
        else
        {
            myAnimator.enabled = false;
        }
    }

    public void LoadSprite()
    {
        if (!spriteRenderer)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        if (timeCrystalData)
        {
            spriteRenderer.color = timeCrystalData.color;
        }
        else if (artifactData)
        {
            spriteRenderer.sprite = artifactData.icon;
        }
    }

    public void InstansiateItemInfoCanvas()
    {
        GameObject canvas = Instantiate(itemInfoCanvas);
        ItemCanvas itemInfoCanvasScripts = canvas.GetComponent<ItemCanvas>();
        if (timeCrystalData)
        {
            itemInfoCanvasScripts.SetDataIntoFiled(timeCrystalData);
        }
        else if (artifactData)
        {
            itemInfoCanvasScripts.SetDataIntoFiled(artifactData);
        }

    }

    // Switch Item 

    // Pick up iteme
}
