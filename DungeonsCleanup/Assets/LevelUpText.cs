using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpText : MonoBehaviour
{
    [SerializeField] private float lifeCycle = 0.3f;
    private Animator myAnimator;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myAnimator.SetFloat("LifeCycle", lifeCycle);
    }

    public void ActivateText()
    {
        gameObject.SetActive(true);
    }

    public void DestroyText()
    {
        gameObject.SetActive(false);
    }
}
