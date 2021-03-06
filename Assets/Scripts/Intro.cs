using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    [Header("Unity references")]
    private PlayerController pc;
    private GameManager gm;
    private AudioManager am;
    public TextMeshProUGUI textContainer;
    public GameObject introOverlay;
    public Animator animator;

    [Header("Basic info")]
    public bool endScene = false;
    public string text = "";
    private string[] textArray;

    private string displayText = "";
    private int currCharIndex = 0;

    private float timeBetweenChars = 0f;
    private float timeBetweenCharsBase = 0.037f;
    private float timeOnWhitespaceChar = 0.348f;
    private bool isTalking = false;

    void Start() {
        this.pc = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        this.gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        this.am = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();

        this.pc.disableControls();
        this.introOverlay.SetActive(true);

        this.text += "\n\nPress <color=\"green\">Space<color=\"white\"> to continue";
        this.textArray = Regex.Split(this.text, @"");
        this.textContainer.text = "";

        this.am.play("frog_talk");
    }

    void Update() {
        if (this.timeBetweenChars <= 0f) {
            if (this.currCharIndex < this.textArray.Length) {
                string currChar = this.textArray[this.currCharIndex++];
                this.displayText += currChar;
                if (currChar == "<") {
                    for (; this.textArray[this.currCharIndex] != ">";) {
                        this.displayText += this.textArray[this.currCharIndex++];
                    }
                    this.displayText += this.textArray[this.currCharIndex++];
                }

                this.textContainer.text = this.displayText;

                if (this.isStopCharacter(currChar)) {
                    this.timeBetweenChars = this.timeOnWhitespaceChar;
                    if (this.isTalking) {
                        this.am.stop("frog_talk");
                        this.animator.SetBool("talk", false);
                        this.isTalking = false;
                    }
                } else {
                    this.timeBetweenChars = this.timeBetweenCharsBase;
                    if (!this.isTalking) {
                        this.am.play("frog_talk");
                        this.animator.SetBool("talk", true);
                        this.isTalking = true;
                    }
                }
            } else {
                this.am.stop("frog_talk");
                this.animator.SetBool("talk", false);

                if (Input.GetKeyDown(KeyCode.Space)) {
                    if (endScene) {
                        this.gm.setGoToLevelSelect(true);
                        SceneManager.LoadScene("Menu");
                    }
                    else {
                        this.introOverlay.SetActive(false);
                        StartCoroutine(this.awaitEnableControls());
                    }
                }
            }
        } else {
            this.timeBetweenChars -= Time.deltaTime;
        }

        if (this.currCharIndex < this.textArray.Length && Input.anyKey) {
            this.currCharIndex = this.textArray.Length;
            this.displayText = this.text;
            this.textContainer.text = this.displayText;
            this.animator.SetBool("talk", false);
        }
    }

    private bool isStopCharacter(string c) {
        return c == "," || c == "." || c == "!" || c == "?";
    }

    private IEnumerator awaitEnableControls() {
        yield return new WaitForSeconds(0.1f);
        this.pc.enableControls();
    }

}
