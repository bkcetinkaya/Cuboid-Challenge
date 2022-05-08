using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField]
    private DieController[] _dieColliders;
    
    private HeartsUIManager heartsUIManager;

    private void Start()
    {

        heartsUIManager = GameObject.FindGameObjectWithTag("HeartsUIManager").GetComponent<HeartsUIManager>();
        
        var borders = GameObject.FindGameObjectsWithTag("Border");
        
        

        _dieColliders = new DieController[borders.Length];

        for (int i = 0; i < borders.Length; i++)
        {
            _dieColliders[i] = borders[i].GetComponent<DieController>();
        }

        for (int i = 0; i < _dieColliders.Length; i++)
        {
            _dieColliders[i].OnPlayerCollided += RestartScene;

        }
    
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
