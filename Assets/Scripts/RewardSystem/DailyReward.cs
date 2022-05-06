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
    
    [Space]

    [SerializeField]
    [Tooltip("in 24h format for example: 12 or 24 or 09")]
    private int rewardHour;

    [SerializeField]
    private int rewardMinute;

    TimeSpan _result;
    double currCountdownValue ;

    private void Start()
    {


        if (!PlayerPrefs.HasKey("RewardClaimable"))
        {
            PlayerPrefs.SetInt("RewardClaimable", 1);
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.GetInt("RewardClaimable") == 0)
        {
            
            SetClaimRewardButton(false);
            SetCountDownText(true);
            SetTimeDifference();
            StartCoroutine(StartCountdown());
        }
        if (PlayerPrefs.GetInt("RewardClaimable") == 1)
        {
            
            claimRewardButton.interactable = true;
            SetCountDownText(false);
            StopCoroutine(StartCountdown());
        }    

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
            Debug.Log("Negative Value!");

            return diff.Duration();
        }

        TimeSpan result = offsetTime.Subtract(diff);
 
        return result;

    }

    public IEnumerator StartCountdown()
    {
        
        while (currCountdownValue > 0.9f)
        {
            Debug.Log("Countdown: " + currCountdownValue);
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
    }

    private void SetRewardUnClaimable()
    {
        PlayerPrefs.SetInt("RewardClaimable", 0);
        PlayerPrefs.Save();
    }
    public void ClaimReward()
    {
        PlayerHealthController.Instance.SetPlayerHealth(3);


        ResetTimer();
    }

    private void ResetTimer()
    {
        SetRewardUnClaimable();
        SetClaimRewardButton(false);
        SetCountDownText(true);
        SetTimeDifference();
        StartCoroutine(StartCountdown());
    }
}
