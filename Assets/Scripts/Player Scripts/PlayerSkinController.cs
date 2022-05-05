using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinController : MonoBehaviour
{
    private Player _player;

    public int playerMaterialIndex;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void SetPlayerMaterial(int index)
    {
        playerMaterialIndex = index;
        SaveSystem.SavePlayerMaterialData(this);
        _player.UpdatePlayerSkin();
    }  
}
