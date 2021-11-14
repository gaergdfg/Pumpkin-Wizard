using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChange : MonoBehaviour {
    [SerializeField] private Texture2D cursor;

    private void Start() {
        Cursor.SetCursor(cursor, Vector3.zero, CursorMode.ForceSoftware);
    }
}