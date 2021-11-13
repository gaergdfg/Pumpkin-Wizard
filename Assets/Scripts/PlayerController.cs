using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("Unity references")]
    public Transform player;
    private Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundMask;
    private GameManager gm;

    [Header("Basic info")]
    public float speed = 4.5f;
    public float jumpForce = 9f;
    public float checkRadius = 0.55f;
    private bool facingRight = true;
    private bool controlsDisabled = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update() {
        if (controlsDisabled) {
            return;
        }

        // getting player input
        float input = Input.GetAxisRaw("Horizontal");

        // move player horizontally
        rb.velocity = new Vector2(input * speed, rb.velocity.y);

        // flip player sprite when moving left/right
        if ((input < 0 && this.facingRight) || (input > 0 && !this.facingRight)) {
            this.flipPlayerX();
        }

        // jump if player is grounded and presses jump
        bool isGrounded = Physics2D.OverlapCircle(this.groundCheck.position, this.checkRadius, this.groundMask);
        if (isGrounded && playerWantsToJump()) {
            rb.velocity = Vector2.up * this.jumpForce;
        }

        // try toggling wizard frog's teleport range indicator
        if (this.gm.getRemoteTeleportUnlocked() && Input.GetKeyDown(KeyCode.E)) {
            this.activateWizardFrogTeleport(Input.mousePosition);
        }
    }

    public void enableControls() {
        this.controlsDisabled = false;
    }

    public void disableControls() {
        this.controlsDisabled = true;
    }

    private void flipPlayerX() {
        this.player.localScale = new Vector3(
            -this.player.localScale.x,
            this.player.localScale.y,
            this.player.localScale.z
        );
        this.facingRight = !this.facingRight;
    }

    private bool playerWantsToJump() {
        return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space);
    }

    private void activateWizardFrogTeleport(Vector3 mousePosition) {
        // convert mouse position to world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 worldPosition2D = new Vector2(worldPosition.x, worldPosition.y);

        // get wizard frog near the mouse cursor
        Vector2 size = new Vector2(0.5f, 0.5f);
        Collider2D wizardFrog = Physics2D.OverlapBox(worldPosition2D, size, 0f);

        if (!wizardFrog) {
            return;
        }

        WizardFrog wizardFrogScript = wizardFrog.GetComponent<WizardFrog>(); // TODO: consider removing the check
        if (wizardFrogScript == null) {
            return;
        }

        wizardFrogScript.enableIndicator();
    }

}
