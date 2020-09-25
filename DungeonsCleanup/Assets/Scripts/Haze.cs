using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Haze : MonoBehaviour
{
    Animator myAnimator;
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void RotateHaze()
    {
         transform.localScale = new Vector2(-1, 1);
    }

    public void SetHazeOnStairs()
    {
        myAnimator.SetBool("StairsJump", true);
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void StartAnimation()
    {
        myAnimator.SetTrigger("Start");
    }
}
