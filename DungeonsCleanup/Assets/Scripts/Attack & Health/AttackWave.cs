using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackWave : MonoBehaviour
{
    [SerializeField] float lifeTime;
    [SerializeField] bool isMoving;
    [SerializeField] float movingSpeed;

    Rigidbody2D myRigitBody2D;
    IEnumerator Start()
    {
        if (isMoving)
        {
            myRigitBody2D = GetComponent<Rigidbody2D>();
            myRigitBody2D.velocity = new Vector2(movingSpeed * Mathf.Sign(transform.localScale.x), 0);
        }
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

}
