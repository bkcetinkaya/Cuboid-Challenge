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

    bool isRewardAvaliable;
    

    private void Start()
    {
        _result = CalculateTimeDifference();
        currCountdownValue = _result.TotalSeconds;
     
        

        if(currCountdownValue > 0)
        {
            isRewardAvaliable = false;
            StartCoroutine(StartCountdown());
        }
        if(currCountdownValue == 0)
        {
            isRewardAvaliable = true;
        }
        

    }

    private TimeSpan CalculateTimeDifference()
    {
        DateTime currentTime = DateTime.Now;
        DateTime rewardTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 12, 00, 00);


        TimeSpan offsetTime = new TimeSpan(24, 00, 00);

        TimeSpan diff = currentTime.Subtract(rewardTime);

        TimeSpan result = offsetTime - diff;

        return result;
    }

    public IEnumerator StartCountdown()
    {
        
        while (currCountdownValue > 0)
        {
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;

            timeLeftText.text = TimeSpan.FromSeconds(currCountdownValue).ToString(@"dhh\:mm\:ss");
        }
    }


}
