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

    // Start is called before the first frame update
    void Start()
    {
        GlobalInfo.sessionsCount++;
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
}
