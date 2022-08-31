using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharackerCuddleChecker : MonoBehaviour
{
    [SerializeField] private Transform cuddleCheckerPointPos;
    [Range(0.2f, 3f)] [SerializeField] private float cuddleCheckZoneHight = 1f;
    [Range(0.2f, 3f)] [SerializeField] private float cuddleCheckZoneWidth = 1f;
    [SerializeField] LayerMask cuddlingObjLayer;
    public bool detected = false;
    private CharacterNavigatorController navigatorController;
    private void Awake()
    {
        navigatorController = GetComponent<CharacterNavigatorController>();
    }

    private void Update()
    {
        detected = Physics2D.OverlapBox(cuddleCheckerPointPos.position,
            new Vector2(cuddleCheckZoneWidth, cuddleCheckZoneHight), 0, cuddlingObjLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f,192f/255f,203f/255f);

        if (navigatorController)
        {

            Gizmos.DrawWireCube(cuddleCheckerPointPos.position, new Vector2(cuddleCheckZoneWidth, cuddleCheckZoneHight)
                );
        }
        else
        {

            Gizmos.DrawWireCube(cuddleCheckerPointPos.position, new Vector2(cuddleCheckZoneWidth, cuddleCheckZoneHight)
                );
        }
    }
}
