using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Main : MonoBehaviour
{
	private static int totalGold = 0;
	private static int currentGold = 0;
	private static TextMeshProUGUI totalGoldText;
	private static TextMeshProUGUI currentGoldText;

	private void Awake()
	{
		totalGoldText = GameObject.Find("total_gold").GetComponentInChildren<TextMeshProUGUI>();
		currentGoldText = GameObject.Find("current_gold").GetComponentInChildren<TextMeshProUGUI>();
		TotalGold = 0;
		CurrentGold = 0;
	}
	public static int TotalGold
	{
		get { return totalGold; }
		set
		{
			totalGold = value;
			totalGoldText.text = $"Total Gold: $ {value}";
		}
	}
	public static int CurrentGold
	{
		get { return currentGold; }
		set
		{
			currentGold = value;
			currentGoldText.text = $"Current Gold: $ {value}";
		}
	}
}
