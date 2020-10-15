using Inventory.Data;
using UnityEngine;

namespace Inventory
{
    /// <summary>
    /// Utility class for:
    /// Load Data form playerPrefs
    /// Save Data in playePrefs
    /// </summary>
    public class Utility
    {
        //Load Data 
        public static PlayerData LoadPlayerData()
        {
            string playerDataJson = PlayerPrefs.GetString("playerData", string.Empty);
            if (string.IsNullOrEmpty(playerDataJson))
                return new PlayerData();
            return UnityEngine.JsonUtility.FromJson<PlayerData>(playerDataJson);
        }

        //Save Data
        public static void SavePlayerData(string playerJson)
        {
            PlayerPrefs.SetString("playerData", playerJson);
        }
    }
}
