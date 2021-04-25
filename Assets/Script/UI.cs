using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UI : MonoBehaviour
{
	public static TextMeshProUGUI totalGoldText;
	public static TextMeshProUGUI currentGoldText;
	public static RectTransform O2Bar;
	public static RectTransform HPBar;
	public static Image damage;
	public static TextMeshProUGUI shockCDText;
	public static TextMeshProUGUI flashCDText;
	public static PlayerController playerController;
	public static GameObject flashBombIcon;
	public static GameObject shockGunIcon;
	public static Canvas shopUICanvas;
	public static Animator UIAnimator;

	private void Awake()
	{
		totalGoldText = GameObject.Find("total_gold").GetComponentInChildren<TextMeshProUGUI>();
		currentGoldText = GameObject.Find("current_gold").GetComponentInChildren<TextMeshProUGUI>();
		O2Bar = GameObject.Find("O2Bar").GetComponent<RectTransform>();
		HPBar = GameObject.Find("HPBar").GetComponent<RectTransform>();
		shockCDText = GameObject.Find("Canvas/Shock").GetComponentInChildren<TextMeshProUGUI>();
		flashCDText = GameObject.Find("Canvas/Flash").GetComponentInChildren<TextMeshProUGUI>();
		damage = GameObject.Find("Damage").GetComponent<Image>();
		flashBombIcon = GameObject.Find("Canvas/Flash");
		shockGunIcon = GameObject.Find("Canvas/Shock");
		playerController = FindObjectOfType<PlayerController>();
		shopUICanvas = GameObject.Find("ShopUI").GetComponent<Canvas>();
        UIAnimator = GameObject.Find("Canvas").GetComponent<Animator>();

		shopUICanvas.enabled = false;
	}

	private void Update()
	{
		float oxygenPercent = playerController.oxygen / playerController.oxygenMax;
		O2Bar.localScale = new Vector3(oxygenPercent, 1, 1);
		float HPPercent = playerController.HP / playerController.HPMax;
		HPBar.localScale = new Vector3(HPPercent, 1, 1);
		damage.color = new Color(1, 1, 1, (float)(playerController.invincibleCounter) / playerController.invincibleTime * 0.5f);
		int shockCD = playerController.GetShockCD();
		if (shockCD == 0)
		{
			shockCDText.text = "";
		} else
		{
			shockCDText.text = $"{shockCD}";
		}
		int flashCD = playerController.GetFlashCD();
		if (flashCD == 0)
		{
			flashCDText.text = "";
		} else
		{
			flashCDText.text = $"{flashCD}";
		}
	}
}
