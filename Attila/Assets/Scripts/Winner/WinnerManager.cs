using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinnerManager : MonoBehaviour
{
    public Text troops;
    public Text weapons;
    public Text water;
    public Text food;
    public Text gold;    
    public Text score;


    // Start is called before the first frame update
    void Start()
    {
        ShowInfo();
    }

    public void ShowInfo()
    {
        troops.text = GlobalInfo.troops.ToString("#,#");
        weapons.text = GlobalInfo.weapons.ToString("#,#");
        water.text = GlobalInfo.water.ToString("#,#");
        food.text = GlobalInfo.food.ToString("#,#");
        gold.text = GlobalInfo.gold.ToString("#,#");
        score.text = GlobalInfo.score.ToString("#,#");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
