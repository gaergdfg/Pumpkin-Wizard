using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    [Header("Unity references")]
    public Transform player;
    private Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundMask;
    private GameManager gm;
    private AudioManager am;

    [Header("Basic info")]
    public float speed = 4.5f;
    public float jumpForce = 9f;
    public float checkRadius = 0.55f;
    private bool facingRight = true;
    private bool controlsDisabled = false;
    private bool remoteTeleportUnlocked = false;

    void Start() {
        this.rb = GetComponent<Rigidbody2D>();
        this.gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        this.am = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Update() {
        // restart the level if player presses R or if he fell off
        if (Input.GetKeyDown(KeyCode.R) || transform.position.y <= -10f) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            // TODO: possible music change
            am.stop("frog_talk");
            this.gm.setGoToLevelSelect(true);
            SceneManager.LoadScene("Menu");
        }

        if (controlsDisabled) {
            return;
        }

        // getting player input
        float input = Input.GetAxisRaw("Horizontal");

        // move player horizontally
        this.rb.velocity = new Vector2(input * speed, this.rb.velocity.y);

        // flip player sprite when moving left/right
        if ((input < 0 && this.facingRight) || (input > 0 && !this.facingRight)) {
            this.flipPlayerX();
        }

        // jump if player is grounded and presses jump
        bool isGrounded = Physics2D.Raycast(this.groundCheck.position, Vector2.down, this.checkRadius, this.groundMask);
        if (isGrounded && playerWantsToJump()) {
            rb.velocity = Vector2.up * this.jumpForce;
            am.play("player_jump");
        }

        // try toggling wizard frog's teleport range indicator
        if (this.getRemoteTeleportUnlocked() && Input.GetKeyDown(KeyCode.E)) {
            this.activateWizardFrogTeleport(Input.mousePosition);
        }
    }

    public void enableControls() {
        this.controlsDisabled = false;
    }

    public void disableControls() {
        this.controlsDisabled = true;
    }

    public void stop() {
        this.rb.velocity = Vector2.zero;
        this.rb.angularVelocity = 0f;
    }
    public void setRemoteTeleportUnlocked(bool remoteTeleportUnlocked) {
        this.remoteTeleportUnlocked = remoteTeleportUnlocked;
        transform.Find("Wand").gameObject.GetComponent<SpriteRenderer>().enabled = remoteTeleportUnlocked;
    }

    public bool getRemoteTeleportUnlocked() {
        return this.remoteTeleportUnlocked;
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
