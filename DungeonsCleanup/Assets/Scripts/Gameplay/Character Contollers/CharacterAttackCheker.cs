using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackCheker : MonoBehaviour
{
    [SerializeField] private Transform attackSpawnPos;
    [Range(0.2f, 3f)][SerializeField] private float attackCheckZoneHight = 1f;
    [Range(0.2f, 3f)] [SerializeField] private float attackCheckZoneWidth = 1f;
    [SerializeField] LayerMask attackingObjLayer;
    public bool detected = false;
    private CharacterNavigatorController navigatorController;
    private void Awake()
    {
        navigatorController = GetComponent<CharacterNavigatorController>();
    }

    private void Update()
    {
        detected = Physics2D.OverlapBox((Vector2)attackSpawnPos.position -
            new Vector2(attackCheckZoneWidth / 2f * (navigatorController.facingRight ? -1 : 1), attackCheckZoneHight / 2f),
            new Vector2(attackCheckZoneWidth, attackCheckZoneHight), 0, attackingObjLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireCube((Vector2)attackSpawnPos.position - new Vector2(attackCheckZoneWidth/2f * (navigatorController.facingRight ? -1:1)
            , attackCheckZoneHight/2f), new Vector2(attackCheckZoneWidth, attackCheckZoneHight)
            );
    }

}
