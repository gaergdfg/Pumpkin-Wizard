using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    [Header("Basic info")]
    private static GameManager instance;

    [Header("Game state")]
    private bool remoteTeleportUnlocked = true; // TODO: set initially to false

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public bool getRemoteTeleportUnlocked() {
        return this.remoteTeleportUnlocked;
    }

}
