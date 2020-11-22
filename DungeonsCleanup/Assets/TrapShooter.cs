using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform[] spawnProjectilesPoints;
    [SerializeField] private AudioClip shootSFX;
    [SerializeField] private float boostAudio;
    [Range(0f,360f)] [SerializeField] private float angle = 0f;
    private AudioSource[] myAudioSources;
    private void Start()
    {
        myAudioSources = GetComponentsInChildren<AudioSource>();
    }
    private void Shoot()
    {
        for (int index = 0; index < spawnProjectilesPoints.Length; index++)
        {

            GameObject projectile = Instantiate(projectilePrefab, spawnProjectilesPoints[index].transform.position, Quaternion.identity) as GameObject;
            TrapArrowMovement trapArrowMovement = projectile.GetComponent<TrapArrowMovement>();
            trapArrowMovement.SetDirectionByAngle(angle);
            myAudioSources[index].PlayOneShot(shootSFX, boostAudio);
        }

    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(spawnProjectilesPoints[0].position, new Vector2(Mathf.Cos(angle * Mathf.PI / 180f), Mathf.Sin(angle * Mathf.PI / 180f)));
    }
}
