using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemieSpawner : MonoBehaviour
{
    [SerializeField] int spawningEnemieIndex = 0;
    [SerializeField] GameObject[] enemiesPrefabs;
    [SerializeField] LayerMask activatorLayer;
    [Range(0f, 10f)][SerializeField] float checkActivaterZoneRaduis = 3f;
    [SerializeField] WaypointRoot waypointRoot;

    private void Update()
    {
        if (waypointRoot == null)
        {
            Debug.LogError("Root component must be selected. Please assign a root component.");
        }
        bool detected = Physics2D.OverlapCircle(transform.position, checkActivaterZoneRaduis, activatorLayer);
        if (detected)
        {
            if(enemiesPrefabs.Length != 0)
            {
                GameObject newEnemie = Instantiate(enemiesPrefabs[spawningEnemieIndex], transform.position, Quaternion.identity);
                WaypointNavigator waypointNavigator = newEnemie.GetComponent<WaypointNavigator>();
                waypointNavigator.waypointRoot = waypointRoot;
                Destroy(gameObject);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;

        Gizmos.DrawWireSphere(transform.position, checkActivaterZoneRaduis);
    }

}
