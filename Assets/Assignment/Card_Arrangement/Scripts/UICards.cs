using Framework.UIManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Button = Framework.UIManagement.Button;

namespace CardArrangement
{
    /// <summary>
    /// Have refernce of GroupButton, CardTamplete and menu
    /// Get Selected card data
    /// </summary>
    public class UICards : UI, IWidgetBeginDragHandler, IWidgetDragHandler, IWidgetDropHandler, IWidgetDragEndHandler, IWidgetClickHandler
    {
        [SerializeField] private Menu m_Menu; //Menu refernce
        [SerializeField] private Button m_GroupButton; //Group button refernce
        [SerializeField] private Card m_CardTemplate; //Card tampelet rerence
        [SerializeField] private Button m_BackButton; //Back button
        private HashSet<Card> mSelectedCard = new HashSet<Card>(); //Getter  for Seledcted card

        //Set placeholder and careate Cards
        public void CreateCards(string name, Sprite sprite)
        {
            Widget placeHolder = m_Menu.Add("PlaceHolder") as Widget;
            Card card = m_CardTemplate.Duplicate(name) as Card;
            card.pImage = sprite;
            placeHolder.AddChild(card);
            card.SetPlaceholder(placeHolder.transform);
        }

        //On Begin Drag called

        void IWidgetBeginDragHandler.OnBeginDrag(Widget widget, PointerEventData eventData)
        {
            ClearSelection();
            (widget as Card).pCanvasGroup.blocksRaycasts = false;
            widget.transform.SetParent(transform);
        }

        //On Drag called
        void IWidgetDragHandler.OnDrag(Widget widget, PointerEventData eventData)
        {
            widget.pPosition = eventData.position;
        }


        //On Drop called
        void IWidgetDropHandler.OnDrop(Widget dropWidget, Widget dragWidget, PointerEventData eventData)
        {
            Transform placeHolder = (dropWidget as Card).pPlaceHolder;
            (dropWidget as Card).SetPlaceholder((dragWidget as Card).pPlaceHolder);
            (dragWidget as Card).SetPlaceholder(placeHolder);
        }

        //On Drag End called
        void IWidgetDragEndHandler.OnDragEnd(Widget widget, PointerEventData eventData)
        {
            Card card = widget as Card;
            card.pCanvasGroup.blocksRaycasts = true;
            card.pIsChecked = false;
            card.SetPlaceholder(card.pPlaceHolder);
        }

        //OnClic called
        void IWidgetClickHandler.OnClick(Widget widget, PointerEventData eventData)
        {
            if (widget is Card)
            {
                if ((widget as Card).pIsChecked)
                    mSelectedCard.Add(widget as Card);
                else
                    mSelectedCard.Remove(widget as Card);
                m_GroupButton.pVisible = mSelectedCard.Count > 1;
            }
            else if (widget == m_GroupButton)
                ReArranageSelectedCards();
        }


        //Rearrage Selected cards
        private void ReArranageSelectedCards()
        {
            int count = -1;
            foreach (Card card in mSelectedCard)
                card.pPlaceHolder.SetSiblingIndex(++count);
            ClearSelection();
        }

        //Clear Selected cards group
        private void ClearSelection()
        {
            foreach (Card card in mSelectedCard)
                card.pIsChecked = false;
            mSelectedCard.Clear();
            m_GroupButton.pVisible = false;
        }


        //Load main menu
        public void OnBackButton()
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
