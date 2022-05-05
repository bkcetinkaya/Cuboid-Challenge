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

    private void Start()
    {
        GetLevelStates();
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
        SceneManager.LoadScene(levelName);
    }

    public void LoadLevelWithIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

}