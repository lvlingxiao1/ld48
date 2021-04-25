using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkTreasure : MonoBehaviour
{
	public int value;
	bool pickable;

	SharkControl shark;
	private void Start()
	{
		pickable = false;
		shark = GetComponentInParent<SharkControl>();
	}

	private void FixedUpdate()
	{
		if (!pickable && shark.shockCounter > 0)
		{
			pickable = true;
			transform.parent = null;
		}
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (pickable && other.CompareTag("Player"))
		{
			Main.CurrentGold += value;
			Destroy(gameObject);
		}
	}
}
