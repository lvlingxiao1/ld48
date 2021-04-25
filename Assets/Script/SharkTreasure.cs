using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkTreasure : MonoBehaviour
{
    public int value;
    bool pickable;

    SharkContro shark;
    private void Start() {
        pickable = false;
        shark = GetComponentInParent<SharkContro>();
    }

    private void FixedUpdate() {
        if (!pickable && shark.shockCounter > 0){
            pickable = true;
            transform.parent = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (pickable && other.CompareTag("Player"))
        {
            FindObjectOfType<Main>().CurrentGold += value;
            Destroy(gameObject);
        }
    }
}
