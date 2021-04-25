using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Main : MonoBehaviour
{
	private static int totalGold = 0;
	private static int currentGold = 0;
	private static TextMeshProUGUI totalGoldText;
	private static TextMeshProUGUI currentGoldText;
	private static RectTransform O2Bar;
	private static RectTransform HPBar;
	private static Image damage;
	private static PlayerController playerController;
	private static GameObject shock;
    private static GameObject flash;

	private void Awake()
	{
		totalGoldText = GameObject.Find("total_gold").GetComponentInChildren<TextMeshProUGUI>();
		currentGoldText = GameObject.Find("current_gold").GetComponentInChildren<TextMeshProUGUI>();
		O2Bar = GameObject.Find("O2Bar").GetComponent<RectTransform>();
		HPBar = GameObject.Find("HPBar").GetComponent<RectTransform>();
        shock = GameObject.Find("Canvas/Shock");
		TotalGold = 0;
		CurrentGold = 0;
		GameObject damageImage = GameObject.Find("Damage");
		//damageImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
		damage = damageImage.GetComponent<Image>();
		playerController = FindObjectOfType<PlayerController>();
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

	private void Update()
	{
		//if (player.position.y > 77)
		//{
		//    TotalGold = TotalGold + CurrentGold;
		//    CurrentGold = 0;
		//}
		float oxygenPercent = playerController.oxygen / playerController.oxygenMax;
		O2Bar.localScale = new Vector3(oxygenPercent, 1, 1);
		float HPPercent = playerController.HP / playerController.HPMax;
		HPBar.localScale = new Vector3(HPPercent, 1, 1);
		damage.color = new Color(1, 1, 1, (float)(playerController.invincibleCount) / playerController.invincibleTime * 0.5f);
        shock.SetActive(playerController.hasShockGun);
		int shockCD = playerController.getShockCD();
		if (shockCD == 0){
            shock.GetComponentInChildren<TextMeshProUGUI>().text = "";
		}else{
            shock.GetComponentInChildren<TextMeshProUGUI>().text = $"{shockCD}";
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
	}
}
