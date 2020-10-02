using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] string nextSceneName;
    [SerializeField] float dalayBeforeStart = 1f;
    [SerializeField] GameObject canvas;
    Animator myAnimator;
    private void Start()
    {
        canvas.active = true;
        myAnimator = GetComponent<Animator>();
        StartCoroutine(LoadingStartCrossfade());
    }
    IEnumerator LoadingStartCrossfade()
    {
        yield return new WaitForSeconds(dalayBeforeStart);
        myAnimator.SetTrigger("StartStartCrossfade");
    }
    public void SwitchOffCanvas()
    {
        canvas.active = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        canvas.active = true;
        myAnimator.SetTrigger("StartExitCrossfade");
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
