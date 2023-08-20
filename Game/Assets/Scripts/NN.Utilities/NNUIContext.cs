using UnityEngine;

namespace NN.Utilities
{
    public class NNUIContext : MonoBehaviour
    {
        private RectTransform uiViewContainer = null;
        private RectTransform uiPopupContainer = null;


        private void Awake()
        {
            if (uiViewContainer == null)
                uiViewContainer = transform.Find("UIView") as RectTransform;

            if (uiPopupContainer == null)
                uiPopupContainer = transform.Find("UIPopup") as RectTransform;

            NNUIManager.SetPopupsContainer(uiPopupContainer);
            NNUIManager.SetViewContainer(uiViewContainer);
        }
    }
}

