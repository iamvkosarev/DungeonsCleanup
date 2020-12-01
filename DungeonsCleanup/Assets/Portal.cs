using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private GameObject teleportCanvasPrefab;
    [SerializeField] private Transform pointToTeleport;
    [SerializeField] private int damage = 80;
    private Animator animator;
    private PlayerHealth playerHealth;
    private Transform playerTransform;
    private PlayerMovement playerMovement;
    private SpriteRenderer playerSpriteRenderer;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    public void InstansiatePortalInfoCanvas(PlayerHealth playerHealth, Transform playerTransform, PlayerMovement playerMovement, LoseMenuScript loseMenuScript, SpriteRenderer playerSpriteRenderer)
    {
        playerTransform.position = transform.position;
        GameObject canvas = Instantiate(teleportCanvasPrefab);
        TeleportCanvas teleportCanvas = canvas.GetComponent<TeleportCanvas>();
        this.playerHealth = playerHealth;
        this.playerMovement = playerMovement;
        this.playerSpriteRenderer = playerSpriteRenderer;
        this.playerTransform = playerTransform;
        teleportCanvas.SetParameters(playerHealth.GetHealth(), damage, this, loseMenuScript);
    }
    public void PlaySFX()
    {
        audioSource.Play();
    }
    public void PlayerInvis()
    {
        playerSpriteRenderer.color = new Color(1, 1, 1, 0);
    }
    public void PlayerVisible()
    {
        playerSpriteRenderer.color = new Color(1, 1, 1, 1);
    }
    public void StartTeleport()
    {
        playerMovement.StopRotating();
        playerMovement.StopHorizontalMovement();
        playerMovement.StopTumbleweed();
        playerMovement.StopGroundJumps();
        animator.Play("Portal Teleport");
    }

    public void Teleport()
    {
        playerMovement.StartHorizontalMovement();
        playerMovement.StartRotaing();
        playerMovement.StartGroundJumps();
        playerTransform.position = pointToTeleport.position;
        PlayerVisible();
        playerHealth.TakeAwayHelath(damage);
    }
}
