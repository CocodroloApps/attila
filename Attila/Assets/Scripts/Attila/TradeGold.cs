using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeGold : MonoBehaviour
{
    public Text gold;

    public Text troops;
    public Text weapons;
    public Text water;
    public Text food;

    const int gold1 = 4000;
    const int gold2 = 2000;
    const int gold3 = 1000;
    const int gold4 = 1000;

    public void UpdateGold()
    {
        if (GlobalInfo.gold == 0)
        {
            gold.text = "-";
        }
        else
        {
            gold.text = GlobalInfo.gold.ToString("#,#");
        }
        UpdateTroops();
        UpdateWeapons();
        UpdateWater();
        UpdateFood();
    }

    private void UpdateTroops()
    {
        if (GlobalInfo.troops == 0)
        {
            troops.text = "0";
        }
        else
        {
            troops.text = GlobalInfo.troops.ToString("#,#");
        }
    }

    private void UpdateWeapons()
    {
        if (GlobalInfo.weapons == 0)
        {
            weapons.text = "0";
        }
        else
        {
            weapons.text = GlobalInfo.weapons.ToString("#,#");
        }
    }

    private void UpdateWater()
    {
        if (GlobalInfo.water == 0)
        {
            water.text = "0";
        }
        else
        {
            water.text = GlobalInfo.water.ToString("#,#");
        }
    }

    private void UpdateFood()
    {
        if (GlobalInfo.food == 0)
        {
            food.text = "0";
        }
        else
        {
            food.text = GlobalInfo.food.ToString("#,#");
        }
    }

    public void SaveSell()
    {
        GameObject.Find("GameManager").GetComponent<AudioAttila>().ClickEffect();
        GameObject.Find("GameManager").GetComponent<GameManager>().ShowInfo();
        PlayerInfo loadedData = DataSaver.loadData<PlayerInfo>(GlobalInfo.configFile, "txt");
        loadedData.gold = GlobalInfo.gold;
        loadedData.troops = GlobalInfo.troops;
        loadedData.weapons = GlobalInfo.weapons;
        loadedData.water = GlobalInfo.water;
        loadedData.food = GlobalInfo.food;
        DataSaver.saveData(loadedData, GlobalInfo.configFile, "txt");
    }

    public void SellGold1()
    {
        if (GlobalInfo.gold >= gold1)
        {
            GlobalInfo.troops = GlobalInfo.troops + 1000;
            GlobalInfo.gold = GlobalInfo.gold - gold1;
        }        
        SaveSell();
        UpdateGold();
    }

    public void SellGold2()
    {
        if (GlobalInfo.gold >= gold2)
        {
            GlobalInfo.weapons = GlobalInfo.weapons + 1000;
            GlobalInfo.gold = GlobalInfo.gold - gold2;
        }
        SaveSell();
        UpdateGold();
    }

    public void SellGold3()
    {
        if (GlobalInfo.gold >= gold3)
        {
            GlobalInfo.water = GlobalInfo.water + 1000;
            GlobalInfo.gold = GlobalInfo.gold - gold3;
        }        
        SaveSell();
        UpdateGold();
    }

    public void SellGold4()
    {
        if (GlobalInfo.gold >= gold4)
        {
            GlobalInfo.food = GlobalInfo.food + 1000;
            GlobalInfo.gold = GlobalInfo.gold - gold4;
        }
        SaveSell();
        UpdateGold();
    }
}
