using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsUIManager : MonoBehaviour
{
    [SerializeField]
    private Image[] hearts;

    private int HealthValue;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        ResetUI();
        HealthValue = PlayerHealthController.Instance.GetPlayerHealth();

        if (HealthValue == 0)
        {
            return;
        }

        for (int i = 0; i < HealthValue; i++)
        {
            hearts[i].fillAmount = 1f;
        }
    }

    private void ResetUI()
    {
        for (int i = 0; i < 3; i++)
        {
            hearts[i].fillAmount = 0f;
        }
    }
}
