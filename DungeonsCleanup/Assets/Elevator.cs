using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Elevator secondElevator;
    
    public void Transfer(Transform player)
    {
        //Animation

        player.position = secondElevator.transform.position;
    }
}
