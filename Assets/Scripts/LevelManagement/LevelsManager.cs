using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
   
    public int levelCount;

    private void Start()
    {
        RemoveAllPlayerPrefsOnFirstLaunch();
        SetPlayerPrefsIfNotExists();      
       
    }


    private void RemoveAllPlayerPrefsOnFirstLaunch()
    {
        if (PlayerPrefs.HasKey("PrefsRemoved"))
        {
            return;
        }

        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("PrefsRemoved", 1);

    }

    private void SetPlayerPrefsIfNotExists()
    {
        if (PlayerPrefs.HasKey("1"))
        {
            return;
        }

        //Lock every level except level 1
        for (int i = 1; i < levelCount; i++)
        {
            string name = i.ToString();
            Debug.Log(name);
            PlayerPrefs.SetInt(name, 0);
        }
       
    }

    public void SetLevelState(string levelName,int value)
    {       
        PlayerPrefs.SetInt(levelName, value);
    }

}
