using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Data
{
    /// <summary>
    /// Handle player data
    /// Add store data in player intentory data
    /// </summary>
    [System.Serializable]
    public class PlayerData
    {
        //Player wallet
        public float wallet;
        //Purchased item list
        public List<Item> purchasedItems;

        //load player data
        public PlayerData()
        {
            purchasedItems = new List<Item>();
            wallet = 10000;
        }

        //wrapper to convert in json
        public string ToJson()
        {
            return UnityEngine.JsonUtility.ToJson(this);
        }
        
        //Add items in player inventory list
        public void AddItemToPlayerInventory(Item item)
        {
            if (purchasedItems.Contains(item))
                return;
            wallet -= item.price;
            purchasedItems.Add(item);
            Utility.SavePlayerData(ToJson());
        }
    }
}