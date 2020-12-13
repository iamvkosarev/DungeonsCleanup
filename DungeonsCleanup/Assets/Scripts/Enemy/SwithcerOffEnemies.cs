using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwithcerOffEnemies : MonoBehaviour
{
    [SerializeField] private Vector2 checkEnemiesSize;
    [SerializeField] private Color checkEnemiesColor;
    [SerializeField] private LayerMask enemiesLayer;
    EnemiesMovement[] enemies;
    Collider2D[] lastFindedColliders;
    void Start()
    {
        EnemiesMovement[] enemiesMovements = FindObjectsOfType<EnemiesMovement>();
        foreach(EnemiesMovement enemiesMovement in enemiesMovements)
        {
            enemiesMovement.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        CheckEnemies();
    }

    private void CheckEnemies()
    {
        Collider2D[] newColliders = Physics2D.OverlapBoxAll(transform.position, checkEnemiesSize, enemiesLayer);
        if (newColliders != lastFindedColliders)
        {
            foreach(Collider2D enemieCollider in newColliders)
            {
                enemieCollider.gameObject.SetActive(false);
            }
            lastFindedColliders = newColliders;
            foreach (Collider2D enemieCollider in newColliders)
            {
                enemieCollider.gameObject.SetActive(true);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = checkEnemiesColor;
        Gizmos.DrawCube(transform.position, checkEnemiesSize);
    }
}
