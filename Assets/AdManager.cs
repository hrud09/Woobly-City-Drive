using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    // Start is called before the first frame update

    private RewardedAd rewardedAd;
    public string rewarderAdId;
    
    private InterstitialAd interstitial;
    public string interstitialAdId;
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
//#if UNITY_ANDROID
  //      rewarderAdId = "ca-app-pub-3940256099942544/5224354917";
//#elif UNITY_IPHONE
           rewarderAdId = "ca-app-pub-3940256099942544/1712485313";
           interstitialAdId = "ca-app-pub-3940256099942544/4411468910";
//#endif
        LoadRewardedAd();
        LoadInterstitialAd();
        
    }

    public void ShowRewardedAd()
    {
        if (this.rewardedAd.IsLoaded()) {
            this.rewardedAd.Show();
        }
    }

    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded()) {
            this.interstitial.Show();
        }
    }
    public void LoadInterstitialAd()
    {
        
        this.interstitial = new InterstitialAd(interstitialAdId);
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        this.interstitial.OnAdClosed += HandleOnAdClosed;
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        GameManager.Instance.LoadMenu();
    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        GameManager.Instance.LoadMenu();
    }
    public void LoadRewardedAd()
    {
        rewardedAd = new RewardedAd(rewarderAdId);
        AdRequest request = new AdRequest.Builder().Build();
       
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.

        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);



    }
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        print(args.ToString());
       GameManager.Instance.gameOverPannel.SetActive(false);

    }
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        GameManager.Instance.RegeneratePlayer(30);

      
    }
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
      
        GameManager.Instance.RegeneratePlayer(30);
 
    }
}
