using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletCanvas : MonoBehaviour
{
    Animator myAnimator;

    private void Start()
    {
        myAnimator= GetComponent<Animator>();
    }
    public void StopGame()
    {
        Time.timeScale = 0f;
    }
    public void PlayCloseAnimation()
    {
        
        Time.timeScale = 1f;
        myAnimator.SetBool("Show Tablet", false);
    }
    public void DestroyCanvas()
    {
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}
