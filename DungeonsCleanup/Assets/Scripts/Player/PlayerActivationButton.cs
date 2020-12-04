using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerActivationButton : MonoBehaviour
{
    [SerializeField] string baseButtonName = "Открыть";
    [SerializeField] string absorptionButtonName = "Поглатить";
    [SerializeField] float checkRadius;
    [Header("Layers")]
    [SerializeField] LayerMask interactionWithPlayerLayer;
    [SerializeField] LayerMask weaponLayer;
    [SerializeField] string elevatorTag;
    [SerializeField] LayerMask doorLayer;
    [SerializeField] string tabletTag;
    [SerializeField] LayerMask itemLayer;
    [SerializeField] string absorptionShadowTag;
    [SerializeField] string portalTag;
    [SerializeField] ActivateSomeThingButton activateSomeThingButton;

    [SerializeField] SpriteRenderer bodySpriteRenderer;

    [Header("OpenDoor")]
    [SerializeField] Transform doorCheckPoint;
    [SerializeField] Vector2 doorCheckSize;

    LoseMenuScript loseMenuScript;
    PlayerActionControls playerActionControls;
    PlayerAttackManager playerAttackManager;
    PlayerMovement playerMovement;
    PlayerDevelopmentManager playerDevelopmentManager;
    TextMeshProUGUI buttonsTextMPro;
    PlayerHealth playerHealth;
    bool canActivateHatch;
    bool isReadyToActivateHatch;
    bool canPlayerActivateSomeThing;
    bool isEnemyReadyToAbsorption;
    bool isCurrentItemIsShadowBottle;
    private void Awake()
    {
        playerActionControls = new PlayerActionControls();
        playerActionControls.Land.ActivateSomething.started += _ => ActivateSomeThing();

    }
    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerMovement = GetComponent<PlayerMovement>();
        buttonsTextMPro = activateSomeThingButton.GetComponentInChildren<TextMeshProUGUI>();
        playerDevelopmentManager = GetComponent<PlayerDevelopmentManager>();
        playerAttackManager = GetComponent<PlayerAttackManager>();
        loseMenuScript = playerHealth.GetLoseCanvasScripts();
    }

    private void ActivateSomeThing()
    {
        if (!canPlayerActivateSomeThing) { return; }
        OpenDoor();
        TransferPlayer();
        ShowTabletText();
        ShowItmeCanvas();
        ShowPortalCanvas();
        ActivateHatch();
        AbsorptionShadow();
    }

    private void ShowPortalCanvas()
    {
        Collider2D portalCollider = Physics2D.OverlapCircle(transform.position, checkRadius, interactionWithPlayerLayer);
        if (portalCollider != null && portalCollider.gameObject.tag == portalTag)
        {
            Debug.Log("Portal!");
            portalCollider.GetComponent<Portal>().InstansiatePortalInfoCanvas(playerHealth,transform,playerMovement,loseMenuScript, bodySpriteRenderer);
        }
    }

    private void AbsorptionShadow()
    {
        bool isCurrentItemIsShadowBottle = playerDevelopmentManager.IsCurrentSelectedItemAShadowBorrle();
        Collider2D absorptionCollider = Physics2D.OverlapCircle(transform.position, checkRadius, interactionWithPlayerLayer);
        if (absorptionCollider && isCurrentItemIsShadowBottle && absorptionCollider.gameObject.tag == absorptionShadowTag)
        {
            AbsorptionShadow absorptionShadow = absorptionCollider.gameObject.GetComponent<AbsorptionShadow>();
            playerDevelopmentManager.AddShadow(absorptionShadow.shadowId);
            Destroy(absorptionCollider.gameObject);
        }

    }

    private void Update()
    {
        CheckPossibilityToActivateSomeThing();
        SwitchingActivateButton();
    }
    private void ActivateHatch()
    {
        if (canActivateHatch)
        {
            isReadyToActivateHatch = true;
        }
    }

    internal void CanActivateHatch(bool mode)
    {
        canActivateHatch = mode;
        if (!mode)
        {
            isReadyToActivateHatch = false;
        }
    }
    public bool IsReadyForActivationHatch()
    {
        return isReadyToActivateHatch;
    }

    private void SwitchingActivateButton()
    {
        if (canPlayerActivateSomeThing)
        {
            if (isEnemyReadyToAbsorption && isCurrentItemIsShadowBottle)
            {
                buttonsTextMPro.text = absorptionButtonName;
            }
            else
            {
                buttonsTextMPro.text = baseButtonName;
            }
            activateSomeThingButton.SwitchOn();
        }
        else
        {
            activateSomeThingButton.SwitchOff();
        }
    }

    private void CheckPossibilityToActivateSomeThing()
    {
        bool isPlayerTouchDoor = Physics2D.OverlapBox(doorCheckPoint.position, doorCheckSize, 0, doorLayer);
        bool isTouchInteractibleObject = Physics2D.OverlapCircle(transform.position, checkRadius, interactionWithPlayerLayer);
        bool isPlayerTouchItem = Physics2D.OverlapCircle(transform.position, checkRadius, itemLayer);
        isCurrentItemIsShadowBottle = playerDevelopmentManager.IsCurrentSelectedItemAShadowBorrle();
        canPlayerActivateSomeThing = (canActivateHatch|| isTouchInteractibleObject || isPlayerTouchItem || isEnemyReadyToAbsorption && isCurrentItemIsShadowBottle);
    }



    private void OpenDoor()
    {
        Collider2D doorCollider = Physics2D.OverlapBox(doorCheckPoint.position, doorCheckSize, 0, doorLayer);
        if (doorCollider != null)
        {
            Debug.Log("Open door!");
            doorCollider.gameObject.GetComponent<Door>().Open();
        }
    }

    private void TransferPlayer()
    {
        Collider2D elevatorCollider =  Physics2D.OverlapCircle(transform.position, checkRadius, interactionWithPlayerLayer);
        if (elevatorCollider != null && elevatorCollider.gameObject.tag == elevatorTag)
        {
            Debug.Log("Teleportation!");
            elevatorCollider.gameObject.GetComponent<Elevator>().Transfer(gameObject.transform);
        }
    }
    private void ShowItmeCanvas()
    {
        Collider2D itemCollider = Physics2D.OverlapCircle(transform.position, checkRadius, itemLayer);
        if (itemCollider != null)
        {
            Debug.Log("Item!");
            itemCollider.GetComponent<Item>().InstansiateItemInfoCanvas(playerDevelopmentManager, loseMenuScript);
        }
    }
    private void ShowTabletText()
    {
        Collider2D paperCollider = Physics2D.OverlapCircle(transform.position, checkRadius, interactionWithPlayerLayer);
        if (paperCollider != null && paperCollider.gameObject.tag == tabletTag)
        {
            Debug.Log("Tablet Text!");
            paperCollider.GetComponent<Paper>().InstansiateTabletCanvas();
        }
    }

    private void OnEnable()
    {
        playerActionControls.Enable();
    }
    private void OnDisable()
    {
        playerActionControls.Disable();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(doorCheckPoint.position, doorCheckSize);
    }
}
