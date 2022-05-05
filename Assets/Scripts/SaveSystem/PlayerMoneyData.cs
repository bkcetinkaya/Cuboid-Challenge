using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerMoneyData 
{
    public int Money;

    public PlayerMoneyData(PlayerMoneyController playerMoneyController)
    {
        Money = playerMoneyController.Money;
    }


}
