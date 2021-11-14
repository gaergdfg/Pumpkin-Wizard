using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    [Header("Basic info")]
    private static GameManager instance;

    [Header("Game state")]
    private bool remoteTeleportUnlocked = true; // TODO: set initially to false
    private int levelNo = 1;
    private bool[] levelBeaten;
    private bool goToLevelSelect = false;

    void Awake() {
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
