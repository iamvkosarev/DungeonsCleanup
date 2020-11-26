using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : MonoBehaviour
{
    [SerializeField] private GameObject forEarthquakeGround;
    private GoblinBossAttack goblinBoss;
    private int startNumberOfGrounds;
    // Start is called before the first frame update

    public void SetBoss(GoblinBossAttack goblinBossAttack)
    {
        goblinBoss = goblinBossAttack;
        startNumberOfGrounds = goblinBoss.numberOfGrounds;
        StartCoroutine(StartAttack());
    }
    private IEnumerator StartAttack()
    {
        while(goblinBoss.numberOfGrounds != 0)
        {
            GameObject ground = Instantiate(forEarthquakeGround, 
                new Vector2(transform.position.x - 
                Mathf.Sign(goblinBoss.gameObject.transform.rotation.y) * ((startNumberOfGrounds - goblinBoss.numberOfGrounds) * 0.6f + 1f), goblinBoss.groundYPosition), transform.rotation);
            yield return new WaitForSeconds(goblinBoss.perionOfSpawn);
            ground.transform.SetParent(gameObject.transform);
            goblinBoss.numberOfGrounds--;
        }

        goblinBoss.numberOfGrounds = startNumberOfGrounds;
    }

}
