using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI moneyText;

    [SerializeField]
    public Button[] buyButtons;

    [SerializeField]
    public Button[] selectButtons;

    [SerializeField]
    public Transform popUpNotificationText;

    [SerializeField]
    public Transform popUpNotEnoughMoneyNotificationText;


    public int moneyAmount;

    void Awake()
    {
        UpdateUI();
    }
    private void Start()
    {
        popUpNotificationText.gameObject.SetActive(false);

        if (!PlayerPrefs.HasKey("slctbtn1"))
        {
            PlayerPrefs.SetInt("slctbtn0", 0);
            PlayerPrefs.SetInt("slctbtn1", 0);
            PlayerPrefs.SetInt("slctbtn2", 0);
            PlayerPrefs.SetInt("slctbtn3", 0);
            PlayerPrefs.SetInt("slctbtn4", 0);
            PlayerPrefs.SetInt("slctbtn5", 0);
        }

        for (int i = 0; i < selectButtons.Length; i++)
        {
            selectButtons[i].gameObject.SetActive(PlayerPrefs.GetInt("slctbtn"+i) == 1);
        }
    }

    public void UpdateUI()
    {
        PlayerMoneyData playerMoneyData = SaveSystem.LoadPlayerMoneyData();
        if (playerMoneyData != null)
        {
            moneyAmount = playerMoneyData.Money;
            moneyText.text = "Money: " + moneyAmount.ToString();
        }
      
    }

    public void DisplayPopUpNotification(Transform transform)
    {
        StartCoroutine(DisplayPopUp(transform));
    }

    public void DisplayNotEnoughMoneyPopUpNotification(Transform transform)
    {
        StartCoroutine(DisplayNotEnoughMoneyPopUp(transform));
    }

    public void TurnBuyOptionToSelect(int index)
    {
        selectButtons[index].gameObject.SetActive(true);
        PlayerPrefs.SetInt("slctbtn"+index, 1);
    }

    IEnumerator DisplayPopUp(Transform transform)
    {
        
        popUpNotificationText.transform.position = transform.position;
        popUpNotificationText.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.7f);
        popUpNotificationText.gameObject.SetActive(false);
    }

    IEnumerator DisplayNotEnoughMoneyPopUp(Transform transform)
    {

        popUpNotEnoughMoneyNotificationText.transform.position = transform.position;
        popUpNotEnoughMoneyNotificationText.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.7f);
        popUpNotEnoughMoneyNotificationText.gameObject.SetActive(false);
    }
}
