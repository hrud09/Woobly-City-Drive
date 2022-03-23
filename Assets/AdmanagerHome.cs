using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdmanagerHome : MonoBehaviour
{
    private InterstitialAd interstitial;
    public string interstitialAdId;
    void Start()
    {
      
//#if UNITY_ANDROID
       
        interstitialAdId = "ca-app-pub-3940256099942544/1033173712";
        //#elif UNITY_IPHONE

        //           interstitialAdId = "ca-app-pub-3940256099942544/4411468910";
        //#endif


        this.interstitial = new InterstitialAd(interstitialAdId);
       
    }

    public void ShowInterstitialAd()
    {
        LoadInterstitialAd();
        this.interstitial.Show();   
    }
    public void LoadInterstitialAd()
    {

        this.interstitial.OnAdClosed += HandleOnAdClosed;
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
       
        //  FindObjectOfType<CarShopManager>().SelectCar();
    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
    
        FindObjectOfType<CarShopManager>().SelectCar();
       
    }
}
