using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float moveTarget;
    float moveAxis;
    public float moveSpeed = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        moveAxis = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        moveAxis = Input.GetAxis("Move");
        Vector3 diff = Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2, 0);
        moveTarget = Mathf.Atan2(diff.y, diff.x);
        transform.eulerAngles = new Vector3(0,0, moveTarget * Mathf.Rad2Deg);
    }

    private void FixedUpdate() {
        if (moveAxis > 0){
            Transform parent = transform.parent;
            parent.position = new Vector3(parent.position.x + Mathf.Cos(moveTarget) * moveAxis * moveSpeed, parent.position.y + Mathf.Sin(moveTarget) * moveAxis * moveSpeed, parent.position.z);
        }
    }
}
