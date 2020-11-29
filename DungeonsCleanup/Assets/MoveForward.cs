using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float speed;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);   
    }
}
