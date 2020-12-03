using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeleportCanvas : MonoBehaviour
{
    [SerializeField] private float delayToBeAbleDestroy = 0.5f;
    private bool canBeDestroy = false;
    [SerializeField] private TextMeshProUGUI currentHealthField;
    [SerializeField] private TextMeshProUGUI damageField;
    LoseMenuScript loseMenuScript;
    Portal portal;

    public void SetParameters(int playerHealth, int damage, Portal portal, LoseMenuScript loseMenuScript)
    {
        this.loseMenuScript = loseMenuScript;
        this.loseMenuScript.ManagePlayerBarsAndGamepad(false);
        this.portal = portal;
        currentHealthField.text = playerHealth.ToString();
        damageField.text = damage.ToString();
    }
    private void Start()
    {
        StopGame();
        StartCoroutine(WaitingToBeAbleDestroy());
    }
    IEnumerator WaitingToBeAbleDestroy()
    {
        canBeDestroy = false;
        yield return new WaitForSeconds(delayToBeAbleDestroy);
        canBeDestroy = true;
    }
    public void ActivatePortal()
    {
        portal.StartTeleport();
        DestroyCanvas();
    }
    public void StopGame()
    {
        Time.timeScale = 0f;
    }
    public void DestroyCanvas()
    {
        if (!canBeDestroy) { return; }
        Time.timeScale = 1f;
        loseMenuScript.ManagePlayerBarsAndGamepad(true);
        Destroy(gameObject);
    }
}
