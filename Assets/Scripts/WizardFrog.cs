using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardFrog : MonoBehaviour {

    [Header("Unity references")]
    public GameObject teleportRangeIndicator;
    private ParticleSystem particles;
    private PlayerController pc;
    private AudioManager am;
    
    [Header("Basic info")]
    public float teleportRange = 2.625f;
    private bool playerInRange = false;
    private bool teleportable = false;

    void Start() {
        this.pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        this.particles = gameObject.GetComponentInChildren<ParticleSystem>();
        this.am = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Update() {
        // enable the teleport range indicator if player is close enough and wants to interact
        if (this.playerInRange && Input.GetKeyDown(KeyCode.E) && !this.teleportable) {
            this.enableIndicator();
        }

        // disable the teleport range indicator if player interacts with the wizard frog for the second time
        else if (this.teleportable && Input.GetKeyDown(KeyCode.E)) {
            this.disableIndicator();
        }

        // try teleporting the wizard frog if it's in correct state and user pressed LMB
        if (this.teleportable && Input.GetMouseButtonDown(0)) {
            this.teleport(Input.mousePosition);
        }
    }

    private void OnCollisionEnter2D(Collision2D collission) {
        // TODO: enable "Press E to teleport the Wizard Frog" dialog OR do some sparkly thingy

        this.playerInRange = true;
    }

    private void OnCollisionExit2D(Collision2D collission) {
        this.disableIndicator();

        this.playerInRange = false;
    }

    public void enableIndicator() {
        if (this.teleportRangeIndicator.activeSelf) {
            return;
        }

        this.pc.disableControls();
        this.pc.stop();

        this.teleportRangeIndicator.SetActive(true);

        this.teleportable = true;
    }

    public void disableIndicator() {
        if (!this.teleportRangeIndicator.activeSelf) {
            return;
        }
        
        this.pc.enableControls();

        this.teleportRangeIndicator.SetActive(false);

        this.teleportable = false;
    }

    public void teleport(Vector3 mousePosition) {
        // convert mouse position to world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0f;

        float distance = Vector3.Distance(transform.position, worldPosition);

        if (distance > this.teleportRange) {
            return;
        }

        // teleport the wizard frog
        this.transform.position = worldPosition;

        // display particles
        this.particles.Play();

        // play sound
        am.play("frog_smoke");

        // clean up
        this.disableIndicator();

        Destroy(this); // TODO: consider removing the limit
    }

}
