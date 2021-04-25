using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("Player") && !other.CompareTag("Attack")){
            if (other.CompareTag("Shark")){
                other.GetComponent<SharkControl>().StartShock();
            }
            Destroy(gameObject);
        }
    }
}
