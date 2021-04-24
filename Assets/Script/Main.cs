using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Main : MonoBehaviour
{
	private int totalGold = 0;
	private int currentGold = 0;
	private TextMeshProUGUI totalGoldText;
	private TextMeshProUGUI currentGoldText;


	private void Awake()
	{
		totalGoldText = GameObject.Find("total_gold").GetComponentInChildren<TextMeshProUGUI>();
		currentGoldText = GameObject.Find("current_gold").GetComponentInChildren<TextMeshProUGUI>();
		TotalGold = 0;
		CurrentGold = 0;
	}
	public int TotalGold
	{
		get { return totalGold; }
		set
		{
			totalGold = value;
			totalGoldText.text = $"Total Gold: $ {value}";
		}
	}
	public int CurrentGold
	{
		get { return currentGold; }
		set
		{
			currentGold = value;
			currentGoldText.text = $"Current Gold: $ {value}";
		}
	}


}
