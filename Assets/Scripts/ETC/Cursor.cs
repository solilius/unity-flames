using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour {

    void Start() {
       UnityEngine.Cursor.visible = false;
    }

    void Update()
    {
        transform.position = Input.mousePosition;
    }

    void OnDestroy() {
        UnityEngine.Cursor.visible = true;
    }
}
