using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterAttackManager : MonoBehaviour
{
    private CharacterAttackChecker attackCheker;
    private CharacterDetectChecker detectChecker;
    private CharacterNavigatorController navigatorController;
    private CharackerCuddleChecker cuddleChecker;
    private WaypointNavigator waypointNavigator;

    [Serializable]
    private class WaitCharacterAfterLost
    {
        public bool hasWaiting;
        public float waitTime;
        public float passedTime = 0;
    }
    [SerializeField] private WaitCharacterAfterLost wait = new WaitCharacterAfterLost();

    private void Awake()
    {
        cuddleChecker = GetComponent<CharackerCuddleChecker>();
        attackCheker = GetComponent<CharacterAttackChecker>();
        detectChecker = GetComponent<CharacterDetectChecker>();
        navigatorController = GetComponent<CharacterNavigatorController>();
        waypointNavigator = GetComponent<WaypointNavigator>();

        wait.passedTime = wait.waitTime;
    }

    private void Update()
    {
        if (detectChecker.firstDetectedCharacter)
        {
            waypointNavigator.StopChasingWaypoints();
            wait.passedTime = 0;
            if (cuddleChecker.detected)
            {
                float signOfWalk = Mathf.Sign(detectChecker.posToGo.x - transform.position.x);
                Vector3 pointToGo = transform.position - new Vector3(transform.position.x + 0.5f * signOfWalk, transform.position.y, transform.position.z);
                navigatorController.SetDestination(pointToGo, MovementType.Run);
            }
            else if (attackCheker.detected)
            {
                navigatorController.SetDestination(detectChecker.posToGo, MovementType.Stand);
            }
            else
            {
                navigatorController.SetDestination(detectChecker.posToGo, MovementType.Run);
            }
        }
        else
        {
            if (!wait.hasWaiting || wait.passedTime >= wait.waitTime)
            {
                waypointNavigator.StartChasingWaypoints();
            }
            else
            {
                navigatorController.SetDestination(transform.position, MovementType.Stand);
                wait.passedTime += Time.deltaTime;
            }
        }
    }
}
