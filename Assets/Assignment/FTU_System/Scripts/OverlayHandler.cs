using System.Collections.Generic;
using UnityEngine;

namespace JG.FTUSystem
{
    /// <summary>
    /// Responsive for Generate ovelay on the main canvas
    /// </summary>
    public class OverlayHandler : MonoBehaviour
    {
        [SerializeField] private GameObject m_ImgObj;     
        [SerializeField] private RectTransform m_Canvas;
        [SerializeField] private Transform m_Parent;
        [SerializeField] private List<RectTransform> m_ObjToHighlight;
        private List<GameObject> m_OverlayObject = new List<GameObject>();
        //Set highlighted UI
        public void SetHighletedObject(List<RectTransform> m_ObjToHighlight)
        {
            ResetOverlay();
            List<Rectangle> rects = RegionFinder.GetCoverRects(m_Canvas, m_ObjToHighlight);
            GenerateOverlay(rects);
        }

        //Generate Overlay over canvas
        private void GenerateOverlay(List<Rectangle> rects)
        {
            for (int i = 0; i < rects.Count; i++)
            {
                Rectangle rect = rects[i];
                var obj = Instantiate<GameObject>(m_ImgObj);
                m_OverlayObject.Add(obj);
                obj.transform.SetParent(m_Parent.transform, false);
                obj.transform.localScale = Vector3.one;
                RectTransform rTrans = obj.GetComponent<RectTransform>();
                rTrans.localPosition = GetPositionFromOverlay(rect);
                rTrans.sizeDelta = GetOverlaySize(rect);
            }
        }

        //Get overlay size
        private Vector2 GetOverlaySize(Rectangle rect)
        {
            return new Vector2(rect.v11.x - rect.v00.x, rect.v11.y - rect.v00.y);
        }


        //Get rect position from overlay
        private Vector3 GetPositionFromOverlay(Rectangle rect)
        {
            Vector2 size = GetOverlaySize(rect);
            return new Vector3(rect.v00.x + size.x / 2, rect.v00.y + size.y / 2, 0);
        }  
        
        //Reset Overlay images
        private void ResetOverlay()
        {
            for(int i=0; i< m_OverlayObject.Count; i++)
            {
                Destroy(m_OverlayObject[i]);
            }
            m_OverlayObject = new List<GameObject>();
        }
    }
}
