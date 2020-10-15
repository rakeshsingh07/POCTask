using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace JG.PIP
{
    /// <summary>
    /// Load all the scene
    /// </summary>
    public class MainUIHandler : MonoBehaviour
    {
        //Scene name
        [SerializeField] private string m_CardScene, m_PopUpScene, m_FTUScene, m_InventoryScene;

        //Button references
        [SerializeField] private Button m_CardGameButton, m_PopupButton, m_FTUButton, m_InventoryButton;

        private void Start()
        {
            //Load Card Game
            m_CardGameButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(m_CardScene);
            });


            //Load Popup Scene
            m_PopupButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(m_PopUpScene);
            });

            //Load FTU Scene
            m_FTUButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(m_FTUScene);
            });


            //Load Inventory Scene
            m_InventoryButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(m_InventoryScene);
            });
        }
    }
}
