using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenu : MonoBehaviour
{
    public Text troops;
    public Text weapons;
    public Text water;
    public Text food;
    public Text gold;

    public GameObject langBox;
    public GameObject configBox;
    public GameObject legalBox;

    // Start is called before the first frame update
    void Start()
    {
        GlobalInfo.sessionsCount++;
        if (GlobalInfo.gameFirstTime == true)
        {
            ShowLegalBox();
            GlobalInfo.gameFirstTime = false;
        }
        StartCoroutine(SaveSessionsConfig());
        SetEnviroment();
    }

    IEnumerator SaveSessionsConfig()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerInfo loadedData = DataSaver.loadData<PlayerInfo>(GlobalInfo.configFile, "txt");        
        loadedData.sessionsCount = GlobalInfo.sessionsCount;
        DataSaver.saveData(loadedData, GlobalInfo.configFile, "txt");        
    }

    public void SetEnviroment()
    {
        troops.text = GlobalInfo.troops.ToString("#,#");
        weapons.text = GlobalInfo.weapons.ToString("#,#");
        water.text = GlobalInfo.water.ToString("#,#");
        food.text = GlobalInfo.food.ToString("#,#");
        gold.text = GlobalInfo.gold.ToString("#,#");        
    }

    public void PlayGame()
    {
        if (GlobalInfo.playFirstTime == true)
        {
            // Save PLAYDATEFIRSTTIME info to CONFIG
            PlayerInfo loadedData = DataSaver.loadData<PlayerInfo>(GlobalInfo.configFile, "txt");
            loadedData.playDateFirstTime = DateTime.Now.ToBinary().ToString();
            DataSaver.saveData(loadedData, GlobalInfo.configFile, "txt");
            GlobalInfo.playFirstTime = false;
        }
        if (GlobalInfo.maxStageCompleted < GlobalInfo.maxStagesGame)
        {
            SceneManager.LoadScene("StageSelector");
        } else
        {
            SceneManager.LoadScene("Winner");
        }        
    }

    public void ShowLanguageBox()
    {
        langBox.SetActive(true);
    }

    public void HideLanguageBox()
    {
        langBox.SetActive(false);
    }

    public void ShowConfigBox()
    {
        GameObject.Find("MenuManager").GetComponent<Config>().HideCompleted();
        configBox.SetActive(true);
    }

    public void HideConfigBox()
    {
        configBox.SetActive(false);
    }

    public void ShowLegalBox()
    {
        legalBox.SetActive(true);
    }

    public void OpenPolicyWeb()
    {
        Application.OpenURL("https://www.jugandohaciendojuegos.com/p/attila-privacy-policy.html");
    }

    public void HideLegalBox()
    {
        legalBox.SetActive(false);
    }
}
