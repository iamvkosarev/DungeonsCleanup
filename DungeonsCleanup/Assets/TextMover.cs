using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextMover : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float delayBeforeLoadMainMenu;
    [SerializeField] private LevelLoader levelLoader;
    private RectTransform rectTransform;
    private Vector3 canvasPosition;
    void Start()
    {
        canvasPosition = GetComponentInParent<Transform>().position;
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, speed, 0);
        if(transform.position.y + canvasPosition.y > rectTransform.rect.height)
        {
            StartCoroutine(SetDelayBeforeLoadMainMenu());
        }
    }

    private IEnumerator SetDelayBeforeLoadMainMenu()
    {
        yield return new WaitForSeconds(delayBeforeLoadMainMenu);
        levelLoader.LoadMainMenu();
    }
}
