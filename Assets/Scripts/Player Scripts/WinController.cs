using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    public event Action OnPlayerCollided;
    private Player _cubeController;
    private Rigidbody _playerRigidbody;
    private Collider _playerCollider;
    private LevelsManager _levelsManager;
    private PlayerMoneyController _playerMoneyController;
    


    [SerializeField]
    private Material dissolveMaterial;

    private bool isWon=false;

    private void Start()
    {
     
        _cubeController = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _playerRigidbody = _cubeController.GetComponent<Rigidbody>();
        _playerCollider = _cubeController.GetComponent<Collider>();
        _levelsManager = GameObject.FindGameObjectWithTag("LevelsManager").GetComponent<LevelsManager>();
        _playerMoneyController = GameObject.FindGameObjectWithTag("PlayerMoneyController").GetComponent<PlayerMoneyController>();
        dissolveMaterial.SetFloat("Height", 2.6f);
     
        Color color = _cubeController.GetComponent<Renderer>().material.color;
        dissolveMaterial.SetColor("ColorVal", color);
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinCollider"))
        {
            

            isWon = true;
            // If the player is not dead we can display the dissolve effect
            if (_playerRigidbody.isKinematic)
            {
                
                
                _cubeController.GetComponent<Renderer>().material = dissolveMaterial;
                if (!isNextLevelUnlockedAlready())
                {
                    _playerMoneyController.AddMoney(10);

                }
                
                StartCoroutine(WinAndThenIvokeNextLevel());
                
            }
            
        }
    }

    public bool isNextLevelUnlockedAlready()
    {
        // is next level unlocked?
        bool NextLevelUnlocked = PlayerPrefs.GetInt(GetNextLevelName()) == 1;

        return NextLevelUnlocked;
    }

    private void Update()
    {
        if (!isWon)
        {
            return;
        }

        dissolveMaterial.SetFloat("Height", Mathf.Lerp(dissolveMaterial.GetFloat("Height"), 0f, 2 * Time.deltaTime));
    }
    private IEnumerator WinAndThenIvokeNextLevel()
    {
        // Turn off isTrigger on player collider to avoid falling through objects while displaying dissolve
        _playerCollider.isTrigger = false;
        // Disabling isKinematic to disable movements input
        _playerRigidbody.isKinematic = false;

        AudioManager.Instance.Play("WinSound");

        yield return new WaitForSeconds(1.5f);
        
        OnPlayerCollided?.Invoke();
        _levelsManager.SetLevelState(GetNextLevelName(), 1);
    }

    private static string GetNextLevelName()
    {
       // string levelName = SceneManager.GetActiveScene().name;
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;

        //StringBuilder stringBuilder = new StringBuilder();

        //for (int i = 0; i < levelName.Length-1; i++)
        //{
        //    stringBuilder.Append(levelName[i]);
        //}

        //stringBuilder.Append(sceneIndex + 1);
        Debug.Log((sceneIndex + 1).ToString());
        return (sceneIndex + 1).ToString();
    }
}
