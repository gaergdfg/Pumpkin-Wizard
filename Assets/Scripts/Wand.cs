using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour {
    private GameManager gm;
    private GameObject player;

    private void Awake() {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        gm.setRemoteTeleportUnlocked(true);
        player.transform.Find("Wand").gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Destroy(gameObject);
    }
}