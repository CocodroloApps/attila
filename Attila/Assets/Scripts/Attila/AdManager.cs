using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyMobile;

public class AdManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadAdInterticial();
    }

    public void StopAdBanner()
    {
        Advertising.HideBannerAd();
    }

    public void LoadAdInterticial()
    {
        Advertising.LoadInterstitialAd();
    }
    
    public void ShowAdInterticial()
    {
        //Ad Insterticial
        Advertising.LoadInterstitialAd();
        bool isReady = Advertising.IsInterstitialAdReady();
        if (isReady)
        {
            StartCoroutine(Interstitial());
        }
        Advertising.LoadInterstitialAd();
    }

    IEnumerator Interstitial()
    {
        yield return new WaitForSeconds(1.5f);
        Advertising.ShowInterstitialAd();
    }

    public void ShowBanner()
    {
        //Load Ad Banner;
        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
    }
}
