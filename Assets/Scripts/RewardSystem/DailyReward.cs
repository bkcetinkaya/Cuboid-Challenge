using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DailyReward : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI timeLeftText;

    [SerializeField]
    private Button claimRewardButton;

    [SerializeField]
    private Button DailyRewardIconButton;

    private Transform RewardUI;

    private HeartsUIManager heartsUIManager;
    
    [Space]

    [SerializeField]
    [Tooltip("in 24h format for example: 12 or 24 or 09 etc..")]
    private int rewardHour;

    [SerializeField]
    private int rewardMinute;

    TimeSpan _result;
    double currCountdownValue ;

    private void Start()
    {

        CheckIfRewardWasAvaliableWhenGameWasClosed();


        RewardUI = GameObject.FindGameObjectWithTag("RewardUI").GetComponent<Transform>();
        heartsUIManager = GameObject.FindGameObjectWithTag("HeartsUIManager").GetComponent<HeartsUIManager>();
        SetRewardUI(false);

        if (!PlayerPrefs.HasKey("RewardClaimable"))
        {
            PlayerPrefs.SetInt("RewardClaimable", 1);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.GetInt("RewardClaimable") == 0)
        {
            SetDailyRewardIconColor(Color.gray);
            SetClaimRewardButton(false);
            SetCountDownText(true);
            SetTimeDifference();
            StartCoroutine(StartCountdown());
        }
        if (PlayerPrefs.GetInt("RewardClaimable") == 1)
        {
            SetDailyRewardIconColor(Color.white);
            claimRewardButton.interactable = true;
            SetCountDownText(false);
            StopCoroutine(StartCountdown());
        }    

    }

    private void CheckIfRewardWasAvaliableWhenGameWasClosed()
    {
        TimeSpan diff = CalculateTimeDifference();

        int secs = (int)diff.TotalSeconds;

        int _lastTimeSecs = PlayerPrefs.GetInt("LastTime");

        
        if(secs > _lastTimeSecs)
        {
            SetRewardClaimable();
        }
    }

    private void SetDailyRewardIconColor(Color color)
    {
        var colors = DailyRewardIconButton.colors;
        colors.normalColor = color;
        DailyRewardIconButton.colors = colors;

    }

    private void SetTimeDifference()
    {
        _result = CalculateTimeDifference();
        currCountdownValue = _result.TotalSeconds;
    }

    private TimeSpan CalculateTimeDifference()
    {
        DateTime currentTime = DateTime.Now;
        DateTime rewardTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, rewardHour,rewardMinute, 00);

        TimeSpan offsetTime = TimeSpan.FromHours(24);
        TimeSpan diff = currentTime.Subtract(rewardTime);
       
        //if time difference is negative
        if(diff < TimeSpan.Zero)
        {
           

            return diff.Duration();
        }

        TimeSpan result = offsetTime.Subtract(diff);
 
        return result;

    }

    public IEnumerator StartCountdown()
    {
        
        while (currCountdownValue > 0.9f)
        {
           
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;


            timeLeftText.text = TimeSpan.FromSeconds(currCountdownValue).ToString(@"hh\:mm\:ss");
        }

        SetClaimRewardButton(true);
        SetCountDownText(false);
        SetRewardClaimable();
      
    }

   

    private void SetClaimRewardButton(bool value)
    {
        
        claimRewardButton.interactable= value;
       
    }

    private void SetCountDownText(bool value)
    {
        timeLeftText.gameObject.SetActive(value);
    }
    private void SetRewardClaimable()
    {
        PlayerPrefs.SetInt("RewardClaimable", 1);
        PlayerPrefs.Save();

        SetDailyRewardIconColor(Color.white);
    }

    private void SetRewardUnClaimable()
    {
        PlayerPrefs.SetInt("RewardClaimable", 0);
        PlayerPrefs.Save();
    }
    public void ClaimReward()
    {
        PlayerHealthController.Instance.SetPlayerHealth(3);
        heartsUIManager.UpdateUI();

        ResetTimer();
    }

    private void ResetTimer()
    {
        SetRewardUnClaimable();
        SetClaimRewardButton(false);
        SetDailyRewardIconColor(Color.gray);
        SetCountDownText(true);
        SetTimeDifference();
        StartCoroutine(StartCountdown());
    }

    public void SetRewardUI(bool value)
    {
        RewardUI.gameObject.SetActive(value);
    }

    private void OnApplicationQuit()
    {
        TimeSpan currDiff = CalculateTimeDifference();

        int seconds = (int)currDiff.TotalSeconds;

        PlayerPrefs.SetInt("LastTime", seconds);
        PlayerPrefs.Save();

        Debug.Log(seconds);

    }
}
