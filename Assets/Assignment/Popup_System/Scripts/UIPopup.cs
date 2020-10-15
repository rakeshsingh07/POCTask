using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace JG.PopUpSystem
{
    //
    // Summary.
    //   PopUp UI references
    public class UIPopup : MonoBehaviour
    {
        //
        // Summary:
        //   The Root container of PopUp
        public GameObject m_RootGameObject;
        //
        // Summary:
        //   PopUp Heading text
        public Text m_HeadingText;
        //
        // Summary:
        //   PopUp Description text
        public Text m_MessageText;
        //
        // Summary:
        //   PopUp Warning text
        public Text m_WarningText;
        //
        // Summary:
        //   PopUp Single OK Button text
        public Text m_OKButtonText;
        //
        // Summary:
        //   PopUp Left Button text
        public Text m_LeftButtonText;
        //
        // Summary:
        //   PopUp Right Button text
        public Text m_RightButtonText;
        //
        // Summary:
        //   PopUp Cross Button
        public Button _CrossButton;
        //
        // Summary:
        //   PopUp OK Button
        public Button _OkButton;
        //
        // Summary:
        //   PopUp Left Button
        public Button _LeftButton;
        //
        // Summary:
        //   PopUp Right Button
        public Button _RightButton;
    }
}
