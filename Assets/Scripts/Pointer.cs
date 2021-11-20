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
        float width = rectTransform.rect.width * canvas.transform.localScale.x;
        float height = rectTransform.rect.height * canvas.transform.localScale.y;
        pointerPosition = new Vector3(transform.position.x + width / 2, transform.position.y + height / 2, transform.position.z);
    }

    private void DisablePointer() {
        pointerImage.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        pointer.transform.position = pointerPosition;
        pointerImage.enabled = true;
        audioManager.play("button_hover");
    }

    public void OnPointerExit(PointerEventData eventData) {
        pointerImage.enabled = false;
    }
}