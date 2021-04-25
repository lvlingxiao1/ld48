using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopItemMouseOverHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public string itemName;
	public string description;

	public void OnPointerEnter(PointerEventData eventData)
	{
		UI.itemName.text = itemName;
		UI.itemDescription.text = description;
		UI.shopTooltip.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		UI.shopTooltip.SetActive(false);

	}
}
