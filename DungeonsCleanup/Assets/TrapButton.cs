using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapButton : MonoBehaviour
{
    private Animator animator;
    private Animator parentAnimator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        parentAnimator = transform.parent.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer != 0 && other.gameObject.layer != 9)
        {
            animator.SetBool("On", true);
            parentAnimator.SetBool("Shot", true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.layer != 0 && other.gameObject.layer != 9)
        {
            animator.SetBool("On", false);
            parentAnimator.SetBool("Shot", false);
        }
    }


}
