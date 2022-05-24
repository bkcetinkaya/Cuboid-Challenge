using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour
{

    private RewardedAd rewardedAd;
    private HeartsUIManager heartsUIManager;
  

    public event Action OnRewardClaimed;

    // Start is called before the first frame update
    void Start()
    {

        MobileAds.Initialize(initStatus => { });

        heartsUIManager = GameObject.FindGameObjectWithTag("HeartsUIManager").GetComponent<HeartsUIManager>();

        CreateAndLoadNewAd();

       

    }

    private void CreateAndLoadNewAd()
    {
        rewardedAd = new RewardedAd("ca-app-pub-7363838760234979/8824146077");
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;

        LoadAd();
    }

    private void LoadAd()
    {
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Ad Loaded");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;

       
        PlayerHealthController.Instance.SetPlayerHealth((int)amount);
        heartsUIManager.UpdateUI();
        OnRewardClaimed?.Invoke();
    }
    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        CreateAndLoadNewAd();
    }

    public void ShowAd()
    {   

        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        else
        {
            LoadAd();
            rewardedAd.Show();
        }
    }

}
