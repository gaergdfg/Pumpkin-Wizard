using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Pointer : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private GameObject pointer;
    private Image pointerImage;
    private Button button;

    private Vector3 pointerPosition;

    private void Start() {
        button.onClick.AddListener(DisablePointer);

        RectTransform rectTransform = GetComponent<RectTransform>();
        float width = rectTransform.rect.width;
        float height = rectTransform.rect.height;
        pointerPosition = new Vector3(transform.position.x + width / 2, transform.position.y + height / 2, transform.position.z);
    }

    private void Awake() {
        pointer = GameObject.FindWithTag("Pointer");
        pointerImage = pointer.GetComponent<Image>();
        button = GetComponent<Button>();
    }

    private void DisablePointer() {
        pointerImage.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        pointer.transform.position = pointerPosition;
        pointerImage.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        pointerImage.enabled = false;
    }
}