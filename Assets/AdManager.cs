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

    void Start()
    {
        MobileAds.Initialize(initStatus => { });
#if UNITY_ANDROID
        rewarderAdId = "ca-app-pub-3940256099942544/5224354917";
#elif UNITY_IPHONE
            rewarderAdId = "ca-app-pub-3940256099942544/1712485313";
             
#endif
        request = new AdRequest.Builder().Build();

    }

    AdRequest request;
    public void LoadRewardedAd()
    {

        GameManager.Instance.showingAds = true;
        this.rewardedAd = new RewardedAd(rewarderAdId);
        this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.

        // Load the rewarded ad with the request.
        this.rewardedAd.LoadAd(request);



    }
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        print(args.ToString());
       GameManager.Instance.gameOverPannel.SetActive(false);

    }
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        GameManager.Instance.RegeneratePlayer(350);

        GameManager.Instance.showingAds = false;
    }
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        print(args.ToString());
        GameManager.Instance.LoadMenu();
        GameManager.Instance.showingAds = false;
    }
}
