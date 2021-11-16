using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenu : MonoBehaviour {

    [SerializeField] private GameObject levelMenu;

    private GameManager gm;
    private Image pumpkinImage;

    private void Awake() {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        pumpkinImage = gameObject.transform.Find("Pumpkin").GetComponent<Image>();
    }

    public void Start() {
        if (gm.getGoToLevelSelect()) {
            gameObject.SetActive(false);
            levelMenu.SetActive(true);
        }
    }

    private void OnEnable() {
        if (gm.getGameCompleted()) {
            pumpkinImage.enabled = true;
        }
    }

    public void QuitButton() {
        Application.Quit();
    }
}