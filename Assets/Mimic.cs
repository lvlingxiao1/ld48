using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : MonoBehaviour
{
	Animator animator;
	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		animator.SetTrigger("trigger");
		var player = FindObjectOfType<PlayerController>();
		player.moveable = false;
	}
}
