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
    [SerializeField] public AbilityType abilityType = AbilityType.Null;
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
    #region Activations
    public void Activate(Vector2 playerPosition, Vector2 windPushRadius, LayerMask enemiesLayer, float pushXForce, float pushYForce)
    {
        Abilities ability = new Abilities();
        if(abilityType == AbilityType.Null)
        {
            return;
        }
        else if (abilityType == AbilityType.WindPush)
        {
            ability.WindPush(playerPosition, windPushRadius, enemiesLayer, pushXForce, pushYForce);
        }
    }
    public void Activate(Transform playerTransform, int shadowsBottleId)
    {
        Abilities ability = new Abilities();
        if (abilityType == AbilityType.Null)
        {
            return;
        }
        else if (abilityType == AbilityType.CallOfTheShadows)
        {
            ability.CallOfTheShadows(playerTransform, shadowsBottleId);
        }
    }
    #endregion
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
