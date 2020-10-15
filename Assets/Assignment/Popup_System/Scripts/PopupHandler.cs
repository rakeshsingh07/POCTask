using System;
using System.Collections.Generic;
using UnityEngine;
using JG.PopUpSystem.PopupData;
using UnityEngine.UI;

namespace JG.PopUpSystem
{
    /// <summary>
    /// Popup button type
    /// </summary>
    public enum PopUpButtonCallback
    {
        CROSS = 0,
        OK = 1,
        RIGHT = 2,
        LEFT = 3
    }

    /// <summary>
    /// Queued Popup data 
    /// Button lable text
    /// </summary>
    public class QueuedPopUpData : Data
    {
        /// <summary>
        /// Button action listner
        /// </summary>
        public Action<PopUpButtonCallback> callback;
        /// <summary>
        /// Onay butto/submit button text
        /// </summary>
        public string OkButtonLabel;
        /// <summary>
        /// Left button text
        /// </summary>
        public string LeftButtonLable;
        /// <summary>
        /// Right button text
        /// </summary>
        public string RightButtonLabel;
    }

    /// <summary>
    /// Set popup button listner
    /// Set popup UI
    /// set button text
    /// set popup data
    /// </summary>
    public class PopupHandler : MonoBehaviour
    {
        //Button action callback
        private Action<PopUpButtonCallback> buttonCallback;   
        //Currence instance
        private static PopupHandler _instance;
        //Queued popup 
        private Queue<QueuedPopUpData> m_PopUpQueud = new Queue<QueuedPopUpData>();
        //Current queued popup
        private QueuedPopUpData mCurrentCard = null;
        //Popup UI data
        private UIPopup m_GenericPopup = null;  
        //Popup prefab reference
        [SerializeField] GameObject m_GenericPopupPrefab = null;
       
        /// <summary>
        /// Create singleton instance 
        /// </summary>
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        /// <summary>
        /// Instantiate current popup type
        /// Set instantiated popup rectransform 
        /// Set popup button listner
        /// </summary>
        /// <returns> current popup type</returns>
        private UIPopup GetPopupGO()
        {
            if (m_GenericPopup == null)
            {
                GameObject go = Instantiate(m_GenericPopupPrefab);
                go.transform.SetParent(transform); // root
                go.transform.SetAsFirstSibling();
                go.transform.localScale = Vector3.one;
                RectTransform trans = go.GetComponent<RectTransform>();
                trans.localPosition = Vector3.zero;
                trans.offsetMax = Vector2.zero;
                trans.offsetMin = Vector2.zero;
                m_GenericPopup = go.GetComponent<UIPopup>();

                // Set Listner
                m_GenericPopup._OkButton.onClick.AddListener(() =>
                {
                    if ((bool)_instance.mCurrentCard?.IsAutoClose)
                        Hide();
                    buttonCallback?.Invoke(PopUpButtonCallback.OK);
                });

                m_GenericPopup._LeftButton.onClick.AddListener(() =>
                {
                    if ((bool)_instance.mCurrentCard?.IsAutoClose)
                        Hide();
                    buttonCallback?.Invoke(PopUpButtonCallback.LEFT);
                });

                m_GenericPopup._RightButton.onClick.AddListener(() =>
                {
                    if ((bool)_instance.mCurrentCard?.IsAutoClose)
                        Hide();
                    buttonCallback?.Invoke(PopUpButtonCallback.RIGHT);
                });

                m_GenericPopup._CrossButton.onClick.AddListener(() =>
                {
                    if ((bool)_instance.mCurrentCard?.IsAutoClose)
                        Hide();
                    buttonCallback?.Invoke(PopUpButtonCallback.CROSS);
                });

            }
            return m_GenericPopup;
        }

