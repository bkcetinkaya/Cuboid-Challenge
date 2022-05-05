using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public int Money;

    [SerializeField]
    private ShopUIManager shopUI;
    
    private PlayerMoneyController playerMoneyController;
    private PlayerSkinController playerSkinController;

    public Dictionary<int, int> items = new Dictionary<int, int>();

    private void Start()
    {
        InitItems();
        playerMoneyController = GameObject.FindGameObjectWithTag("MoneyController").GetComponent<PlayerMoneyController>();
        playerSkinController = GameObject.FindGameObjectWithTag("SkinController").GetComponent<PlayerSkinController>();
        
    }
    private void InitItems()
    {
        items.Add(0, 100);
        items.Add(1, 100);
        items.Add(2, 100);
        items.Add(3, 100);
        items.Add(4, 100);
        items.Add(5, 100);
    }

    public void BuyItem(int index)
    {

        var isAffordable = IsAffordable(index);

        if (isAffordable)
        {
            DecreaseMoney(index);
            SelectItem(index);
            shopUI.TurnBuyOptionToSelect(index);
            shopUI.UpdateUI();
            return;
        }

        shopUI.DisplayNotEnoughMoneyPopUpNotification(shopUI.selectButtons[index].transform);
    }

  
    private void SelectItem(int index)
    {
        shopUI.DisplayPopUpNotification(shopUI.selectButtons[index].transform);
        playerSkinController.SetPlayerMaterial(index);
    }

    private void DecreaseMoney(int index)
    {
        playerMoneyController.DecreaseMoney(items[index]);
    }

    private bool IsAffordable(int index)
    {
        GetCurrentMoneyData();
        
        if(Money >= items[index])
        {
            return true;
        }

        return false;
    }

    private void GetCurrentMoneyData()
    {
        PlayerMoneyData playerMoneyData = SaveSystem.LoadPlayerMoneyData();
        Money = playerMoneyData.Money;
    }
}
