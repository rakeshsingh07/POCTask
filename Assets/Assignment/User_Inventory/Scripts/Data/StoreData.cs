using System.Collections.Generic;

namespace Inventory.Data
{
    /// <summary>
    /// Responsibel to store data
    /// </summary>
    [System.Serializable]
    public class StoreData
    {
        //Store item list
        public List<Item> storeItems;

        //Wrapper for to load json
        public string ToJson()
        {
            return UnityEngine.JsonUtility.ToJson(this);
        }

        //Remove purchsed item form store list
        public bool RemoveItem(string id)
        {
            var index = storeItems.FindIndex((x) => x.id == id);
            if (index == -1)
                return false;
            storeItems.RemoveAt(index);
            return true;
        }
    }
}