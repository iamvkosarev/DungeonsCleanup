using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemCanvas : MonoBehaviour
{
    [SerializeField] private GameObject artifactsField;
    [SerializeField] private GameObject timeCrystalsField;
    private ItemType itemType;
    private int itemIndex;
    private Item item;

    [Header("Artifact's Fields")]
    [SerializeField] private Image iconArtifactImage;
    [SerializeField] private TextMeshProUGUI nameArtifactField;
    [SerializeField] private TextMeshProUGUI descriptionArtifactField;
    [SerializeField] private TextMeshProUGUI rankArtifactField;
    [SerializeField] private TextMeshProUGUI abilityArtifactField;
    [SerializeField] private GameObject addArtifactButton;
    [SerializeField] private GameObject replacementArtifactButton;
    [Header("Time Crystal's Fields")]
    [SerializeField] private Image iconCrystalImage;
    [SerializeField] private TextMeshProUGUI activationDescriptionCrystalField;
    [SerializeField] private TextMeshProUGUI rankCrystalField;
    [SerializeField] private GameObject addCrystalButton;
    [SerializeField] private GameObject replacementCrystalButton;

    // Interecting with Player
    private int freePlayerInventoryIndex;
    private int selectedPlayerInventoryIndex;
    private PlayerDevelopmentManager playerDevelopmentManager;

    private void Start()
    {
        //StopGame();
    }
    public void StopGame()
    {
        Time.timeScale = 0f;
    }
    public void DestroyCanvas()
    {
        //Time.timeScale = 1f;
        Destroy(gameObject);
    }

    #region Setters

    private void SetType(ItemType itemType)
    {
        SwitchOffAllFields();
        this.itemType = itemType;
        if (itemType == ItemType.Artifact)
        {
            artifactsField.SetActive(true);
        }
        else if(itemType == ItemType.TimeCrystal)
        {
            timeCrystalsField.SetActive(true);
        }
    }

    internal void SetItemGameObject(Item item)
    {
        this.item = item;
    }

    internal void SetPlayerDeveloperMenu(PlayerDevelopmentManager playerDevelopmentManager)
    {
        this.playerDevelopmentManager = playerDevelopmentManager;
        OpenButtonForAddIntoInventory();
    }

    private void OpenButtonForAddIntoInventory()
    {
        freePlayerInventoryIndex = playerDevelopmentManager.GetFreeItemField();
        if (freePlayerInventoryIndex != -1)
        {
            OpenAddIntoInventoryButton();
        }
        else
        {
            OpenReplacementInsideInventoryButton();
        }
    }

    private void OpenReplacementInsideInventoryButton()
    {
        addArtifactButton.SetActive(false);
        addCrystalButton.SetActive(false);
        replacementArtifactButton.SetActive(true);
        replacementCrystalButton.SetActive(true);
    }

    private void OpenAddIntoInventoryButton()
    {

        addArtifactButton.SetActive(true);
        addCrystalButton.SetActive(true);
        replacementArtifactButton.SetActive(false);
        replacementCrystalButton.SetActive(false);
    }

    public void AddIntoInventory()
    {
        playerDevelopmentManager.SetItem(freePlayerInventoryIndex, itemIndex, itemType);
        DestroyItem();
        DestroyCanvas();
    }

    private void DestroyItem()
    {
        Destroy(item.gameObject);
    }

    public void ReplacementInsideInventory()
    {
        ItemData newItemData = playerDevelopmentManager.SwitchItem(selectedPlayerInventoryIndex, itemIndex, itemType);
        item.SwitchItem(newItemData);
        DestroyCanvas();
    }

    private void SwitchOffAllFields()
    {
        artifactsField.SetActive(false);
        timeCrystalsField.SetActive(false);
    }

    public void SetDataIntoFiled(ArtifactData artifactData)
    {
        itemIndex = artifactData.id;

        SetType(ItemType.Artifact);
        iconArtifactImage.sprite = artifactData.icon;
        rankArtifactField.text += artifactData.rank;
        nameArtifactField.text = artifactData.nameOfArtifact;
        descriptionArtifactField.text += artifactData.description;
        abilityArtifactField.text += artifactData.abilityDescription;
    }
    public void SetDataIntoFiled(TimeCrystalData timeCrystalData)
    {
        itemIndex = timeCrystalData.id;

        SetType(ItemType.TimeCrystal);
        iconCrystalImage.color = timeCrystalData.color;
        rankCrystalField.text += timeCrystalData.rank;
        activationDescriptionCrystalField.text += timeCrystalData.activationDescription;
    }
    #endregion
}
