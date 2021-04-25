using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
	public ShopItem[] items;

	int numItems;
	Transform shopUI;
	Transform[] itemsUI;
	Canvas shopUICanvas;

	const int PRICE = 1;
	const int BUY = 2;

	void Awake()
	{
		numItems = items.Length;
		itemsUI = new Transform[numItems];
		shopUI = GameObject.Find("ShopUI").transform;
		for (int i = 0; i < numItems; i++)
		{
			itemsUI[i] = shopUI.GetChild(i);
			itemsUI[i].GetChild(PRICE).GetComponentInChildren<TextMeshProUGUI>().text = $"$ {items[i].prices[0]}";
			int temp = i;
			itemsUI[i].GetChild(BUY).GetComponent<Button>().onClick.AddListener(() => Buy(temp));
		}
		shopUICanvas = shopUI.GetComponent<Canvas>();
		shopUICanvas.enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Main.TotalGold += Main.CurrentGold;
			Main.CurrentGold = 0;
			shopUICanvas.enabled = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			shopUICanvas.enabled = false;
		}
	}

	void Buy(int itemID)
	{
		if (items[itemID].currentLevel == items[itemID].maxLevel) return;
		items[itemID].currentLevel++;
		UpdateSoldOut();
	}

	void UpdateSoldOut()
	{
		for (int i = 0; i < numItems; i++)
		{
			if (items[i].currentLevel == items[i].maxLevel)
			{
				itemsUI[i].GetChild(PRICE).GetComponentInChildren<TextMeshProUGUI>().text = "--";
				itemsUI[i].GetChild(BUY).GetComponentInChildren<TextMeshProUGUI>().text = "Sold Out";
			} else
			{
				itemsUI[i].GetChild(PRICE).GetComponentInChildren<TextMeshProUGUI>().text = $"$ {items[i].prices[items[i].currentLevel]}";
			}
		}
	}
}

[System.Serializable]
public class ShopItem
{
	public string name;
	public int currentLevel;
	public int maxLevel;
	public int[] prices;
	public string description;
}