using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMoneyController : MonoBehaviour
{
  
    public int Money=0;

    private void Start()
    {
        InitMoneyData();
    }

    private void InitMoneyData()
    {
        if (PlayerPrefs.HasKey("MoneyData"))
        {
            return;
        }

        PlayerPrefs.SetInt("MoneyData", 1);
        SaveMoneyData();
    }

    public void AddMoney(int value)
    {
        Money = GetCurrentMoneyData();

        Money += value;
        SaveMoneyData();       
    }
 
    public void DecreaseMoney(int value)
    {
        Money = GetCurrentMoneyData();
        Money -= value;
        SaveMoneyData();     
    } 

    private int GetCurrentMoneyData()
    {

        PlayerMoneyData playerMoneyData = SaveSystem.LoadPlayerMoneyData();     

        return playerMoneyData.Money;
    }

    private void SaveMoneyData()
    {
        SaveSystem.SavePlayerMoneyData(this);
    }
}
