using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapButton : MonoBehaviour
{
    [SerializeField] private int enemyFeetLayerNum;
    [SerializeField] private bool canEnemyTouchButton = true;
    [SerializeField] private float timeOnUnpressButton = 5f;
    private Animator animator;
    private Animator parentAnimator;
    private void Start()
    {
        animator = GetComponent<Animator>();
        parentAnimator = transform.parent.GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(enemyFeetLayerNum == other.gameObject.layer && canEnemyTouchButton
            || enemyFeetLayerNum != other.gameObject.layer)
        {
            animator.SetBool("On", true);
            parentAnimator.SetBool("Shot", true);
            StartCoroutine(UnpressButton());
        }
    }

    IEnumerator UnpressButton()
    {
        yield return new WaitForSeconds(timeOnUnpressButton);
        animator.SetBool("On", false);
        parentAnimator.SetBool("Shot", false);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (enemyFeetLayerNum == other.gameObject.layer && canEnemyTouchButton
            || enemyFeetLayerNum != other.gameObject.layer)
        {
            animator.SetBool("On", false);
            parentAnimator.SetBool("Shot", false);
        }
    }


}
