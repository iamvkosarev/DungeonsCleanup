using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ListsOfItmes listsOfItmes;
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
            myAnimator.enabled = true;
            myAnimator.SetBool("isCrystal", true);
        }
        else
        {
            myAnimator.SetBool("isCrystal", false);
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
    public void SwitchItem(ItemData itemData)
    {
        timeCrystalData = null;
        artifactData = null;
        ItemType newItemType = itemData.itemType;
        if (newItemType == ItemType.Artifact)
        {
            artifactData = listsOfItmes.GetArtifactData(itemData.id);
        }
        else if(newItemType == ItemType.TimeCrystal)
        {
            timeCrystalData = listsOfItmes.GetTimeCrystalData(itemData.id);
        }
        LoadShining();
        LoadSprite();
    }
    public void InstansiateItemInfoCanvas(PlayerDevelopmentManager playerDevelopmentManager)
    {
        GameObject canvas = Instantiate(itemInfoCanvas);
        ItemCanvas itemInfoCanvasScripts = canvas.GetComponent<ItemCanvas>();
        itemInfoCanvasScripts.SetPlayerDeveloperMenu(playerDevelopmentManager);
        itemInfoCanvasScripts.SetItemGameObject(this);
        if (timeCrystalData)
        {
            itemInfoCanvasScripts.SetDataIntoFileds(timeCrystalData);
        }
        else if (artifactData)
        {
            itemInfoCanvasScripts.SetDataIntoFileds(artifactData);
        }

    }

    // Switch Item 

    // Pick up iteme
}
