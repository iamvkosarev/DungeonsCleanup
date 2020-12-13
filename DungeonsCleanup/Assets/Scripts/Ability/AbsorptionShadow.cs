using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorptionShadow : MonoBehaviour
{
    [SerializeField] public int shadowId;
    private BoxCollider2D collider2D;
    private bool canBeAbsorpted = false;

    private void Start()
    {
        collider2D = GetComponent<BoxCollider2D>();
        CanBeAbsorpted(false);
    }
    public void CanBeAbsorpted(bool mode)
    {
        canBeAbsorpted = mode;
        collider2D.enabled = mode;
    }
}
