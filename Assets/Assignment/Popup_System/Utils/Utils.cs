using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

namespace JG.PopUpSystem
{
    public class Utils : MonoBehaviour
    {
        //Animation for popup
        public static void PlayAnimation(GameObject root, Image background, bool show, Action onComplete = null)
        {
            if (show)
            {
                root.transform.localScale = Vector3.zero;
                background.enabled = true;
                root.SetActive(true);               
                root.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutQuint).SetUpdate(UpdateType.Late, true).OnComplete(() => {
                    onComplete?.Invoke();
                });
            }
            else
            {
                root.SetActive(false);
                background.enabled = false;
                onComplete?.Invoke();               
            }
        }
    }
}
