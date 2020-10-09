using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator myAnimator;

    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    public void Open()
    {
        myAnimator.SetTrigger("Opening");
        GetComponent<BoxCollider2D>().enabled = false;
    }
}
