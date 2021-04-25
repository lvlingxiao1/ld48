using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureDamageControl : MonoBehaviour
{
    public int level = 1;

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController controller = other.GetComponentInChildren<PlayerController>();
            if (controller.suitLevel < level)
            {
                controller.inPressure = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController controller = other.GetComponentInChildren<PlayerController>();
            controller.inPressure = false;
        }
    }
}
