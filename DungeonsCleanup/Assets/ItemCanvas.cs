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

    [Header("Artifact's Fields")]
    [SerializeField] private Image iconArtifactImage;
    [SerializeField] private TextMeshProUGUI nameArtifactField;
    [SerializeField] private TextMeshProUGUI descriptionArtifactField;
    [SerializeField] private TextMeshProUGUI rankArtifactField;
    [SerializeField] private TextMeshProUGUI abilityArtifactField;
    [Header("Time Crystal's Fields")]
    [SerializeField] private Image iconCrystalImage;
    [SerializeField] private TextMeshProUGUI activationDescriptionCrystalField;
    [SerializeField] private TextMeshProUGUI rankCrystalField;

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
        if (itemType == ItemType.Artifact)
        {
            artifactsField.SetActive(true);
        }
        else if(itemType == ItemType.TimeCrystal)
        {
            timeCrystalsField.SetActive(true);
        }
    }

    private void SwitchOffAllFields()
    {
        artifactsField.SetActive(false);
        timeCrystalsField.SetActive(false);
    }

    public void SetDataIntoFiled(ArtifactData artifactData)
    {
        SetType(ItemType.Artifact);
        iconArtifactImage.sprite = artifactData.icon;
        rankArtifactField.text += artifactData.rank;
        nameArtifactField.text = artifactData.nameOfArtifact;
        descriptionArtifactField.text += artifactData.description;
        abilityArtifactField.text += artifactData.abilityDescription;
    }
    public void SetDataIntoFiled(TimeCrystalData timeCrystalData)
    {
        SetType(ItemType.TimeCrystal);
        iconCrystalImage.color = timeCrystalData.color;
        rankCrystalField.text += timeCrystalData.rank;
        activationDescriptionCrystalField.text += timeCrystalData.activationDescription;
    }
    #endregion
}
