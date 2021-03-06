using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [Header("Basic info")]
    private static GameManager instance;

    [Header("Game state")]
    private int levelNo = 18;
    private bool[] levelBeaten;
    private int levelBeatenCount = 0;
    private bool goToLevelSelect = false;
    private bool gameCompleted = false;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        this.levelBeaten = new bool[levelNo + 1];
    }

    public void markLevelBeaten(int levelId) {
        if (levelId > levelNo || this.levelBeaten[levelId]) {
            return;
        }

        this.levelBeaten[levelId] = true;

        if (++levelBeatenCount == levelNo) {
            gameCompleted = true;
        }
    }

    public bool getGoToLevelSelect() {
        return this.goToLevelSelect;
    }

    public void setGoToLevelSelect(bool goToLevelSelect) {
        this.goToLevelSelect = goToLevelSelect;
    }

    public bool isLevelBeaten(int level) {
        return levelBeaten[level];
    }

    public bool getGameCompleted() {
        return this.gameCompleted;
    }

}
