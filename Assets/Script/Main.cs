using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Main : MonoBehaviour
{
	private static int totalGold = 0;
	private static int currentGold = 0;
	private static PlayerController playerController;

	private void Start()
	{
		TotalGold = 88888;
		CurrentGold = 0;
		playerController = FindObjectOfType<PlayerController>();
	}
	public static int TotalGold
	{
		get { return totalGold; }
		set
		{
			totalGold = value;
			UI.totalGoldText.text = $"Total Gold: $ {value}";
		}
	}
	public static int CurrentGold
	{
		get { return currentGold; }
		set
		{
			currentGold = value;
			UI.currentGoldText.text = $"Current Gold: $ {value}"; ;
		}
	}

	public static void PlayerDead(Vector3 position)
	{
		if (CurrentGold > 0)
		{
			GameObject lostMoney = Resources.Load<GameObject>("LostMoney");
			GameObject newLostMoney = Instantiate(lostMoney);
			newLostMoney.GetComponent<Treasure>().value = CurrentGold;
			CurrentGold = 0;
			newLostMoney.transform.position = position;
		}
        playerController.moveable = false;
		UI.UIAnimator.SetTrigger("inDead");
	}
}