        /// <summary>
        /// Store current popup data 
        /// Enque current popup
        /// </summary>
        /// <param name="aTitle"> Title Lable</param>
        /// <param name="aMessage"> Message Lable</param>
        /// <param name="aWarningText"> Warning Lable</param>
        /// <param name="aOkButtontext"> OK button Lable</param>
        /// <param name="aLeftButtontext"> Left button Lable</param>
        /// <param name="aRightButtontext"> Right button Lable</param>
        /// <param name="aCross"> Cross button refernce</param>
        /// <param name="aCallback"> Button Listner</param>
        private void StoreData(string aTitle, string aMessage, string aWarningText, string aOkButtontext, string aLeftButtontext, string aRightButtontext, bool aCross, Action<PopUpButtonCallback> aCallback)
        {
            QueuedPopUpData tData = new QueuedPopUpData
            {
                HeadingMessage = aTitle,
                DescriptionMessage = aMessage,
                WarningMessage = aWarningText,
                callback = aCallback,
                CrossAllowed = aCross,
                OkButtonLabel = aOkButtontext,
                LeftButtonLable = aLeftButtontext,
                RightButtonLabel = aRightButtontext,
                IsAutoClose = true
            };
            m_PopUpQueud.Enqueue(tData);           
            ShowUI(tData);
        }

        /// <summary>
        /// Set UI 
        /// </summary>
        /// <param name="aData"> Queued Popup data </param>
        private void ShowUI(QueuedPopUpData aData)
        {
            mCurrentCard = aData;
            // Fill Data in UI
            UIPopup tPop = _instance.GetPopupGO();
            tPop.m_HeadingText.text = aData.HeadingMessage;
            tPop.m_MessageText.text = aData.DescriptionMessage;
            tPop.m_WarningText.text = aData.WarningMessage;
            tPop.m_OKButtonText.text = aData.OkButtonLabel;
            tPop.m_RightButtonText.text = aData.RightButtonLabel;
            tPop.m_LeftButtonText.text = aData.LeftButtonLable;
            buttonCallback = aData.callback;

            // Button Activation 
            bool tValue = string.IsNullOrEmpty(aData.OkButtonLabel);
            tPop._OkButton.gameObject.SetActive(!tValue);
            tPop._RightButton.gameObject.SetActive(tValue);
            tPop._LeftButton.gameObject.SetActive(tValue);
            tPop._CrossButton.gameObject.SetActive(aData.CrossAllowed);

            m_GenericPopup.gameObject.SetActive(true);
 
            //Play Popup UI tween animation
            Utils.PlayAnimation(m_GenericPopup.m_RootGameObject, m_GenericPopup.gameObject.GetComponent<Image>(), true);            
        }

        /// <summary>
        /// Get popup Data
        /// </summary>
        /// <param name="aPopupType"> popup Type </param>
        /// <returns> Popup Data </returns>
        public static Data GetPopupsData(string popUpType)
        {
            return PopupDataHandler.pInstance._PopupData.data.Find(ls => ls.PopUpType == popUpType);
        }

        /// <summary>
        /// Single Button Enum
        /// </summary>
        /// <param name="aPopupType"> popup Type </param>
        /// <param name="aLeftButtonText"> Left button text </param>
        /// <param name="aRightButtonText"> Right button text </param>
        /// <param name="aCross"> Cross button flag </param>
        /// <param name="aCallback"> Button clicl callback </param>
        public static void ShowPopup(string aPopupType, string aOkButtontext, string aWarningText = "", bool aCross = false, Action<PopUpButtonCallback> aCallback = null)
        {
            Data tempData = GetPopupsData(aPopupType);
            _instance.StoreData(tempData.HeadingMessage, tempData.DescriptionMessage, aWarningText, aOkButtontext, "", "", aCross, aCallback);
        }

        /// <summary>
        ///  Double Button Enum
        /// </summary>
        /// <param name="aPopupType"> popup Type </param>
        /// <param name="aLeftButtonText"> Left button text </param>
        /// <param name="aRightButtonText"> Right button text </param>
        /// <param name="aWarningText"> Warning Text </param>
        /// <param name="aCross"> Cross button flag </param>
        /// <param name="aCallback"> Button clicl callback </param>
        /// <param name="args"> Parameter list </param>

