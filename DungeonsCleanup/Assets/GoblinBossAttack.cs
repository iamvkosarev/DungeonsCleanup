using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinBossAttack : MonoBehaviour
{
    [SerializeField] private Transform player;

    [Header("Push Attack")]
    [SerializeField] private float distanceForPush = 3f;
    [SerializeField] private float minPushXForce = 800f;
    [SerializeField] private float maxPushXForce = 1200f;
    [SerializeField] private float minPushYForce = 300f;
    [SerializeField] private float maxPushYForce = 500f;
    [SerializeField] private int damage = 25;
    private Animator myAnimator;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(player.position.x - transform.position.x) < distanceForPush)
        {
            myAnimator.SetTrigger("Push Attack");
        }
    }

    public void PushAttack()
    {
        if(Mathf.Abs(player.position.x - transform.position.x) < distanceForPush)
        {
            float pushXForce = Random.Range(minPushXForce, maxPushXForce);
            float pushYForce = Random.Range(minPushYForce, maxPushYForce);
            player.gameObject.GetComponent<Rigidbody2D>().
                AddForce(new Vector2(-pushXForce * Mathf.Sign(transform.localScale.x), pushYForce));
            player.gameObject.GetComponent<HealthUI>().TakeAwayHelath(damage);
        }
    }
}
