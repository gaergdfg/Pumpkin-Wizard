using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour {

    [Header("Unity references")]
    private GameManager gm;
    private AudioManager am;

    void Start() {
        this.gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        this.am = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag != "Player") {
            return;
        }

        // mark level as beaten
        string sceneName = SceneManager.GetActiveScene().name;
        int sceneId = this.getSceneId(sceneName);
        this.gm.markLevelBeaten(sceneId);

        // go back to menu
        this.gm.setGoToLevelSelect(true);
        this.am.play("portal");
        SceneManager.LoadScene("Menu");
    }

    private int getSceneId(string sceneName) {
        int i = 0;
        while (sceneName[i++] != '#') {}

        int sceneId = 0;
        for (; i < sceneName.Length; i++) {
            sceneId *= 10;
            sceneId += sceneName[i] - '0';
        }

        return sceneId;
    }

}
