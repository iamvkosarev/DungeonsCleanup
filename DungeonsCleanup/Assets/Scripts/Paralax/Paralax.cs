using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Paralax : MonoBehaviour
{
    [SerializeField] private float paralaxEffector;
    [SerializeField] private GameObject cam;
    private float length, startPos;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
    void FixedUpdate()
    {
        float temp = cam.transform.position.x * (1 - paralaxEffector);
        float dist = cam.transform.position.x * paralaxEffector;
        transform.position = new Vector2(dist + startPos, transform.position.y);
        if (temp > startPos + length) startPos += length;
        else if (temp < startPos - length) startPos -= length;
    }
}
