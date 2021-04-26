using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
		FindObjectOfType<PlayerController>().moveable = false;
		GameObject.Find("revenue-shop").GetComponent<TextMeshProUGUI>().text = $"Shop Sales: $ {Shop.totalRevenue}";
		GameObject.Find("revenue-cash").GetComponent<TextMeshProUGUI>().text = $"Cash: $ {Main.CurrentGold + Main.TotalGold}";
		GameObject.Find("revenue-total").GetComponent<TextMeshProUGUI>().text = $"Total: $ {Shop.totalRevenue + Main.CurrentGold + Main.TotalGold}";
		UI.UIAnimator.SetTrigger("end");
	}
}
