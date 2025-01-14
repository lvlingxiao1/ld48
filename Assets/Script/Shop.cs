﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
	ShopItem[] items =
		{
		new ShopItem {
			name = "Searchlight",
			maxLevel = 1,
			prices = new int[] { 3000 },
			description = "You will need this to see in deep sea."
		},
		new ShopItem {
			name = "Electric Shock Gun",
			maxLevel = 1,
			prices = new int[] { 2000 },
			description = "Stun the fishes."
		},
		new ShopItem {
			name = "Flash Bomb",
			maxLevel = 1,
			prices = new int[] { 4000 },
			description = "Fishes in deep sea are afraid of light."
		},
		new ShopItem {
			name = "Oxygen Tank",
			maxLevel = 2,
			prices = new int[] { 5000, 10000 },
			description = "Increase Oxygen capacity by 50%."
		},
		new ShopItem {
			name = "Diving Suit",
			maxLevel = 2,
			prices = new int[] { 10000, 20000 },
			description = "Allow you to dive deeper.\nIncrease HP by 50%.\nIncrease swim speed by 20%."
		},
	};

	int numItems;
	Transform shopUI;
	Transform[] itemsUI;
	PlayerController player;
	public static int totalRevenue = 0;

	const int PRICE = 1;
	const int BUY = 2;

	const int SEARCHLIGHT = 0;
	const int SHOCK_GUN = 1;
	const int FLASH_BOMB = 2;
	const int O2_TANK = 3;
	const int DIVING_SUIT = 4;


	void Start()
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
			var mouseOverHandler = itemsUI[i].GetComponent<ShopItemMouseOverHandler>();
			mouseOverHandler.itemName = items[i].name;
			mouseOverHandler.description = items[i].description;
		}
		player = FindObjectOfType<PlayerController>();
		UpdatePlayerEquipments();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			Main.TotalGold += Main.CurrentGold;
			Main.CurrentGold = 0;
			player.HP = player.HPMax;
			UI.shopUICanvas.enabled = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			UI.shopUICanvas.enabled = false;
			player.UpdateEquipment();
		}
	}

	void Buy(int itemID)
	{
		if (items[itemID].currentLevel == items[itemID].maxLevel) return;
		if (itemID == O2_TANK && items[itemID].currentLevel == 1 && items[DIVING_SUIT].currentLevel < 1) return;

		int price = items[itemID].prices[items[itemID].currentLevel];
		if (price > Main.TotalGold) return;
		Main.TotalGold -= price;
		items[itemID].currentLevel++;
		UpdateSoldOut();
		UpdatePlayerEquipments();
		totalRevenue += price;
	}

	void UpdateSoldOut()
	{
		for (int i = 0; i < numItems; i++)
		{
			if (items[i].currentLevel == items[i].maxLevel)
			{
				itemsUI[i].GetChild(PRICE).GetComponentInChildren<TextMeshProUGUI>().text = "--";
				itemsUI[i].GetChild(BUY).GetComponentInChildren<TextMeshProUGUI>().text = "Sold Out";
			} else if (i == O2_TANK && items[i].currentLevel == 1 && items[DIVING_SUIT].currentLevel < 1)
			{
				itemsUI[i].GetChild(PRICE).GetComponentInChildren<TextMeshProUGUI>().text = "N/A";
				itemsUI[i].GetChild(BUY).GetComponentInChildren<TextMeshProUGUI>().text = "N/A";
			} else
			{
				itemsUI[i].GetChild(PRICE).GetComponentInChildren<TextMeshProUGUI>().text = $"$ {items[i].prices[items[i].currentLevel]}";
				itemsUI[i].GetChild(BUY).GetComponentInChildren<TextMeshProUGUI>().text = $"BUY";
			}
		}
	}

	void UpdatePlayerEquipments()
	{
		player.hasSearchlight = items[SEARCHLIGHT].currentLevel > 0;
		player.hasShockGun = items[SHOCK_GUN].currentLevel > 0;
		player.hasFlash = items[FLASH_BOMB].currentLevel > 0;
		player.o2TankLevel = items[O2_TANK].currentLevel;
		player.suitLevel = items[DIVING_SUIT].currentLevel;
		UI.flashBombIcon.SetActive(player.hasFlash);
		UI.shockGunIcon.SetActive(player.hasShockGun);
	}
}

[System.Serializable]
public class ShopItem
{
	public string name;
	public int currentLevel = 0;
	public int maxLevel;
	public int[] prices;
	public string description;
}