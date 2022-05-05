
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{
    public static void SavePlayerMaterialData(PlayerSkinController playerSkinController)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/mat.erial";

        FileStream fileStream = new FileStream(path, FileMode.Create);

        PlayerSkinData playerSkinData = new PlayerSkinData(playerSkinController);

        binaryFormatter.Serialize(fileStream, playerSkinData);
        fileStream.Close();
    }

    public static void SavePlayerMoneyData(PlayerMoneyController playerMoneyController)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/mo.ney";

        FileStream fileStream = new FileStream(path, FileMode.Create);

        PlayerMoneyData playerMoneyData = new PlayerMoneyData(playerMoneyController);

        binaryFormatter.Serialize(fileStream, playerMoneyData);
        fileStream.Close();
    }

    public static PlayerMoneyData LoadPlayerMoneyData()
    {
        string path = Application.persistentDataPath + "/mo.ney";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            PlayerMoneyData playerMoneyData = binaryFormatter.Deserialize(fileStream) as PlayerMoneyData;
            fileStream.Close();

            return playerMoneyData;
        }
        else
        {
            return null;
        }
    }
    public static PlayerSkinData LoadPlayerMaterialData()
    {
        string path = Application.persistentDataPath + "/mat.erial";
        if (File.Exists(path))
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream(path, FileMode.Open);
            PlayerSkinData playerSkinData = binaryFormatter.Deserialize(fileStream) as PlayerSkinData;
            fileStream.Close();

            return playerSkinData;
        }
        else
        {
            return null;
        }
    }
}
