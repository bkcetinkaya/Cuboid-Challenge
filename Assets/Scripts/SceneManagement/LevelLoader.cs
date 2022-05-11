using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private Button[] levelButtons;

    private OutOfLivesUIManager OutOfLivesUIManager;

    private void Start()
    {
        GetLevelStates();

        try
        {
            OutOfLivesUIManager = GameObject.FindGameObjectWithTag("OOL").GetComponent<OutOfLivesUIManager>();
        }
        catch (Exception e)
        {

            Debug.Log(e);
        }
       
        
    }

    private void GetLevelStates()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i == 0)
            {
                levelButtons[i].interactable = true;
                continue;
            }
            levelButtons[i].interactable = PlayerPrefs.GetInt((i+1).ToString()) == 1;

        }
    }

    public void LoadLevelWithName(string levelName)
    {
        if(levelName.Equals("Shop Menu"))
        {
            SceneManager.LoadScene(levelName);
            return;
        }

        if (levelName.Equals("LevelsMenu"))
        {
            SceneManager.LoadScene(levelName);
            return;
        }

        if (CheckIfPlayerHasEnoughHP())
            SceneManager.LoadScene(levelName);
    }

    public void LoadLevelWithIndex(int index)
    {
        

        if(CheckIfPlayerHasEnoughHP())
            SceneManager.LoadScene(index);
        else
        {
            if(OutOfLivesUIManager != null)
            {
                OutOfLivesUIManager.ShowOutOfLivesUI();
            }
            
        }
    }

    private bool CheckIfPlayerHasEnoughHP()
    {
        var health = PlayerHealthController.Instance.GetPlayerHealth();

        if(health >= 1)
        {
            return true;
        }

        // player has 0 HP
        return false;
    }

}