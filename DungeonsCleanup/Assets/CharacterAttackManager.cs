using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterAttackManager : MonoBehaviour
{
    private CharacterAttackCheker attackCheker;
    private CharacterDetectChecker detectChecker;
    private CharacterNavigatorController navigatorController;
    private WaypointNavigator waypointNavigator;

    [Serializable]
    private class WaitCharacterAfterLost
    {
        public bool hasWaiting;
        public float waitTime;
        public float passedTime = 0;
    }
    [SerializeField] private WaitCharacterAfterLost wait = new WaitCharacterAfterLost();

    private SpawnerOfAttackingWave spawnerOfAttackingWave;
    private SpawnerOfProjectile spawnerOfProjectile;

    private void Awake()
    {
        attackCheker = GetComponent<CharacterAttackCheker>();
        detectChecker = GetComponent<CharacterDetectChecker>();
        navigatorController = GetComponent<CharacterNavigatorController>();
        waypointNavigator = GetComponent<WaypointNavigator>();


        spawnerOfAttackingWave = GetComponent<SpawnerOfAttackingWave>();
        spawnerOfProjectile = GetComponent<SpawnerOfProjectile>();

        wait.passedTime = wait.waitTime;
    }

    private void Update()
    {
        if (detectChecker.firstDetectedCharacter)
        {
            wait.passedTime = 0;
            if (attackCheker.detected)
            {
                navigatorController.SetDestination(detectChecker.posToGo, MovementType.Stand);
            }
            else
            {
                waypointNavigator.StopChasingWaypoints();
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
