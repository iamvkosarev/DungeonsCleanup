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
    [SerializeField] public SpriteRenderer spriteRenderer;
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
            if(myAnimator)
                myAnimator.SetBool("isCrystal", false);
        }
    }
    private void LateUpdate()
    {
        LoadSprite();
        return;
    }

    public void LoadSprite()
    {
        if (timeCrystalData)
        {
            spriteRenderer.color = timeCrystalData.color;
        }
        else if (artifactData)
        {
            spriteRenderer.sprite = artifactData.GetIcon();
        }

        if(myAnimator)
            myAnimator.StopRecording();
        else
            return;
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
    public void InstansiateItemInfoCanvas(PlayerDevelopmentManager playerDevelopmentManager, LoseMenuScript loseMenuScript)
    {
        GameObject canvas = Instantiate(itemInfoCanvas);
        ItemCanvas itemInfoCanvasScripts = canvas.GetComponent<ItemCanvas>();
        itemInfoCanvasScripts.SetPlayerDeveloperMenu(playerDevelopmentManager);
        itemInfoCanvasScripts.SetLoseMenuScript(loseMenuScript);
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
