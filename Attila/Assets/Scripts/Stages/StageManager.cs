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

    // Start is called before the first frame update
    void Start()
    {
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
            }
            if ( x > 1 && x <= GlobalInfo.maxStageCompleted + 1)
            {             
                GameObject line = GeneralUtils.FindObject(lineMain, "l" + (x - 1).ToString() + "B");
                line.SetActive(true);
            }
        }
    }

    public void ToGameScene()
    {
        GlobalInfo.actualStage = GlobalInfo.maxStageCompleted + 1;
        SceneManager.LoadScene("Attila");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
