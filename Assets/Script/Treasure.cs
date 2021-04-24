using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
	public int value;
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			FindObjectOfType<Main>().CurrentGold += value;
			Destroy(gameObject);
		}
	}
}
