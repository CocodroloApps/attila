using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour
{
    public Text gems;

    const int weapons = 500;
    const int water = 150;
    const int food = 150;
    const int gold = 1000;

    public void UpdateGems()
    {        
        gems.text = GlobalInfo.score.ToString("#,#");
    }

    public void SaveSell()
    {
        GameObject.Find("MenuManager").GetComponent<AudioMainMenu>().ClickEffect();
        GameObject.Find("MenuManager").GetComponent<MainMenu>().SetEnviroment();
        PlayerInfo loadedData = DataSaver.loadData<PlayerInfo>(GlobalInfo.configFile, "txt");
        loadedData.score = GlobalInfo.score;
        loadedData.weapons = GlobalInfo.weapons;
        loadedData.water = GlobalInfo.water;
        loadedData.food = GlobalInfo.food;
        loadedData.gold = GlobalInfo.gold;
        DataSaver.saveData(loadedData, GlobalInfo.configFile, "txt");
    }

    public void SellWeapons()
    {
        if (GlobalInfo.score >= weapons)
        {
            GlobalInfo.weapons = GlobalInfo.weapons + 25000;
            GlobalInfo.score = GlobalInfo.score - weapons;
        }
        SaveSell();
        UpdateGems();
    }

    public void SellWater()
    {
        if (GlobalInfo.score >= water)
        {
            GlobalInfo.water = GlobalInfo.water + 25000;
            GlobalInfo.score = GlobalInfo.score - water;
        }
        SaveSell();
        UpdateGems();
    }

    public void SellFood()
    {
        if (GlobalInfo.score >= food)
        {
            GlobalInfo.food = GlobalInfo.food + 25000;
            GlobalInfo.score = GlobalInfo.score - food;
        }
        SaveSell();
        UpdateGems();
    }

    public void SellGold()
    {
        if (GlobalInfo.score >= gold)
        {
            GlobalInfo.gold = GlobalInfo.gold + 25000;
            GlobalInfo.score = GlobalInfo.score - gold;
        }
        SaveSell();
        UpdateGems();
    }

}
