﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockButton : MonoBehaviour {

	public Button buttonComponent;
	public Text nameLabel;
	public Text priceText;
	public Text countText;
	public Text averageText;

	private ulong minprice=1200;
	private ulong maxprice=700000;
	private Item item;
	private ShopScrollList scrollList;


	// Use this for initialization
	void Start () 
	{
		//Screen.SetResolution(Screen.width, (Screen.width / 10) * 16 ,true);
		//Screen.SetResolution(Screen.width, (Screen.width / 2) * 3 ,true); 
		//Screen.SetResolution( 300, 480, true );
		buttonComponent.onClick.AddListener (HandleClick);
	}

	public void Setup(Item currentItem, ShopScrollList currentScrollList)
	{

		item = currentItem;
		 if (item.price > maxprice) {
			float stockRate = (float)Random.Range (-30, 0) / 100;
			item.price = (ulong)(item.price + item.price * stockRate);
			priceText.text = "<color=#0000ff>" + item.price.ToString () + "</color>";    //blue

		}else if (item.price < minprice) {
			float stockRate = (float)Random.Range (0, 30) / 100;
			item.price = (ulong)(item.price + item.price * stockRate);
			priceText.text = "<color=#ff0000>" + item.price.ToString () + "</color>";    //red

		} else if (item.price >= minprice) {

			float stockRate = (float)Random.Range (-28, 30) / 100;
			item.price = (ulong)(item.price + item.price * stockRate);
			if (stockRate > 0) {
				priceText.text = "<color=#ff0000>" + item.price.ToString () + "</color>";    //red
			} else if (stockRate == 0) {
				priceText.text = item.price.ToString ();
			} else if (stockRate < 0) {
				priceText.text = "<color=#0000ff>" + item.price.ToString () + "</color>";    //blue
			}

		} 

		nameLabel.text = item.stockName;

		//countText.text = item.count.ToString ();
		//averageText.text = item.Average.ToString ();
		scrollList = currentScrollList;

	}

	public void HandleClick()
	{
		scrollList.ViewInfo (item);
	}

}
