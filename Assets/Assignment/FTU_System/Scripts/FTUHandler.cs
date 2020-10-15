using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace JG.FTUSystem
{
    /// <summary>
    /// Responsible for Overlay and show active object in screen
    /// </summary>
    public class FTUHandler : MonoBehaviour
    {
        //Serialize field
        [SerializeField] private Button m_GroupButton, m_SingleButton; //Button referece
        [SerializeField] private OverlayHandler m_OverlayGenerator; //Overlay generator referece
        [SerializeField] private List<RectTransform> m_SingleObjectHighlight; //List of single object Highlight
        [SerializeField] private List<RectTransform> m_GroupObjectHighlight; //List of multiple object Highlight
        [SerializeField] private Button m_BackButton; //Back button
         //Set single and group highlight object
        private void Start()
        {
            //Single ftu show
            m_SingleButton.onClick.AddListener(() => {
                m_OverlayGenerator.SetHighletedObject(m_SingleObjectHighlight);

            });

            //multi card ftu show
            m_GroupButton.onClick.AddListener(() => {
                m_OverlayGenerator.SetHighletedObject(m_GroupObjectHighlight);

            });

            //Load main scene
            m_BackButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("MainScene");
            });
        }
    }
}
