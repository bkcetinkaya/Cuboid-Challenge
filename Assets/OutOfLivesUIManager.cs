using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfLivesUIManager : MonoBehaviour
{
    private GameObject OutOfLivesUI;

    public static OutOfLivesUIManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        OutOfLivesUI = GameObject.FindGameObjectWithTag("OOL");

        if (isPlayerHpZero())
        {
            ShowOutOfLivesUI();
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

    private void ShowOutOfLivesUI()
    {
        OutOfLivesUI.SetActive(true);
    }

}
