using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    [SerializeField] private float startTimeBtwSpawns = 3f;
    [SerializeField] private float destroyDelay = 0.2f;
    [SerializeField] private GameObject echo;

    private float timeBtwSpawns;
    private void Update()
    {
        if (timeBtwSpawns <= 0)
        {
            GameObject newClone =  Instantiate(echo, echo.transform.position, Quaternion.identity);
            if(echo.transform.rotation.z == 0)
            {
                newClone.transform.rotation = transform.rotation;
            }
            else
            {
                newClone.transform.rotation = echo.transform.rotation;
            }
            newClone.GetComponent<ChangeAlphaChannel>().SetTimeOnChange(destroyDelay);
            Destroy(newClone, destroyDelay);
            timeBtwSpawns = startTimeBtwSpawns;
        }
        else
        {
            timeBtwSpawns -= Time.deltaTime;
        }
    }
}
