using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDetectChecker : MonoBehaviour
{
    [Range(0f, 15f)] [SerializeField] private float frontCheckSize = 5f;
    [Range(0f, 10f)] [SerializeField] private float backCheckSize = 1f;
    [SerializeField] private Transform checkPos;
    [SerializeField] private LayerMask detectingObjLayer;
    [SerializeField] private LayerMask detectingObjAndGroundLayers;

    [Serializable]
    private class Memory
    {
        public bool hasMemory;
        public float memoryTime;
        public float passedTime = 0;
    }
    [SerializeField] private Memory memory = new Memory();

    public bool detectedInFront;
    public bool detectedOnBack;
    public Transform firstDetectedCharacter;
    public Vector3 posToGo;
    private CharacterNavigatorController navigatorController;
    private int countVisiblePoints;

    private void Awake()
    {
        navigatorController = GetComponent<CharacterNavigatorController>();

        memory.passedTime = memory.memoryTime;
    }
    private void Update()
    {
        Detect(out detectedInFront, frontCheckSize, isInFront: true);
        Detect(out detectedOnBack, backCheckSize, isInFront: false);
        if (!detectedInFront && !detectedOnBack)
        {
            if (!memory.hasMemory || memory.passedTime >= memory.memoryTime)
            {
                firstDetectedCharacter = null;
            }
            else
            {
                memory.passedTime += Time.deltaTime;
                if((int)memory.passedTime >= countVisiblePoints)
                {
                    countVisiblePoints += 1;
                    posToGo = firstDetectedCharacter.position;
                }
            }
        }
        else
        {
            posToGo = firstDetectedCharacter.position;
            countVisiblePoints = 0;
            memory.passedTime = 0;
        }
    }

    private void Detect(out bool resultOfDetecting, float radius, bool isInFront)
    {
        Collider2D[] detectedColliders = Physics2D.OverlapCircleAll(checkPos.position, radius, detectingObjLayer);
        foreach(Collider2D detectedCollider in detectedColliders)
        {
            Vector2 diff = detectedCollider.transform.position - checkPos.position;

            #region Check Facing
            if (isInFront && (navigatorController.facingRight & diff.x < 0 || !navigatorController.facingRight & diff.x > 0))
            {
                resultOfDetecting = false;
                return;
            }
            if (!isInFront && (navigatorController.facingRight & diff.x > 0 || !navigatorController.facingRight & diff.x < 0))
            {
                resultOfDetecting = false;
                return;
            }
            #endregion

            RaycastHit2D raycastHit2D = Physics2D.Raycast(checkPos.position, diff.normalized, 
                diff.magnitude, layerMask: detectingObjAndGroundLayers);
            if (raycastHit2D)
            {
                if(1 << raycastHit2D.collider.gameObject.layer == detectingObjLayer){

                    firstDetectedCharacter = raycastHit2D.collider.gameObject.transform;
                    resultOfDetecting = true;
                    return;
                }
            }
        }
        resultOfDetecting = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white * 0.7f;
        Gizmos.DrawWireSphere(checkPos.position, frontCheckSize);
        Gizmos.DrawWireSphere(checkPos.position, backCheckSize);
    }
}
