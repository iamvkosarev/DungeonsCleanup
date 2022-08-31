using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpText : MonoBehaviour
{
    private Animator myAnimator;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void PlayUpAnimation()
    {
        myAnimator.Play("Level Up Text up");
    }
}
