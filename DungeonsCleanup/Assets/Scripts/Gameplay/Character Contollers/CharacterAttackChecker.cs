using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterAttackChecker : MonoBehaviour
{
    #region Different types of checking
    private enum CheckerType
    {
        HalfCircle,
        Box
    }
    [Serializable]
    private class BoxChecker
    {
        [Range(0.2f, 3f)] [SerializeField] public float attackCheckZoneHight = 1f;
        [Range(0.2f, 3f)] [SerializeField] public float attackCheckZoneWidth = 1f;
    }
    [Serializable]
    private class HalfCircleChecker
    {
        [Range(0.2f, 10f)] [SerializeField] public float attackCheckRadius = 2f;
    }
    #endregion
    [SerializeField] private Transform attackCheckPos;
    [SerializeField] private CheckerType checkerType;
    [SerializeField] private BoxChecker boxChecker;
    [SerializeField] private HalfCircleChecker circleChecker;
    [SerializeField] LayerMask attackingObjLayer;
    [SerializeField] LayerMask attackingObjAndGroundLayers;
    public bool detected = false;
    public Vector3 firstDetectedCharacterPos;
    private CharacterNavigatorController navigatorController;
    private void Awake()
    {
        navigatorController = GetComponent<CharacterNavigatorController>();
    }

    private void Update()
    {
        if (checkerType == CheckerType.Box)
        {
            detected = Physics2D.OverlapBox((Vector2)attackCheckPos.position -
                new Vector2(boxChecker.attackCheckZoneWidth / 2f * (navigatorController.facingRight ? -1 : 1), boxChecker.attackCheckZoneHight / 2f),
                new Vector2(boxChecker.attackCheckZoneWidth, boxChecker.attackCheckZoneHight), 0, attackingObjLayer);
        }
        else
        {
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(attackCheckPos.position, circleChecker.attackCheckRadius, attackingObjLayer);
            if (collider2Ds.Length == 0)
            {
                detected = false;
            }
            else
            {
                foreach (Collider2D detectedCollider in collider2Ds)
                {
                    Vector2 diff = detectedCollider.transform.position - attackCheckPos.position;
                    #region Check Facing
                    if ((navigatorController.facingRight & diff.x < 0 || !navigatorController.facingRight & diff.x > 0))
                    {
                        detected = false;
                        continue;
                    }
                    #endregion
                    RaycastHit2D raycastHit2D = Physics2D.Raycast(attackCheckPos.position, diff.normalized,
                    diff.magnitude, layerMask: attackingObjAndGroundLayers);
                    if (raycastHit2D)
                    {
                        if (1 << raycastHit2D.collider.gameObject.layer == attackingObjLayer)
                        {
                            detected = true;
                            firstDetectedCharacterPos = raycastHit2D.collider.gameObject.transform.position;
                            break;
                        }
                        else
                        {

                            detected = false;
                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        if (checkerType == CheckerType.Box)
        {
            if (navigatorController)
            {

                Gizmos.DrawWireCube((Vector2)attackCheckPos.position - new Vector2(boxChecker.attackCheckZoneWidth / 2f * (navigatorController.facingRight ? -1 : 1)
                    , boxChecker.attackCheckZoneHight / 2f), new Vector2(boxChecker.attackCheckZoneWidth, boxChecker.attackCheckZoneHight));
            }
            else
            {

                Gizmos.DrawWireCube((Vector2)attackCheckPos.position - new Vector2(boxChecker.attackCheckZoneWidth / 2f * (1)
                    , boxChecker.attackCheckZoneHight / 2f), new Vector2(boxChecker.attackCheckZoneWidth, boxChecker.attackCheckZoneHight));
            }
        }
        else
        {
            Gizmos.DrawWireSphere(attackCheckPos.position, circleChecker.attackCheckRadius);
        }
    }

}
