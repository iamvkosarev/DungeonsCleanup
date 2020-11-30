using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TypeOfElement
{
    exp,
    hp
}
public class ExperienceAndHP : MonoBehaviour
{
    
    [SerializeField] private TypeOfElement typeOfElement = TypeOfElement.exp;
    [SerializeField] private int amount = 1;
    [Header("Start Push Up")]
    [SerializeField] private Vector2 pushBoundaries;
    [Header("Interacting with player")]
    [SerializeField] private Color colorOfDetectPlayer;
    [SerializeField] private Color colorOfAddingExpToPlayer;
    [SerializeField] private float radiusOfDetectPlayer;
    [SerializeField] private float radiusOfAddExpToPlayer;
    [SerializeField] private float approximationСoefficient;
    [SerializeField] private float delayBeforeStartContact;
    [SerializeField] private LayerMask playerLayer;
    private bool canContant;

    private Rigidbody2D myRb;
    IEnumerator Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        myRb.velocity = GetStartForce();
        yield return new WaitForSeconds(delayBeforeStartContact);
        canContant = true;
    }
    public void SetAmount(int amount)
    {
        this.amount = amount;
    }
    private Vector2 GetStartForce()
    {
        Vector2 force = new Vector2(UnityEngine.Random.Range(pushBoundaries.x, pushBoundaries.y) / 2 * Mathf.Sign(UnityEngine.Random.Range(-1, 1)),
            UnityEngine.Random.Range(pushBoundaries.x, pushBoundaries.y));
        return force;
    }

    private void FixedUpdate()
    {
        if(!canContant) { return; }
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        Collider2D playerCollider_toMoveCheck = Physics2D.OverlapCircle(transform.position, radiusOfDetectPlayer, playerLayer);
        Collider2D playerCollider_toAddExpCheck = Physics2D.OverlapCircle(transform.position, radiusOfAddExpToPlayer, playerLayer);
        if (playerCollider_toMoveCheck )
        {
            if (playerCollider_toAddExpCheck && playerCollider_toAddExpCheck.gameObject.tag != "Shadow")
            {
                PlayerDevelopmentManager playerDevelopmentManager = playerCollider_toAddExpCheck.gameObject.GetComponent<PlayerDevelopmentManager>();
                if (playerDevelopmentManager)
                {
                    if (typeOfElement == TypeOfElement.exp)
                    {
                        playerDevelopmentManager.AddExp(amount);
                    }
                    else if (typeOfElement == TypeOfElement.hp)
                    {
                        playerDevelopmentManager.AddHealth(amount);
                    }
                }
                Destroy(gameObject);
                return;
            }
            float playerX = playerCollider_toMoveCheck.gameObject.transform.position.x;
            float playerY = playerCollider_toMoveCheck.gameObject.transform.position.y;
            float elementX = transform.position.x;
            float elementY = transform.position.y;
            float distanceBetweenPointAndPlayer = Mathf.Sqrt(
                Mathf.Pow(playerX - elementX, 2) +
                Mathf.Pow(playerY - elementY, 2));
            Vector2 force;
            if (playerCollider_toMoveCheck.gameObject.tag == "Shadow")
            {
                force = new Vector2(0,0);
            }
            else
            {
                force = approximationСoefficient / Mathf.Pow(distanceBetweenPointAndPlayer, 3) * new Vector2(playerX - elementX, playerY - elementY);
            }
            myRb.velocity = force;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = colorOfDetectPlayer;
        Gizmos.DrawSphere(transform.position, radiusOfDetectPlayer);
        Gizmos.color = colorOfAddingExpToPlayer;
        Gizmos.DrawSphere(transform.position, radiusOfAddExpToPlayer);
    }

}
