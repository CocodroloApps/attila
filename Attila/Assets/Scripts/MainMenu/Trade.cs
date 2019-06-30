using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EasyMobile;

public class Trade : MonoBehaviour
{
    public Text gems;

    const int gold1 = 500;
    const int gold2 = 700;
    const int gold3 = 1000;

    private int origen = 0;

    private void Start()
    {
        Advertising.LoadRewardedAd();
    }

    void OnEnable()
    {
        Advertising.RewardedAdCompleted += RewardedAdCompletedHandler;
        Advertising.RewardedAdSkipped += RewardedAdSkippedHandler;
    }

    // Unsubscribe events
    void OnDisable()
    {
        Advertising.RewardedAdCompleted -= RewardedAdCompletedHandler;
        Advertising.RewardedAdSkipped -= RewardedAdSkippedHandler;
    }

    void RewardedAdCompletedHandler(RewardedAdNetwork network, AdPlacement location)
    {
        Debug.Log("Rewarded ad has completed. The user should be rewarded now.");

        if (origen == 1)
        {
            VideoReward();
            SaveSell();
        }
        if (origen == 2)
        {
            VideoRewardSpy();
            SaveSell();
        }
    }

    // Event handler called when a rewarded ad has been skipped
    void RewardedAdSkippedHandler(RewardedAdNetwork network, AdPlacement location)
    {
        Debug.Log("Rewarded ad was skipped. The user should NOT be rewarded.");
    }

    public void UpdateGems()
    {
        if (GlobalInfo.score == 0)
        {
            gems.text = "-";
        }
        else
        {
            gems.text = GlobalInfo.score.ToString("#,#");
        }       
    }

    public void SaveSell()
    {
        GameObject.Find("MenuManager").GetComponent<AudioMainMenu>().ClickEffect();
        GameObject.Find("MenuManager").GetComponent<MainMenu>().SetEnviroment();
        PlayerInfo loadedData = DataSaver.loadData<PlayerInfo>(GlobalInfo.configFile, "txt");
        loadedData.score = GlobalInfo.score;
        loadedData.gold = GlobalInfo.gold;
        loadedData.spyMoves = GlobalInfo.spyMoves;
        DataSaver.saveData(loadedData, GlobalInfo.configFile, "txt");
    }

    public void SellGems1()
    {
        if (GlobalInfo.score >= gold1)
        {
            GlobalInfo.gold = GlobalInfo.gold + 50000;
            GlobalInfo.score = GlobalInfo.score - gold1;
        }
        SaveSell();
        UpdateGems();
    }

    public void SellGems2()
    {
        if (GlobalInfo.score >= gold2)
        {
            GlobalInfo.gold = GlobalInfo.gold + 75000;
            GlobalInfo.score = GlobalInfo.score - gold2;
        }
        SaveSell();
        UpdateGems();
    }

    public void SellGems3()
    {
        if (GlobalInfo.score >= gold3)
        {
            GlobalInfo.gold = GlobalInfo.gold + 125000;
            GlobalInfo.score = GlobalInfo.score - gold3;
        }
        SaveSell();
        UpdateGems();
    }

    private void VideoReward()
    {
        GlobalInfo.gold = GlobalInfo.gold + 25000;                
    }

    private void VideoRewardSpy()
    {
        GlobalInfo.spyMoves = GlobalInfo.spyMoves + 5;
    }

    public void RewardVideo()
    {
        bool isReady = Advertising.IsRewardedAdReady();
        origen = 1;
        if (isReady)
        {
            Advertising.ShowRewardedAd();
        }
        Advertising.LoadRewardedAd();      
    }

    public void RewardVideoSpy()
    {
        bool isReady = Advertising.IsRewardedAdReady();
        origen = 2;
        if (isReady)
        {
            Advertising.ShowRewardedAd();
        }
        Advertising.LoadRewardedAd();
    }
}
