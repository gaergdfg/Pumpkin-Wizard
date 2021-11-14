using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenu : MonoBehaviour {

    [SerializeField] private GameObject levelMenu;

    private GameManager gm;

    private void Awake() {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    public void Start() {
        if (gm.getGoToLevelSelect()) {
            gameObject.SetActive(false);
            levelMenu.SetActive(true);
        }
    }

    public void QuitButton() {
        Application.Quit();
    }
}