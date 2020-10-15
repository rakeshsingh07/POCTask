using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Core logic of Popup Data
/// Get data from file 
/// </summary>
namespace JG.PopUpSystem.PopupData
{
    /// <summary>
    /// Popup Data class
    /// </summary>
    [System.Serializable]
    public class Data
    {
        //Popup Title text
        public string HeadingMessage;
        //Popup Desciption text
        public string DescriptionMessage;
        //Popup Warning text
        public string WarningMessage;
        //Cross button allowed in popup
        public bool CrossAllowed;
        //Auto Popup close 
        public bool IsAutoClose;
        //Popup type 
        public string PopUpType;        
    }

    /// <summary>
    /// Popup Data container class
    /// Store popup data in list
    /// </summary>
    [System.Serializable]
    public class PopData
    {
        public List<Data> data = new List<Data>();
    }

    /// <summary>
    /// Deserialize popup data from file
    /// Hold popup data
    /// </summary>
    [System.Serializable]
    public class PopupDataHandler : MonoBehaviour
    {
        /// <summary>
        /// Popup data file referance
        /// </summary>
        [SerializeField] private TextAsset m_PopupData;
        /// <summary>
        /// Popup Data properties
        /// </summary>
        public PopData _PopupData { get; private set; }
        /// <summary>
        /// PopupDataHandler instance
        /// </summary>
        public static PopupDataHandler pInstance { get; private set; }

        /// <summary>
        /// Deserialize popup data from file to data class
        /// Set current instance
        /// </summary>
        private void Start()
        {
            if (pInstance == null)
                pInstance = this;
            else
                Destroy(this);
            _PopupData = JsonConvert.DeserializeObject<PopData>(m_PopupData.text);          
        }
    }
}