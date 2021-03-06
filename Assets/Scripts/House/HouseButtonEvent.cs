﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseButtonEvent : MonoBehaviour {
	//total size
	public const int SIZE=7;
	//Image bgimg;
	public Sprite bg;
	public int Selected_BG=99;
	//house, icon img, btn obj
	public GameObject[] houseObj = new GameObject[SIZE];
	public GameObject[] imgObj = new GameObject[SIZE];
	public GameObject[] btnObj = new GameObject[SIZE];
	public GameObject housepopup;

	public TransMoney myTransMoney;

	//각 배경에 해당하는 가격
	public ulong[] BG_Price = new ulong[SIZE] { 150000000, 250000000, 450000000, 1000000000, 2300000000,
		5200000000, 10600000000 };
	//배경 구매 완료한 경우 false
	public bool[] BG_BuyList = new bool[SIZE] {true, true, true, true, true, 
		true, true}; 

	public ulong money=0;

	bool signal=true;
	/*
	1. 집을 살 만큼의 돈 유무
		a.유 : BUY 버튼 활성화, 아이콘 컬러,  해당 항목(행) 배경색상 밝음  ok
			-- BUY 버튼 onClick,  
				>> BUY 글씨 색상 흐리게 변경, 아이콘 컬러!,  
				>> 아이템(인벤토리)의 집 탭에 구매한 집으로 변경,
		b.무 : 아이콘 흑백, BUY 글씨 색상 흐리게, BUY 버튼 비활성화

	*/
	// Use this for initialization
	void Awake(){
		//house = null;
		for (int i = 0; i < SIZE; i++) {
			//House오브젝트
			houseObj[i] = null;
			imgObj[i] = null;
			btnObj[i] = null;
		}
	}

	void Update () {
		Moneyupdate MU= GameObject.Find("MoneyManager").GetComponent<Moneyupdate>();
		money = MU.money;
		//if(GameObject.Find ("HousePopup").activeSelf == true)
		if (housepopup.activeSelf == true)
			HouseCheck (money);
		else {
			if (signal) {		
				//추후에 렉걸리면 방법을 바꿔야할듯...
				GameObject.Find ("Background").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("bg_img/bg"+Selected_BG) as Sprite;//("bg_img/bg_"+selected_BG) as Sprite;
				signal = false; //한번만 실행하게끔...
			}
		}
	}
	public void houseInitiate () {//houseButton onClick에 연결
		if (housepopup.activeSelf == true) {
			for (int i = 0; i < SIZE; i++) {
				//House,iconimg,btn오브젝트
				houseObj [i] = GameObject.Find ("House (" + i + ")").gameObject;
				imgObj [i] = houseObj [i].transform.FindChild ("Image").gameObject;
				btnObj [i] = houseObj [i].transform.FindChild ("HouseButton (" + i + ")").gameObject;
			}
		}
	}

	public void HouseCheck(ulong money){

		for (int i = 0; i < SIZE; i++) {
			//돈 있을 때,
			if (money > BG_Price [i]) {
				//이미 구매한 경우
				if (BG_BuyList [i] == false) {
					//text-->구매완료 
					houseObj[i].transform.GetChild(1).GetComponent<Text>().text = "구매 완료";
					//어둡게
					houseObj[i].GetComponent<Image> ().color = new Color (0.6f, 0.6f, 0.6f, 1);
					//컬러
					imgObj[i].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("bg_img/bg"+i) as Sprite;//("bg_icon/bg_icon_" + i) as Sprite;
					//버튼 비활성화
					btnObj[i].SetActive(false);
				}
				//돈도 있고, 안샀다면,
				else {
					houseObj [i].transform.GetChild (1).GetComponent<Text> ().text = myTransMoney.strTransMoney(BG_Price[i]);
					//밝게
					houseObj[i].GetComponent<Image> ().color = new Color (154 / 255, 154 / 255, 154 / 255, 154 / 255);
					//컬러아이콘
					imgObj[i].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("bg_img/bg"+i) as Sprite;//("bg_icon/bg_icon_" + i) as Sprite;
					imgObj[i].GetComponent<Image> ().color = new Color (1f,1f,1f,1f);
					//버튼 활성화
					btnObj[i].SetActive(true);

				}
			}
			//살 돈 없을 때,
			else {
				//살 돈 없고, 이미 구매한 경우
				if (BG_BuyList[i]==false) {
					//text-->구매완료 
					houseObj[i].transform.GetChild(1).GetComponent<Text>().text = "구매 완료";
					//어둡게
					houseObj[i].GetComponent<Image> ().color = new Color (0.6f, 0.6f, 0.6f, 1);
					//컬러
					imgObj[i].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("bg_img/bg"+i) as Sprite;//("bg_icon/bg_icon_" + i) as Sprite;
					//버튼 비활성화
					btnObj[i].SetActive(false);
				} 
				//아직 구매 안한 경우
				else {
					houseObj [i].transform.GetChild (1).GetComponent<Text> ().text = myTransMoney.strTransMoney(BG_Price[i]);
					//어둡게
					houseObj[i].GetComponent<Image> ().color = new Color (0.6f, 0.6f, 0.6f, 1);
					//흑백아이콘
					imgObj[i].GetComponent<Image> ().sprite = Resources.Load<Sprite> ("bg_img/bg"+i) as Sprite;//gray_bg"+i) as Sprite;//("bg_icon/sel_bg_icon_" + i) as Sprite;
					//이미지 어둡게
					imgObj [i].GetComponent<Image> ().color = new Color (0.6f, 0.6f, 0.6f, 1);
					//버튼 비활성화
					btnObj[i].SetActive(false);
				}	
			}
		}
	}

	//Button onClick event
	public void setBG (int btn){
		HouseMoneyEvent (btn);
		HouseChangeBG (btn);
		changeBGBuyEnable (btn);
		
		//자산 housenowPrice 업데이트
		AssetsEvent myAssetList= GameObject.Find("AssetsManager").GetComponent<AssetsEvent>();
		myAssetList.Asset_houseNowPrice[btn] = BG_Price[btn];
	}
	public void HouseMoneyEvent(int sel_BG){
		//MoneyManager에서 House의 가격에 따라 MoneyUpdate
		Moneyupdate MU= GameObject.Find("MoneyManager").GetComponent<Moneyupdate>();
		MU.money -= BG_Price[sel_BG];
		Debug.Log (money);
	}
	public void HouseChangeBG(int sel_BG){
		//리소스에서 배경파일 로드
		GameObject.Find ("Background").GetComponent<Image> ().sprite = Resources.Load<Sprite> ("bg_img/bg"+sel_BG) as Sprite;
		Debug.Log ("btn "+sel_BG+"_onClick");
		Selected_BG = sel_BG; //저장을 위해...
	}
	//구매한 경우 false로 표시 해줌
	public void changeBGBuyEnable(int sel_BG){
		BG_BuyList [sel_BG] = !BG_BuyList [sel_BG];
	}
}	
