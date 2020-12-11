using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AttackTag : MonoBehaviour
{
    [SerializeField] private Sprite[] attackTagSprites;
    [SerializeField] private string attakTagSortingLayerName = "VFX";
    [SerializeField] private Transform attackTagPos;
    [SerializeField] private float delayToDestory = 0.5f;
    GameObject attackTag;
    SpriteRenderer attackTagSpriteRenderer;
    Health health;
    private bool canSpawnTag = true;
    private int attackTagsLength;
    
    private void Start()
    {
        attackTagsLength = attackTagSprites.Length;
        health = GetComponent<Health>();
        if (health)
        {
            health.OnDeath += DestroyAttackTag;
        }
    }
    public void SetAttackTag(int num)
    {
        if(!canSpawnTag) { return; }
        if (attackTagsLength == 0)
        {
            return;
        }
        if(attackTagSprites.Length > num)
        {
            if (num == 0)
            {
                attackTag = new GameObject();
                attackTag.transform.parent = transform;
                attackTag.transform.position = attackTagPos.position;
                attackTagSpriteRenderer = attackTag.AddComponent<SpriteRenderer>();
                attackTagSpriteRenderer.sortingLayerName = attakTagSortingLayerName;
                attackTagSpriteRenderer.sprite = attackTagSprites[0];
            }
            else if(attackTag)
            {
                attackTagSpriteRenderer.sprite = attackTagSprites[num];
                if (num == attackTagSprites.Length - 1)
                {
                    DestroyWithoutWait();
                }
            }
        }
    }

    IEnumerator DestroyWithoutWait()
    {
        yield return new WaitForSeconds(delayToDestory);
        DestroyAttackTag();
    }
    public void DestroyAttackTag()
    {
        if (attackTag)
        {
            Destroy(attackTag);
        }
    }
    public void DestroyAttackTag(object obj, EventArgs e)
    {
        DestroyAttackTag();
        canSpawnTag = false;
    }
}
