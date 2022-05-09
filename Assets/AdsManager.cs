using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : MonoBehaviour
{

    private RewardedAd rewardedAd;
    private HeartsUIManager heartsUIManager;

    public event Action OnRewardClaimed;

    // Start is called before the first frame update
    void Start()
    {

        heartsUIManager = GameObject.FindGameObjectWithTag("HeartsUIManager").GetComponent<HeartsUIManager>();
        rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        
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

    public void ShowAd()
    {
        LoadAd();

        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }

}
