using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBossMovement : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float startXScale;
    void Start()
    {
        startXScale = transform.localScale.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsFacingOnAHero())
        {
            Flip();
        }
    }

    private bool IsFacingOnAHero()
    {
        Vector2 startPos = transform.position;
        if(startPos.x >= player.transform.position.x)
        {
            return Mathf.Sign(transform.localScale.x) <= 0;
        }

        else
        {
            return Mathf.Sign(transform.localScale.x) > 0;
        }
    }

    private void Flip()
    {
        transform.localScale = new Vector2(-(Mathf.Sign(transform.localScale.x)) * Mathf.Abs(startXScale), transform.localScale.y);
    }
}
