﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockButton : MonoBehaviour {

	public Button buttonComponent;
	public Text nameLabel;
	public Text priceText;


	private Item item;
	private ShopScrollList scrollList;


	// Use this for initialization
	void Start () 
	{
		buttonComponent.onClick.AddListener (HandleClick);
	}

	public void Setup(Item currentItem, ShopScrollList currentScrollList)
	{
		item = currentItem;
		nameLabel.text = item.stockName;
		priceText.text = item.price.ToString ();
		scrollList = currentScrollList;

	}

	public void HandleClick()
	{
		scrollList.TryTransferItemToOtherShop (item);
	}

}
