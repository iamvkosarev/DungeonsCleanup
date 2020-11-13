using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TimeCrystal")]
public class TimeCrystalData : ItemData
{
    [SerializeField] public string rank;
    [TextArea(minLines: 6, maxLines: 6)] [SerializeField] public string activationDescription;
    [SerializeField] public Color color;
    [SerializeField] public ArtifactData artifactData;
    //[SerializeField] public ActivationСondition activationСondition;
}
