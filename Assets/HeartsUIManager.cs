using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsUIManager : MonoBehaviour
{
    [SerializeField]
    private Image[] hearts;
    
    private int HealthValue;

    [SerializeField]
    private GameObject NotEnoughHPUI;

    private void Start()
    {
        NotEnoughHPUI.SetActive(true);
        UpdateUI();


        
    }

   

    public void UpdateUI()
    {
        ResetUI();
        HealthValue = PlayerHealthController.Instance.GetPlayerHealth();

        Debug.Log("Current HEALTH: " + HealthValue);

        if (HealthValue == 0)
        {
            NotEnoughHPUI.SetActive(true);
            return;
        }

        for (int i = 0; i < HealthValue; i++)
        {
            hearts[i].fillAmount = 1f;
        }
        NotEnoughHPUI.SetActive(false);
    }

    private void ResetUI()
    {
        for (int i = 0; i < 3; i++)
        {
            hearts[i].fillAmount = 0f;
        }
    }
}
