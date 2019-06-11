using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        GameObject.Find("MenuManager").GetComponent<AudioMainMenu>().SceneEffect();
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
        GameObject.Find("MenuManager").GetComponent<AudioMainMenu>().ClickEffect();
        if (GlobalInfo.maxStageCompleted < GlobalInfo.maxStagesGame)
        {
            if (GlobalInfo.showTutorial == true)
            {
                GlobalInfo.showTutorial1 = true;
            } else
            {
                GlobalInfo.showTutorial1 = false;
            }            
            GameObject.Find("MenuManager").GetComponent<UIAnimMenu>().HideAllGUIs();
            StartCoroutine(ToLevel());
        } else
        {
            GameObject.Find("MenuManager").GetComponent<UIAnimMenu>().HideAllGUIs();
            StartCoroutine(ToWinner());
        }        
    }

    IEnumerator ToLevel()
    {
        GameObject.Find("MenuManager").GetComponent<AudioMainMenu>().ClickEffect();
        yield return new WaitUntil(() => GlobalInfo.isShowingInfo == false);
        SceneManager.LoadScene("StageSelector");
    }

    IEnumerator ToWinner()
    {
        GameObject.Find("MenuManager").GetComponent<AudioMainMenu>().ClickEffect();
        yield return new WaitUntil(() => GlobalInfo.isShowingInfo == false);
        SceneManager.LoadScene("Winner");
    }

    public void ShowLanguageBox()
    {
        GameObject.Find("MenuManager").GetComponent<AudioMainMenu>().ClickEffect();
        
        GameObject.Find("MenuManager").GetComponent<AdManager>().ShowAdInterticial();
        
        langBox.SetActive(true);
    }

    public void HideLanguageBox()
    {
        GameObject.Find("MenuManager").GetComponent<AudioMainMenu>().ClickEffect();
        langBox.SetActive(false);
    }

    public void ShowConfigBox()
    {
        GameObject.Find("MenuManager").GetComponent<AudioMainMenu>().ClickEffect();
        GameObject.Find("MenuManager").GetComponent<Config>().HideCompleted();
        GameObject.Find("MenuManager").GetComponent<AdManager>().ShowAdInterticial();
        configBox.SetActive(true);
    }

    public void HideConfigBox()
    {
        GameObject.Find("MenuManager").GetComponent<AudioMainMenu>().ClickEffect();
        configBox.SetActive(false);
    }

    public void ShowLegalBox()
    {
        GameObject.Find("MenuManager").GetComponent<AudioMainMenu>().ClickEffect();
        legalBox.SetActive(true);
    }

    public void OpenPolicyWeb()
    {
        GameObject.Find("MenuManager").GetComponent<AudioMainMenu>().ClickEffect();
        Application.OpenURL("https://www.jugandohaciendojuegos.com/p/attila-privacy-policy.html");
    }

    public void HideLegalBox()
    {
        GameObject.Find("MenuManager").GetComponent<AudioMainMenu>().ClickEffect();
        legalBox.SetActive(false);
    }
}
