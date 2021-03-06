﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmploymentManager : MonoBehaviour
{

	public TransMoney myTransMoney; //connect MoneyManager

    public Button[] btn = new Button[11];

    public ulong[] price = new ulong[11];

    public ulong[] moneyspeed = new ulong[11];
    public ulong[] speedplus = new ulong[11];
    public GameObject[] Employer = new GameObject[11];

    public GameObject[] ClosePopup = new GameObject[10];
    public ulong[] EmployerLevel = new ulong[11];
    public ulong[] plusmoney = new ulong[11];
    public ulong[] Twoplusmoney = new ulong[11];
    public ulong[] NextLevelPrice = new ulong[11];
    public ulong[] TwoNextLevelPrice = new ulong[11];

    public ulong[] StartLevelPrice = new ulong[11];

    public ulong[] gobsem = new ulong[11];
    public ulong[] characterbalance = new ulong[11] { 200, 250, 300, 400, 500, 600, 700, 800, 900, 1000, 2000 };
    public Image[] BackgroundColor = new Image[11];


    public Color close;
    public Color open;
    public ulong firstLevelprice = 10000;
    public Text[] btn_text = new Text[11];
    public Text[] name_text = new Text[11];
    public string[] names_text = new string[11] { "몽실이", "멸치", "얍삽이", "덩치","명사수","이한위","현기증","내부인","우리형","대장님","천수르"};
    public bool[] btnClick = new bool[11];
    public bool chunsooruExist=false;

    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < 11; i++)
        {

            if (btn[i] == null)
            {
                btn[i] = gameObject.GetComponent<UnityEngine.UI.Button>();
            }
            name_text[i].text = "" + EmployerLevel[i];
            if (EmployerLevel[i] == 0)
            {
                if (i == 0)
                {
                    StartLevelPrice[0] = 10000;
                    NextLevelPrice[i] = StartLevelPrice[i];
                    TwoNextLevelPrice[i] = NextLevelPrice[i];
                    gobsem[i] = 1;
                    speedplus[i] = 2;
                    moneyspeed[i] = 2;
                }
                if (i % 2 == 1)
                {
                    StartLevelPrice[i] = StartLevelPrice[i - 1] * 5;
                    NextLevelPrice[i] = StartLevelPrice[i];
                    TwoNextLevelPrice[i] = NextLevelPrice[i];
                    gobsem[i] = gobsem[i - 1] * 5;
                    speedplus[i] = speedplus[i - 1] * 5;
                    moneyspeed[i] = speedplus[i];
                    if (i == 3)
                    {
                        speedplus[i] = 30;
                        moneyspeed[i] = 30;
                    }
                    if(i==9)
                    {
                        speedplus[i] = 1000000;
                        moneyspeed[i] = 1000000;
                    }
                }
                if (i % 2 == 0 && i != 0)
                {
                    StartLevelPrice[i] = StartLevelPrice[i - 1] * 2;
                    NextLevelPrice[i] = StartLevelPrice[i];
                    TwoNextLevelPrice[i] = NextLevelPrice[i];
                    gobsem[i] = gobsem[i - 1] * 2;
                    speedplus[i] = speedplus[i - 1] * 2;
                    moneyspeed[i] = speedplus[i];
                    if (i == 4)
                    {
                        speedplus[i] = 100;
                        moneyspeed[i] = 100;
                    }
                    if(i==10)
                    {
                        speedplus[i] = 5000000;
                        moneyspeed[i] = 5000000;
                    }
                }


            }
            btn_text[i].fontSize = 8;
			btn_text[i].text = "비용:" + myTransMoney.strTransMoney(TwoNextLevelPrice[i]) + "\n" + "초당:" + myTransMoney.strTransMoney(moneyspeed[i]);
            name_text[i].text = names_text[i]+"LV " + EmployerLevel[i];
            btnClick[i] = false;
            if(EmployerLevel[i]>=10 && i!=10)
            {
                ClosePopup[i].active = false;
            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        Moneyupdate moneyu = GameObject.Find("MoneyManager").GetComponent<Moneyupdate>();

        for (int i = 0; i < 11; i++)
        {
            if (moneyu.money > TwoNextLevelPrice[i])
            {
                btn[i].interactable = true;
                //btn[i].enabled = true;
                BackgroundColor[i].color = open;
            }
            else if (moneyu.money <= TwoNextLevelPrice[i])
            {
                btn[i].interactable = false;
                //btn[i].enabled = false;
                BackgroundColor[i].color = close;
            }
        }
        //BtnClickedEvent();


    }
    public void BtnClickedEvent()
    {
        Moneyupdate moneyu = GameObject.Find("MoneyManager").GetComponent<Moneyupdate>();
        for (int i = 0; i < 11; i++)
        {

            if (btnClick[i] == true)
            {

                plusmoney[i] = plusmoney[i] + EmployerLevel[i];

                NextLevelPrice[i] = (10000 + (characterbalance[i] * (plusmoney[i]))) * gobsem[i];
                TwoNextLevelPrice[i] = (10000 + (characterbalance[i] * (plusmoney[i]+(EmployerLevel[i]+1)))) * gobsem[i];
                if (moneyu.money >= NextLevelPrice[i])
                {
                    EmployerLevel[i]++;
                    moneyu.money = moneyu.money - NextLevelPrice[i];
                    moneyspeed[i] += speedplus[i];
                    moneyu.moneyspeed += moneyspeed[i];

                    btn_text[i].fontSize = 8;
					btn_text[i].text = "비용:" + myTransMoney.strTransMoney(TwoNextLevelPrice[i]) + "\n" + "초당:" + myTransMoney.strTransMoney(moneyspeed[i]);
                    name_text[i].text = names_text[i]+"LV " + EmployerLevel[i];
                    Employer[i].active = true;
                }
                
                else if (moneyu.money < NextLevelPrice[i])
                {
                    plusmoney[i] = plusmoney[i]- EmployerLevel[i];
                    NextLevelPrice[i] = (10000 + characterbalance[i] * (plusmoney[i])) * gobsem[i];
                    TwoNextLevelPrice[i] = (10000 + (characterbalance[i] * (plusmoney[i] + (EmployerLevel[i] + 1)))) * gobsem[i];
                }
                

                if (EmployerLevel[i] >= 10 && i != 10)
                {
                    ClosePopup[i].active = false;
                }
                btnClick[i] = false;
            }
        }
    }
    public void dogbtnClick()
    {
        btnClick[0] = true;
        BtnClickedEvent();
    }

    public void myulchibtnClick()
    {
        btnClick[1] = true;
        BtnClickedEvent();
    }

    public void yapsapbtnClick()
    {
        btnClick[2] = true;
        BtnClickedEvent();
    }

    public void dungchibtnClick()
    {
        btnClick[3] = true;
        BtnClickedEvent();
    }

    public void myungsasubtnClick()
    {
        btnClick[4] = true;
        BtnClickedEvent();
    }

    public void skatebtnClick()
    {
        btnClick[5] = true;
        BtnClickedEvent();
    }
    public void godhbtnClick()
    {
        btnClick[6] = true;
        BtnClickedEvent();
    }
    public void ramyunClick()
    {
        btnClick[7] = true;
        BtnClickedEvent();
    }
    public void hohyungClick()
    {
        btnClick[8] = true;
        BtnClickedEvent();
    }
    public void daejangClick()
    {
        btnClick[9] = true;
        BtnClickedEvent();
    }
    public void chunsooruClick()
    {
        btnClick[10] = true;
        BtnClickedEvent();
        chunsooruExist = true;
    }

}