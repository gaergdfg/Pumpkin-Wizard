using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wand : MonoBehaviour {

    private GameObject player;
    private AudioManager am;

    private float speed = 1f;
    private float height = 0.35f;

    private Vector3 originalPosition;

    void Awake() {
        player = GameObject.FindWithTag("Player");
        am = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Start() {
        this.originalPosition = transform.position;
    }
 
    void Update() {
        float offset = Mathf.Sin(Time.time * this.speed) * this.height;
        transform.position = this.originalPosition + new Vector3(0f, offset, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        player.GetComponent<PlayerController>().setRemoteTeleportUnlocked(true);
        am.play("wand_pickup");
        Destroy(gameObject);
    }

}