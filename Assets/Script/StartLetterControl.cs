using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLetterControl : MonoBehaviour
{
    void Start()
    {
        UI.UICanvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)){
            FindObjectOfType<PlayerController>().moveable = true;
            UI.UICanvas.enabled = true;
            Destroy(gameObject);
        }
    }
}
