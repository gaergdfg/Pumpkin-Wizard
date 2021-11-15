using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ghost : MonoBehaviour {

    [Header("Unity references")]
    public Transform pointA;
    public Transform pointB;

    [Header("Basic info")]
    public float speed = 1f;
    private float threshold = 0.05f;
    private bool goingTowardsB = true;

    void FixedUpdate() {
        Vector2 targetPosition = this.goingTowardsB ? pointB.position : pointA.position;
        float diff = Vector2.Distance(targetPosition, transform.position);

        if (diff <= this.threshold) {
            this.goingTowardsB = !this.goingTowardsB;
        } else {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, this.speed * Time.fixedDeltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag != "Player") {
            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
