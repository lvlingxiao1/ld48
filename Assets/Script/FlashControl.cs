using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashControl : MonoBehaviour
{
    public bool end;
    // Start is called before the first frame update
    void Start()
    {
        end = false;
    }

    private void FixedUpdate() {
        if (end){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        
    }
}