        public static void ShowPopup(string aPopupType, string aLeftButtonText, string aRightButtonText, string aWarningText = "", bool aCross = false, Action<PopUpButtonCallback> aCallback = null)
        {           
            Data tempData = GetPopupsData(aPopupType);
            Debug.Log("SHow pop up called =========== " + tempData.PopUpType);
            _instance.StoreData(tempData.HeadingMessage, tempData.DescriptionMessage, aWarningText, "", aLeftButtonText, aRightButtonText, aCross, aCallback);
        }

        /// <summary>
        /// Single Button Enum with Params
        /// </summary>
        /// <param name="aPopupType"> popup Type </param>
        /// <param name="aLeftButtonText"> Left button text </param>
        /// <param name="aRightButtonText"> Right button text </param>
        /// <param name="aWarningText"> Warning Text </param>
        /// <param name="aCross"> Cross button flag </param>
        /// <param name="aCallback"> Button clicl callback </param>
        /// <param name="args"> Parameter list </param>
        public static void ShowPopupWithParams(string aPopupType, string aOkButtontext, string aWarningText = "", bool aCross = false, Action<PopUpButtonCallback> aCallback = null, params string[] args)
        {
            Data tempData = GetPopupsData(aPopupType);
            tempData.DescriptionMessage = string.Format(tempData.DescriptionMessage, args);
            _instance.StoreData(tempData.HeadingMessage, tempData.DescriptionMessage, aWarningText, aOkButtontext, "", "", aCross, aCallback);
        }

        /// <summary>
        /// Double Button Enum with Params
        /// </summary>
        /// <param name="aPopupType"> popup Type </param>
        /// <param name="aLeftButtonText"> Left button text </param>
        /// <param name="aRightButtonText"> Right button text </param>
        /// <param name="aWarningText"> Warning Text </param>
        /// <param name="aCross"> Cross button flag </param>
        /// <param name="aCallback"> Button clicl callback </param>
        /// <param name="args"> Parameter list </param>
        public static void ShowPopupWithParams(string aPopupType, string aLeftButtonText, string aRightButtonText, string aWarningText = "", bool aCross = false, Action<PopUpButtonCallback> aCallback = null, params string[] args)
        {
            Data tempData = GetPopupsData(aPopupType);
            tempData.DescriptionMessage = string.Format(tempData.DescriptionMessage, args);
            _instance.StoreData(tempData.HeadingMessage, tempData.DescriptionMessage, aWarningText, "", aLeftButtonText, aRightButtonText, aCross, aCallback);
        }


        /// <summary>
        ///  Hide the current and show next stacked
        /// </summary>
        public static void Hide()
        {
            if (_instance.mCurrentCard == null)
                return;

            if (_instance.m_GenericPopup == null)
            {
                Debug.LogError("Generic Popup is null.");
                return;
            }

            _instance.mCurrentCard = null;

            //Play Popup UI tween animation
            Utils.PlayAnimation(_instance.m_GenericPopup.m_RootGameObject, _instance.m_GenericPopup.gameObject.GetComponent<Image>(), false, _instance.HideAfterAnimation);
        }

        /// <summary>
        /// Hide all and clear queue
        /// </summary>
        public static void HideAll()
        {
            _instance.mCurrentCard = null;
            _instance.m_PopUpQueud.Clear();
            
            if (_instance.m_GenericPopup == null)
            {
                Debug.LogError("Generic Popup is null.");
                return;
            }

            //Play Popup UI tween animation
            Utils.PlayAnimation(_instance.m_GenericPopup.m_RootGameObject, _instance.m_GenericPopup.gameObject.GetComponent<Image>(), false, _instance.HideAfterAnimation);
        }

        /// <summary>
        ///  Invoking this method after Hide animation complete.
        /// </summary>        
        private void HideAfterAnimation()
        {
            _instance.m_GenericPopup.gameObject.SetActive(false);
            _instance.ShowUI(_instance.m_PopUpQueud.Peek());
            if(_instance.m_PopUpQueud.Count > 0)
            {
                _instance.ShowUI(_instance.m_PopUpQueud.Peek());
            }            
        }
    }
}
