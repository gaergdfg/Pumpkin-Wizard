using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Intro : MonoBehaviour {

    [Header("Unity references")]
    private PlayerController pc;
    public TextMeshProUGUI textContainer;
    public GameObject introOverlay;
    public Animator animator;
    
    [Header("Basic info")]
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

        this.pc.disableControls();
        this.introOverlay.SetActive(true);

        this.text += "\n\nPress any button to continue";
        this.textArray = Regex.Split(this.text, @"");
        this.textContainer.text = "";
    }

    void Update() {
        if (this.timeBetweenChars <= 0f) {
            if (this.currCharIndex < this.textArray.Length) {
                string currChar = this.textArray[this.currCharIndex++];
                this.displayText += currChar;
                this.textContainer.text = this.displayText;

                if (this.isStopCharacter(currChar)) {
                    this.timeBetweenChars = this.timeOnWhitespaceChar;
                    if (this.isTalking) {
                        this.animator.SetBool("talk", false);
                        this.isTalking = false;
                    }
                } else {
                    this.timeBetweenChars = this.timeBetweenCharsBase;
                    if (!this.isTalking) {
                        this.animator.SetBool("talk", true);
                        this.isTalking = true;
                    }
                }
            } else {
                this.animator.SetBool("talk", false);

                if (Input.anyKey) {
                    this.introOverlay.SetActive(false);
                    this.pc.enableControls();
                }
            }
        } else {
            this.timeBetweenChars -= Time.deltaTime;
        }
    }

    private bool isStopCharacter(string c) {
        return c == " " || c == "," || c == "." || c == "!" || c == "?";
    }

}
