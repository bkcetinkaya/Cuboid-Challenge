using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutOfLivesUIManager : MonoBehaviour
{
    private DieController _dieController;

    private string _currentSceneName;


    private void Awake()
    {

        _currentSceneName = SceneManager.GetActiveScene().name;
        if (!_currentSceneName.Equals("LevelsMenu") && !_currentSceneName.Equals("Shop Menu"))
        {
            _dieController = GameObject.FindGameObjectWithTag("Border").GetComponent<DieController>();
            _dieController.OnPlayerDied += ShowOutOfLivesUI;
        }
    }
    private void Start()
    {
        

        
        
        if (isPlayerHpZero() && !_currentSceneName.Equals("LevelsMenu") && _currentSceneName.Equals("Shop Menu"))
        {
            
            ShowOutOfLivesUI();
        }

        HideUI();
    }

    private void HideUI()
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

    private void ShowOutOfLivesUI()
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
