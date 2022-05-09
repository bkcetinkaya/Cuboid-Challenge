using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
  
    private PlayerDieController playerDieController;
    private HeartsUIManager heartsUIManager;

    private AdsManager adsManager;

    private void Start()
    {
        adsManager = GameObject.FindGameObjectWithTag("Ads Manager").GetComponent<AdsManager>();
        heartsUIManager = GameObject.FindGameObjectWithTag("HeartsUIManager").GetComponent<HeartsUIManager>();
        playerDieController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDieController>();
        var borders = GameObject.FindGameObjectsWithTag("Border");

        playerDieController.OnPlayerDied += RestartScene;
        adsManager.OnRewardClaimed += RestartScene;
    }

    private void RestartScene()
    {
        if (CheckIfPlayerHasEnoughHP())
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else
        {
            heartsUIManager.UpdateUI();           
        }
    }

    public void NextLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        

        if(scene.buildIndex >= 32)
        {

            LoadLevelWithName("LevelsMenu");
            return;
        }

        if (CheckIfPlayerHasEnoughHP())
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadLevel(int index)
    {
        if (CheckIfPlayerHasEnoughHP())
            SceneManager.LoadScene(index);
    }

    public void LoadLevelWithName(string levelName)
    {
        if(levelName.Equals("LevelsMenu") || levelName.Equals("Shop Menu"))
        {
            SceneManager.LoadScene(levelName);
            return;
        }

        if (CheckIfPlayerHasEnoughHP())
             SceneManager.LoadScene(levelName);
    }

    private bool CheckIfPlayerHasEnoughHP()
    {
        var health = PlayerHealthController.Instance.GetPlayerHealth();

        if (health >= 1)
        {
            return true;
        }

        // player has 0 HP
        return false;
    }
}
