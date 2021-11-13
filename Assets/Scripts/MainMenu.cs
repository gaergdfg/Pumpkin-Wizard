using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenu : MonoBehaviour {

    public void LevelButton() {
        string sceneIndex = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        SceneManager.LoadScene("Level #" + sceneIndex);
    }

    public void QuitButton() {
        Application.Quit();
    }
}