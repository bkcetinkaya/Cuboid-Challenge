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
            claimRewardButton.interactable = false;
            
            SetTimeDifference();
            StartCoroutine(StartCountdown());
        }
        if (PlayerPrefs.GetInt("RewardClaimable") == 1)
        {
            
            claimRewardButton.interactable = true;
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
        DateTime rewardTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 12,13, 00);


        TimeSpan offsetTime = TimeSpan.FromHours(24);

        Debug.Log(offsetTime.TotalSeconds);
        TimeSpan diff = currentTime.Subtract(rewardTime);
       
        if(diff < TimeSpan.Zero)
        {
            Debug.Log("Negative Value!");

            return diff.Duration();
        }

        TimeSpan result = offsetTime.Subtract(diff);
        //Debug.Log(result);


        return result;



        

    }

    public IEnumerator StartCountdown()
    {
        
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;


            timeLeftText.text = TimeSpan.FromSeconds(currCountdownValue).ToString(@"hh\:mm\:ss");
        }
    }


    public void ClaimReward()
    {
        
    }
}
