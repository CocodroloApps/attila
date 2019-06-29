using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public Image background;
    public Sprite background1;
    public Sprite background2;
    public Sprite background3;

    public GameObject tutorialBox;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("StageManager").GetComponent<AudioStagesMenu>().SceneEffect();
        int stageGroup = 0;
        GameObject lineMain = GameObject.Find("Lines");

        if (GlobalInfo.maxStageCompleted +1 < 11)
        {
            background.GetComponent<Image>().sprite = background1;
            stageGroup = 0;
        }
        if (GlobalInfo.maxStageCompleted +1 >= 11 && GlobalInfo.maxStageCompleted <21)
        {
            background.GetComponent<Image>().sprite = background2;
            stageGroup = 1;
        }
        if (GlobalInfo.maxStageCompleted +1 >= 21)
        {
            background.GetComponent<Image>().sprite = background3;
            stageGroup = 2;
        }

        for (int x = 1; x <= 10; x++)
        {
            GameObject level = GameObject.Find("Stage"+ x.ToString());
            GameObject levelText = GeneralUtils.FindObject(level, "s"+ x.ToString());
            if (levelText!= null)
            {
                levelText.GetComponent<Text>().text = (stageGroup * 10 + x).ToString();
                if (stageGroup * 10 + x == GlobalInfo.maxStageCompleted + 1)
                {
                    GameObject hun = GeneralUtils.FindObject(level, "Hun" + x.ToString());
                    hun.SetActive(true);
                }
                if (stageGroup * 10 + x < GlobalInfo.maxStageCompleted + 1)
                {
                    GameObject done = GeneralUtils.FindObject(level, "Done");
                    done.SetActive(true);
                }
            }
            if ( x > 1 && x <= GlobalInfo.maxStageCompleted + 1)
            {             
                GameObject line = GeneralUtils.FindObject(lineMain, "l" + (x - 1).ToString() + "B");
                line.SetActive(true);
            }
        }

        if (GlobalInfo.showTutorial1 == true)
        {
            ShowTutorialBox();
        }
    }

    public void ToGameScene()
    {
        GameObject.Find("StageManager").GetComponent<AudioStagesMenu>().ClickEffect();
        GlobalInfo.actualStage = GlobalInfo.maxStageCompleted + 1;
        if (GlobalInfo.showTutorial == true)
        {
            if (GlobalInfo.actualStage == 1)
            {
                GlobalInfo.showTutorial2 = true;
                GlobalInfo.showTutorial3 = true;
                GlobalInfo.showTutorial4 = true;
                GlobalInfo.showTutorial5 = true;

                GlobalInfo.showTutorial6 = true;
                GlobalInfo.showTutorial7 = true;
                GlobalInfo.showTutorial8 = true;
            }
            if (GlobalInfo.actualStage == 2)
            {
                GlobalInfo.showTutorial2 = false;
                GlobalInfo.showTutorial3 = false;
                GlobalInfo.showTutorial4 = false;
                GlobalInfo.showTutorial5 = false;
                
                GlobalInfo.showTutorial6 = true;
                GlobalInfo.showTutorial7 = true;
                GlobalInfo.showTutorial8 = true;
            }
            if (GlobalInfo.actualStage == 3)
            {
                GlobalInfo.showTutorial2 = false;
                GlobalInfo.showTutorial3 = false;
                GlobalInfo.showTutorial4 = false;
                GlobalInfo.showTutorial5 = false;
                GlobalInfo.showTutorial6 = false;

                GlobalInfo.showTutorial7 = true;
                GlobalInfo.showTutorial8 = true;
            }
        } else
        {
            GlobalInfo.showTutorial2 = false;
            GlobalInfo.showTutorial3 = false;
            GlobalInfo.showTutorial4 = false;
            GlobalInfo.showTutorial5 = false;
            GlobalInfo.showTutorial6 = false;
            GlobalInfo.showTutorial7 = false;
            GlobalInfo.showTutorial8 = false;
        }
        StartCoroutine(ToGame());
    }

    IEnumerator ToGame()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene("Attila");
    }

    public void ToMainMenu()
    {
        GameObject.Find("StageManager").GetComponent<AudioStagesMenu>().ClickEffect();
        SceneManager.LoadScene("MainMenu");
    }

    public void HideTutorialBox()
    {
        GameObject.Find("StageManager").GetComponent<AudioStagesMenu>().ClickEffect();
        tutorialBox.SetActive(false);
        GlobalInfo.showTutorial1 = false;
    }

    public void ShowTutorialBox()
    {        
        tutorialBox.SetActive(true);
    }
}
