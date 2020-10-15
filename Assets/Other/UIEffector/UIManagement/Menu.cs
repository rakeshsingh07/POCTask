using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework.UIManagement
{
    //WIP: Add/Remove.

    public class Menu : UIBaseBehaviour
    {       
        [SerializeField] protected RectTransform m_Viewport;
        [SerializeField] protected RectTransform m_Content;
        [SerializeField] private UIBaseBehaviour m_Template ;
        private UI m_ParentUI;
        public RectTransform pContent { get => m_Content; }

        public UI pParentUI
        {
            get
            {
                if (m_ParentUI == null)
                    m_ParentUI = GetComponentInParent<UI>();
                return m_ParentUI;
            }
        }

        public UIBaseBehaviour Add(string name)
        {
            GameObject newWidgetObj = Instantiate(m_Template.gameObject, m_Content);
            newWidgetObj.name = name;
            newWidgetObj.transform.localScale = m_Template.transform.localScale;

            if (m_Template is Widget)
            {
                Widget widget = newWidgetObj.GetComponent<Widget>();
                widget.Initialize(pParentUI, null);
                pParentUI.pChildWidgets.Add(widget);
                widget.pVisible = true;
            }
            else if (m_Template is UI)
            {
                UI ui = newWidgetObj.GetComponent<UI>();
                ui.Initialize(pParentUI);
                pParentUI.pChildUIs.Add(ui);
                ui.pVisible = true;
            }
            return newWidgetObj.GetComponent<UIBaseBehaviour>();
        }
    }
}
