using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenuController : MonoBehaviour
{

    [SerializeField]
    private WinController winController;
    [SerializeField]
    private TextMeshProUGUI _MoneyEarnedText;
    [SerializeField]
    private TextMeshProUGUI _LevelCompledetText;

    private string sceneName;
   

   
    void Start()
    {
              
        winController = GameObject.FindGameObjectWithTag("WinController").GetComponent<WinController>();
        winController.OnPlayerCollided += DisplayWinMenu;
        sceneName = SceneManager.GetActiveScene().name;
        DisableWinMenuContent();
    }

  
    public void DisableWinMenuContent()
    {
        foreach (Transform item in transform)
        {

            if (item.gameObject.CompareTag("WinMenu"))
            {
                
                continue;
            }

                item.gameObject.SetActive(false);                     
        }      
    }

    public void ActivateWinMenuContent()
    {
        foreach (Transform item in transform)
        {

            if (item.gameObject.CompareTag("WinMenu"))
            {
                Debug.Log("Yarrak");
                continue;
            }

            item.gameObject.SetActive(true);


        }


    }

    public void DisplayWinMenu()
    {
        ActivateWinMenuContent();
        if (winController.isNextLevelUnlockedAlready())
        {
            _MoneyEarnedText.text = "0";
            _LevelCompledetText.text = sceneName + " Was Already Completed";
        }
        else
        {
            _MoneyEarnedText.text = "10";
            _LevelCompledetText.text = sceneName + " Completed";
        }
    }

}
