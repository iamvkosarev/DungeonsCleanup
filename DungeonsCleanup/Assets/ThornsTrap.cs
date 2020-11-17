using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornsTrap : MonoBehaviour
{
    [Header("Frequency")]
    [SerializeField] private float attackingTime = 2f;
    [SerializeField] private float doesntAttackingTime = 5f;
    [SerializeField]private int damage;
    private Animator myAnimator;
    private PlayerMovement player;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        
        myAnimator = GetComponent<Animator>();
        StartCoroutine(Attacking());
    }
    
    private IEnumerator Attacking()
    {
        while(true)
        {
            
            SwitchColliderValue(false);
            myAnimator.Play("ThornsDoesntAttack");
            yield return new WaitForSeconds(doesntAttackingTime);

            SwitchColliderValue(true);
            myAnimator.Play("ThornsAttack");
            yield return new WaitForSeconds(attackingTime);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject == player.gameObject)
        {
            player.GetComponent<PlayerHealth>().TakeAwayHelath(damage);
        }
    }

    private void SwitchColliderValue(bool value)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = value;
    }
}
