using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Basic info")]
    private static GameManager instance;

    [Header("Game state")]
    public bool remoteTeleportUnlocked = false;
    private int levelNo = 1;
    private bool[] levelBeaten;
    private bool goToLevelSelect = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        this.levelBeaten = new bool[levelNo + 1];
    }

    public void setRemoteTeleportUnlocked(bool remoteTeleportUnlocked) {
        this.remoteTeleportUnlocked = remoteTeleportUnlocked;
    }
    
    public bool getRemoteTeleportUnlocked() {
        return this.remoteTeleportUnlocked;
    }

    public void markLevelBeaten(int levelId) {
        if (levelId > levelNo) {
            return;
        }

        this.levelBeaten[levelId] = true;
    }

    public bool getGoToLevelSelect() {
        return this.goToLevelSelect;
    }

    public void setGoToLevelSelect(bool goToLevelSelect) {
        this.goToLevelSelect = goToLevelSelect;
    }

}
