using Framework.UIManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CardArrangement
{
    /// <summary>
    /// Card tamplete 
    /// Referance for CardHolder, Canvas Group
    /// Set Place holder data
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class Card : ToggleButton
    {
        //Player holder transform
        public Transform pPlaceHolder { get; private set; }

        //Canvas group
        private CanvasGroup mCanvasGroup;
        public CanvasGroup pCanvasGroup { get { return mCanvasGroup == null ? mCanvasGroup = GetComponent<CanvasGroup>() : mCanvasGroup; } }

        /// <summary>
        /// Set parent
        /// Set local position and Placeholder for cards
        /// </summary>
        /// <param name="placeHolder"></param>
        public void SetPlaceholder(Transform placeHolder)
        {
            transform.SetParent(placeHolder);
            pPlaceHolder = placeHolder;
            pLocalPosition = Vector2.zero;
        }
    }
}
