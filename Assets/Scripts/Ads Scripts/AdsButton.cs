using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsButton : MonoBehaviour
{
    private AdsManager adsManager;
    private Button adsButton;

    private void Start()
    {
        adsManager = GameObject.FindGameObjectWithTag("Ads Manager").GetComponent<AdsManager>();
        adsButton = GetComponent<Button>();
        adsButton.onClick.AddListener(adsManager.ShowAd);
    }

}
