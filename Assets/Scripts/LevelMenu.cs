using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class LevelMenu : MonoBehaviour {

    private GameManager gm;
    private GameObject levelsGameObject;
    private GameObject[] levelStars;

    private void Awake() {
        gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        levelsGameObject = transform.Find("Levels").gameObject;
    }

    private void Start() {
        int i = 1;

        foreach (Transform levelButton in levelsGameObject.transform) {
            if (gm.isLevelBeaten(i)) {
                levelButton.Find("Star").gameObject.GetComponent<Image>().enabled = true;
            }

            i++;
        }
    }

    public void LevelButton() {
        string sceneIndex = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        SceneManager.LoadScene("Level #" + sceneIndex);
    }
}