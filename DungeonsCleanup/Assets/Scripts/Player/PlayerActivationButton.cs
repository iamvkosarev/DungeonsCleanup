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
    [SerializeField] LayerMask weaponLayer;
    [SerializeField] LayerMask elevatorLayer;
    [SerializeField] LayerMask doorLayer;
    [SerializeField] LayerMask tabletLayer;
    [SerializeField] LayerMask itemLayer;
    [SerializeField] LayerMask absorptionShadowLayer;
    [SerializeField] LayerMask portalLayer;
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
    bool isReadyToActivateAbsorption;
    bool canPlayerActivateSomeThing;
    bool canActivateAbsorption;
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
        Collider2D portalCollider = Physics2D.OverlapCircle(transform.position, checkRadius, portalLayer);
        if (portalCollider != null)
        {
            Debug.Log("Portal!");
            portalCollider.GetComponent<Portal>().InstansiatePortalInfoCanvas(playerHealth,transform,playerMovement,loseMenuScript, bodySpriteRenderer);
        }
    }

    private void AbsorptionShadow()
    {
        if (canActivateAbsorption)
        {
            isReadyToActivateAbsorption = true;
        }
    }

    public void CanActivateAbsorption(bool mode)
    {
        canActivateAbsorption = mode;
        if (!mode)
        {
            isReadyToActivateAbsorption = false;
        }
    }
    public bool IsReadyForActivationAbsorption()
    {
        return isReadyToActivateAbsorption;
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
            if (canActivateAbsorption && playerDevelopmentManager.IsCurrentSelectedItemAShadowBorrle())
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
        bool isPlayerTouchElevator = Physics2D.OverlapCircle(transform.position, checkRadius, elevatorLayer);
        bool isPlayerTouchTablet = Physics2D.OverlapCircle(transform.position, checkRadius, tabletLayer);
        bool isPlayerTouchItem = Physics2D.OverlapCircle(transform.position, checkRadius, itemLayer);
        bool isPlayerTouchPortal = Physics2D.OverlapCircle(transform.position, checkRadius, portalLayer);
        bool isEnemyReadyToAbsorption = Physics2D.OverlapCircle(transform.position, checkRadius, absorptionShadowLayer);
        bool isCurrentItemIsShadowBottle = playerDevelopmentManager.IsCurrentSelectedItemAShadowBorrle();
        canPlayerActivateSomeThing = (canActivateHatch || isPlayerTouchPortal || isPlayerTouchDoor || isPlayerTouchElevator || isPlayerTouchTablet || isPlayerTouchItem || isEnemyReadyToAbsorption && isCurrentItemIsShadowBottle);
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
        Collider2D elevatorCollider =  Physics2D.OverlapCircle(transform.position, checkRadius, elevatorLayer);
        if (elevatorCollider != null)
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
        Collider2D paperCollider = Physics2D.OverlapCircle(transform.position, checkRadius, tabletLayer);
        if (paperCollider != null)
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
