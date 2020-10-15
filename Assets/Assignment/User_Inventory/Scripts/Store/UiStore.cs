using Inventory.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Inventory
{
    /// <summary>
    /// Show store data in UI
    /// Handler store item purchase
    /// Remove store item on purchase
    /// </summary>
    public class UiStore : MonoBehaviour
    {
        //Serialize field
        //Card layout container
        [SerializeField] private GameObject m_Container;
        //Card list container
        [SerializeField] private Transform m_ListTransform;
        //Card reference
        [SerializeField] private UiStoreItem m_ItemPrefab;

        //Private field
        //Store item list
        private List<UiStoreItem> mUiStoreItems;
        //Store Data
        private StoreData mStoreData;
        //Player Data
        //Wallet balance 
        private PlayerData mPlayerData;
        //Flag for store item initilized
        private bool mIsUiItemInitialised = false;

        //Load Store data 
        private void Awake()
        {
            //PlayerPrefs.DeleteAll();
            mPlayerData = Utility.LoadPlayerData();
            LoadStoreData();
        }

        //Show store UI
        private void Start()
        {                
            Open();
        }

        //Show data in Store UI
        public void Open()
        {
            if (!mIsUiItemInitialised)
                InitStoreItems();
            m_Container.SetActive(true);
        }

        //Initilize store card data
        private void InitStoreItems()
        {
            int length = mStoreData.storeItems.Count;
            mUiStoreItems = new List<UiStoreItem>(length);
            for (int i = 0; i < length; i++)
            {
                var item = mStoreData.storeItems[i];
                if (mPlayerData.purchasedItems.Exists(x => x.id == item.id))
                    continue;
                var uiItem = GameObject.Instantiate(m_ItemPrefab, m_ListTransform);
                uiItem.SetData(mStoreData.storeItems[i]);
                uiItem._BuyButton.onClick.AddListener(() => OnClickPurchase(item));
                mUiStoreItems.Add(uiItem);
            }
            mIsUiItemInitialised = true;
        }

        //Get Card purchse click 
        private void OnClickPurchase(Item item)
        {
            if (mPlayerData.wallet >= item.price)
            {
                mStoreData.RemoveItem(item.id);
                mPlayerData.AddItemToPlayerInventory(item);
                RemoveUiItem(item.id);
            }
            else
            {
                Debug.Log("Not have enough money to purchase this item");
            }
        }

        //Remove item from store after purchse
        private void RemoveUiItem(string id)
        {
            int index = mUiStoreItems.FindIndex(x => x.pItemId == id);
            if (index != -1)
            {
                GameObject obj = mUiStoreItems[index].gameObject;
                mUiStoreItems.RemoveAt(index);
                Destroy(obj);
            }
        }

        //Load main menu
        public void Close()
        {
            SceneManager.LoadScene("MainScene");
        }

        //Load Store Data
        private void LoadStoreData()
        {
            var textAsset = Resources.Load<TextAsset>("store_data");
            if (textAsset)
            {
                mStoreData = JsonUtility.FromJson<StoreData>(textAsset.text);
            }
        }
    }
}
