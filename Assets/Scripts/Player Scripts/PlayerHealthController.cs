using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController Instance { get; private set; }

    public int Health { get; set; }


    private void Awake()
    {
        if(Instance != null)
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
        
        
        //If there is no key called "Health" it means the game is being played for the first time
        if (!PlayerPrefs.HasKey("Health"))
        {
            Health = 3;
            //Set health to its full capacity on first launch
            PlayerPrefs.SetInt("Health", Health);
            PlayerPrefs.Save();
        }


        GetPlayerHealth();
    }


    public void SetPlayerHealth(int value)
    {
        
        Health = GetPlayerHealth();
        
        //prevent health overflowing
        

        Health += value;
        if (Health > 3)
        {
            Health = 3;
        }

        if(Health < 0)
        {
            Health = 0;
        }
        PlayerPrefs.SetInt("Health", Health);
        PlayerPrefs.Save();
    }

    public int GetPlayerHealth()
    {
       var _health =  PlayerPrefs.GetInt("Health");

        if (Health > 3)
        {
            Health = 3;
        }

        return _health;
    }
   
}
