using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerSkinData
{
    public int playerMaterialIndex;

    public PlayerSkinData(PlayerSkinController playerSkinController)
    {
        playerMaterialIndex = playerSkinController.playerMaterialIndex;
    }
}
