using UnityEngine;
using JG.PopUpSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace JG.UI
{
    /// <summary>
    /// Popup calls and handle UI button listner 
    /// </summary>
    public class ShowUIPopUp : MonoBehaviour
    {
        //Buttons referance
        [SerializeField] private Button m_DisableDevModeButton, m_AlreadyRegisterdButton, m_LocationAccess, m_HideAll, m_BackButton;

        /// <summary>
        /// Add listner and show pop
        /// </summary>
        private void Start()
        {
            //Disable Dev mode button listner
            m_DisableDevModeButton.onClick.AddListener(() =>
            {
                PopupHandler.ShowPopup("DISABLE_DEV_MODE", "OK", "QUIT", "", false, (PopUpButtonCallback _popCall) =>
                {
                    if (_popCall == PopUpButtonCallback.LEFT)
                    {
                        Debug.Log("Disable dev mode called here. Left");
                    }
                    else if(_popCall == PopUpButtonCallback.RIGHT)
                    {
                        Debug.Log("Disable dev mode called here. Right");
                    }
                });
            });

            //Alreay Register button listner
            m_AlreadyRegisterdButton.onClick.AddListener(() =>
            {
                PopupHandler.ShowPopup("ALREADY_REGISTERED", "LOGIN", "REGISTER", "", false, (PopUpButtonCallback _popCall) =>
                {
                    Debug.Log("ALREADY_REGISTERED called here.");
                });
            });

            //Location button listner
            m_LocationAccess.onClick.AddListener(() =>
            {
                PopupHandler.ShowPopupWithParams("LOCATION_ACCESS","SETTING", "", true, (PopUpButtonCallback _popCall) =>
                {
                    Debug.Log("LOCATION_ACCESS called here.");
                });
            });

            //Hide all popup
            m_HideAll.onClick.AddListener(() =>
            {
                PopupHandler.Hide();
            });

            //Load main scene
            m_BackButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("MainScene");
            });
        }
    }
}
