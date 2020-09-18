using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haze : MonoBehaviour
{
    [SerializeField] LayerMask stairsLayer;
    [SerializeField] float checkRadius;
    Animator myAnimator;
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void CheckStairsPos()
    {
        if (Physics2D.OverlapCircle(new Vector2(transform.position.x + transform.localScale.x * 0.8f, transform.position.y), checkRadius, stairsLayer))
        {
            transform.localScale = new Vector2(-1, 1);
        }
    }

    public void SetHazeOnStairs()
    {
        myAnimator.SetBool("StairsJump", true);
        CheckStairsPos();
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
