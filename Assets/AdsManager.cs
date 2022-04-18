using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdsManager : MonoBehaviour
{

    public static AdsManager instance;

    public string interstitialAndroid;
    public string interstitialIOS;
    public string rewardedAndroid;
    public string rewardedIOS;

    InterstitialAd interstitial;
    RewardedAd rewardAd;
    BannerView banner;

    Action onSuccess;
    Action onClosedInterstitial;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        InitializeAds();
    }
    public void InitializeAds()
    {
        
        MobileAds.Initialize(initStatus => 
        {
            RequestInterstitial();
            RequestRewardedAd();

            Debug.Log("Google Ads Initialized");

        });
    }

    #region Banner Ads          
    public void RequestBanner(AdPosition adposition)
    {
        string bannerID;

#if UNITY_ANDROID
        bannerID = "";
#elif UNITY_IOS
                bannerID = adsConfig.BannerIdAdmobApple;
#endif

        banner = new BannerView(bannerID, AdSize.Banner, adposition);
        AdRequest request = new AdRequest.Builder().Build();
        banner.LoadAd(request);
    }

    #endregion

    #region Interstital Ads 
    public void RequestInterstitial()
    {
        string interstitalID;

#if UNITY_ANDROID
        interstitalID = interstitialAndroid;
#elif UNITY_IOS
                interstitalID = interstitialIOS;
#endif

        interstitial = new InterstitialAd(interstitalID);

        //call events
        interstitial.OnAdLoaded += HandleOnAdLoaded;
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        interstitial.OnAdOpening += HandleOnAdOpened;
        interstitial.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }

    //show the ad
    public void ShowInterstitial(Action onClosed)
    {
        onClosedInterstitial = onClosed;

        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }


    //events below
    void HandleOnAdLoaded(object sender, EventArgs args)
    {
        //do this when ad loads

    }

    void HandleOnAdFailedToLoad(object sender, EventArgs args)
    {
        //do this when ad fails to load
        GameManager.Instance.LoadMenu();
    }

    void HandleOnAdOpened(object sender, EventArgs args)
    {
        //do this when ad is opened
    }

    void HandleOnAdClosed(object sender, EventArgs args)
    {
        interstitial.Destroy();
        RequestInterstitial();
        onClosedInterstitial?.Invoke();
    }

    void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        //do this when on leaving application;
    }

    #endregion

    #region Rewarded Ads
    void RequestRewardedAd()
    {

        string rewardedID;

#if UNITY_ANDROID
        rewardedID = rewardedAndroid;
#elif UNITY_IOS
         interstitalID = rewardedIOS;
#endif

        rewardAd = new RewardedAd(rewardedID);



        //call events
        rewardAd.OnAdLoaded += HandleRewardAdLoaded;
        rewardAd.OnAdOpening += HandleRewardAdOpening;
        rewardAd.OnAdFailedToShow += HandleRewardAdFailedToShow;
        rewardAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardAd.OnAdClosed += HandleRewardAdClosed;



        AdRequest request = new AdRequest.Builder().Build();
        rewardAd.LoadAd(request); //load & show the banner ad
    }

    //attach to a button that plays ad if ready
    public void ShowRewardedAd(Action successCB)
    {
        if (rewardAd.IsLoaded())
        {
            rewardAd.Show();
            onSuccess = successCB;
        }
    }

    //call events
    public void HandleRewardAdLoaded(object sender, EventArgs args)
    {
        //do this when ad loads


    }

    void HandleRewardAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        //do this when ad fails to loads

    }

    void HandleRewardAdOpening(object sender, EventArgs args)
    {
        //do this when ad is opening
    }

    public void HandleRewardAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        //do this when ad fails to show

    }

    public void HandleUserEarnedReward(object sender, EventArgs args)
    {
        //reward the player here
        onSuccess?.Invoke();
    }

    void HandleRewardAdClosed(object sender, EventArgs args)
    {
        //do this when ad is closed
        RequestRewardedAd();
    }



    //Loaded
    //Showed (Reward)
    //Error
    #endregion
}
