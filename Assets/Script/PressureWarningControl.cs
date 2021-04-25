using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureWarningControl : MonoBehaviour
{
    Transform player;
    GameObject warning;
    public int level = 1;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
        warning = GameObject.Find("PressureWarning");
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            PlayerController controller = other.GetComponentInChildren<PlayerController>();
            if (controller.suitLevel < level){
                warning.GetComponent<SpriteRenderer>().enabled = true;
                controller.SetWarningEnterPosition();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            warning.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
