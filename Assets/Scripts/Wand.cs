using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour {
    private GameObject player;

    private void Awake() {
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        player.GetComponent<PlayerController>().setRemoteTeleportUnlocked(true);
        Destroy(gameObject);
    }
}