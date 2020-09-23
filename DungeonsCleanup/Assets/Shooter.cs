using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] float distanceToShoot = 10f;
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject gun;
    PlayerMovement player;
    Animator myAnimator;

    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        myAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        LookingForAPlayer();
    }

    private void LookingForAPlayer()
    {
        if(Mathf.Abs(transform.position.x - player.transform.position.x) < distanceToShoot && 
            Mathf.Abs(transform.position.y - player.transform.position.y) < 1f)
        {
            myAnimator.SetBool("Shooting", true);
        }
    }

    private void Shooting()
    {
        Instantiate(projectile, gun.transform.position, Quaternion.identity);
        myAnimator.SetBool("Shooting", false);
    }
}
