using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternalAds : MonoBehaviour
{    
    public GameObject AdSpace;
    public Sprite Ad1;
    public Sprite Ad2;
    public float count = 2;
   
    private int adChoose;

    public void ShowAd()
    {
        adChoose = Mathf.RoundToInt(UnityEngine.Random.Range(1f, count));
        if (adChoose == 1)
        {
            AdSpace.GetComponent<Image>().sprite = Ad1;
        }
        if (adChoose == 2)
        {
            AdSpace.GetComponent<Image>().sprite = Ad2;
        }
    }

    public void TryMe()
    {
        if (adChoose == 1)
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.cocodroloapps.CrackEgg");
        }
        if (adChoose == 2)
        {
            Application.OpenURL("https://play.google.com/store/apps/details?id=com.cocodroloapps.Numbers");
        }
    }

    public void CloseAdBox()
    {
        GameObject.Find("MenuManager").GetComponent<MainMenu>().HideAdBox();
    }

}
