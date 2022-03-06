using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdmanagerHome : MonoBehaviour
{
    private InterstitialAd interstitial;
    public string interstitialAdId;
    void Start()
    {
        interstitialAdId = "ca-app-pub-3940256099942544/4411468910";
        LoadInterstitialAd();
    }

    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }
    public void LoadInterstitialAd()
    {

        this.interstitial = new InterstitialAd(interstitialAdId);
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        FindObjectOfType<CarShopManager>().SelectCar();
    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        FindObjectOfType<CarShopManager>().SelectCar();
    }
}
