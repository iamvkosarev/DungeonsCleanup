using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Artifact")]
public class ArtifactData : ItemData
{
    [Header("Main properties")]
    [SerializeField] public string rank;
    [SerializeField] public string nameOfArtifact;
    [TextArea(minLines: 6, maxLines: 6)] [SerializeField] public string description;
    [TextArea(minLines: 6, maxLines: 6)] [SerializeField] public string abilityDescription;
    [SerializeField] public Sprite icon;

    [Header("The activation properties")]
    [SerializeField] public bool canDestroyAfterActivate = false;

    [SerializeField] public bool canReactivate = true;
    [SerializeField] public float delayАfterActivation;
    private float timeOfActivation = 0f;
    private bool wasActivated;

    [SerializeField] public bool hasNumberOfActivations = false;
    [SerializeField] public int numOfActivations;
    private int howManyTimesWasActivated = 0;

    public bool CanDestroyAfterActivate()
    {
        return canDestroyAfterActivate;
    }

    public bool CanBeActivated()
    {
        if (!hasNumberOfActivations)
        {
            return true;
        }
        if (howManyTimesWasActivated <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public bool ActivateTheAttempt()
    {
        if (!hasNumberOfActivations)
        {
            return true;
        }

        if (howManyTimesWasActivated <= 0)
        {
            howManyTimesWasActivated = numOfActivations;
            return false;
        }
        else
        {
            howManyTimesWasActivated -= 1;
            return true;
        }
    }

    public bool ActivateAfterActivation()
    {
        if (!canReactivate)
        {
            if (!wasActivated)
            {
                wasActivated = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        if(timeOfActivation + delayАfterActivation <= Time.time)
        {
            wasActivated = false;
        }
        if (!wasActivated)
        {
            wasActivated = true;
            timeOfActivation = Time.time;
            return true;
        }
        else
        {
            return false;
        }
    }

}
