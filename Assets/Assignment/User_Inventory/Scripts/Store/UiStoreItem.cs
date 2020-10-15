using Inventory.Data;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class UiStoreItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_ItemName,m_ItemPrice;
        [SerializeField] public Button _BuyButton;
        public string pItemId { get; private set; }
        public void SetData(Item item)
        {
            pItemId = item.id;
            m_ItemName.text = item.name;
            m_ItemPrice.text = item.price.ToString();
        }
    }
}