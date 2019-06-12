using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Config : MonoBehaviour
{
    public GameObject Completed;
    public Image sound;
    public Sprite soundSprite;
    public Sprite noSoundSprite;

    public void Start()
    {
        if (GlobalInfo.soundPlay == true)
        {
            sound.sprite = soundSprite;
        } else
        {
            sound.sprite = noSoundSprite;
        }      
    }

    public void ChangeSound()
    {
        GameObject.Find("MenuManager").GetComponent<AudioMainMenu>().ClickEffect();
        if (GlobalInfo.soundPlay == true)
        {
            GlobalInfo.soundPlay = false;
            sound.sprite = noSoundSprite;
        }
        else
        {
            GlobalInfo.soundPlay = true;
            sound.sprite = soundSprite;
        }
    }

    public void Reboot()
    {
        GameObject.Find("MenuManager").GetComponent<AudioMainMenu>().ClickEffect();
        PlayerInfo loadedData = DataSaver.loadData<PlayerInfo>(GlobalInfo.configFile, "txt");
        IntialConditions cond = new IntialConditions();
        loadedData.maxStagesGame = cond.maxStagesGame;
        loadedData.actualStage = cond.actualStage;
        loadedData.maxStageCompleted = cond.maxStageCompleted;
        loadedData.score = cond.score;
        loadedData.water = cond.water;
        loadedData.food = cond.food;
        loadedData.troops = cond.troops;
        loadedData.weapons = cond.weapons;
        loadedData.gold = cond.gold;
        loadedData.showTutorial = true;        
        loadedData.sessionsCount = GlobalInfo.sessionsCount;
        DataSaver.saveData(loadedData, GlobalInfo.configFile, "txt");

        GlobalInfo.stagesCount = 0;
        GlobalInfo.maxStageCompleted = 0;
        GlobalInfo.showTutorial = true;

        //Initial conditions            
        GlobalInfo.maxStagesGame = cond.maxStagesGame;
        GlobalInfo.actualStage = cond.actualStage;
        GlobalInfo.score = cond.score;
        GlobalInfo.water = cond.water;
        GlobalInfo.food = cond.food;
        GlobalInfo.troops = cond.troops;
        GlobalInfo.weapons = cond.weapons;
        GlobalInfo.gold = cond.gold;

        GameObject.Find("MenuManager").GetComponent<MainMenu>().SetEnviroment();
        ShowCompleted();
    }

    public void HideCompleted()
    {
        Completed.SetActive(false);
    }

    public void ShowCompleted()
    {
        Completed.SetActive(true);
    }

    public void Legal()
    {
        GameObject.Find("MenuManager").GetComponent<AudioMainMenu>().ClickEffect();
        GameObject.Find("MenuManager").GetComponent<MainMenu>().HideConfigBox();
        GameObject.Find("MenuManager").GetComponent<MainMenu>().ShowLegalBox();
    }

}
