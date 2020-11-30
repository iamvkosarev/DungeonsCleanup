using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowsLife : MonoBehaviour
{
    [SerializeField] private GameObject deathVFXPrefab;
    [SerializeField] private float timeOnLife = 25f;
    [SerializeField] private float destroyAfterVFXdELAY = 0.5f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(timeOnLife);
        GameObject deathVFX = Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
        deathVFX.transform.parent = transform;
        yield return new WaitForSeconds(destroyAfterVFXdELAY);
        deathVFX.transform.parent =null;
        Destroy(gameObject);
    }
}
