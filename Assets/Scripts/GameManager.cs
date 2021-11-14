using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Basic info")]
    private static GameManager instance;

    [Header("Game state")]
    private bool remoteTeleportUnlocked = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public bool GetRemoteTeleportUnlocked() {
        return this.remoteTeleportUnlocked;
    }

    public void SetRemoteTeleportUnlocked(bool remoteTeleportUnlocked) {
        this.remoteTeleportUnlocked = remoteTeleportUnlocked;
    }
}