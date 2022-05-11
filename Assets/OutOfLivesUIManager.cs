using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfLivesUIManager : MonoBehaviour
{
    private PlayerDieController playerDieController;
    private AdsManager adsManager;
    private string _currentSceneName;


    
    private void Start()
    {
        playerDieController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDieController>();
        adsManager = GameObject.FindGameObjectWithTag("Ads Manager").GetComponent<AdsManager>();

        if (isPlayerHpZero())
        {          
            ShowOutOfLivesUI();
        }

        HideUI();

        playerDieController.OnPlayerHasNoHp += ShowOutOfLivesUI;
        adsManager.OnRewardClaimed += HideUI;
    }

    public void HideUI()
    {
        foreach (Transform item in transform)
        {

            if (item.gameObject.CompareTag("OOL"))
            {

                continue;
            }

            item.gameObject.SetActive(false);


        }
    }

    private bool isPlayerHpZero()
    {
        int health = PlayerHealthController.Instance.GetPlayerHealth();

        if (health == 0)
        {
            return true;
        }

        return false;
    }

    public void ShowOutOfLivesUI()
    {
        foreach (Transform item in transform)
        {

            if (item.gameObject.CompareTag("OOL"))
            {
                
                continue;
            }

            item.gameObject.SetActive(true);


        }
    }

}
