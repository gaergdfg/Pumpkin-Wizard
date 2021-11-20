using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private GameObject pointer;
    private Image pointerImage;
    private AudioManager audioManager;
    private Button button;
    private GameObject canvas;

    private float buttonWidth;
    private float buttonHeight;
    private Vector3 pointerPosition;

    private void Awake() {
        pointer = GameObject.FindWithTag("Pointer");
        pointerImage = pointer.GetComponent<Image>();
        audioManager = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        button = GetComponent<Button>();
        canvas = GameObject.FindWithTag("Canvas");
    }

    private void Start() {
        button.onClick.AddListener(DisablePointer);

        RectTransform rectTransform = GetComponent<RectTransform>();
        buttonWidth = rectTransform.rect.width;
        buttonHeight = rectTransform.rect.height;
    }

    private void DisablePointer() {
        pointerImage.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        pointerPosition = new Vector3(transform.position.x + buttonWidth * canvas.transform.localScale.x / 2, transform.position.y + buttonHeight * canvas.transform.localScale.y / 2, transform.position.z);
        pointer.transform.position = pointerPosition;
        pointerImage.enabled = true;
        audioManager.play("button_hover");
    }

    public void OnPointerExit(PointerEventData eventData) {
        pointerImage.enabled = false;
    }
}