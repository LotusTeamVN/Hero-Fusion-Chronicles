using NN.Utilities;
using UnityEngine;

public static class NNUIManager
{
    public static RectTransform PopupContainer { get; private set; }
    public static RectTransform ViewContainer { get; private set; }


    
    public static readonly string UIContextPrefabPath = "NNUI/----- UIContext -----";


    public static void CreateContainer()
    {
        if (PopupContainer == null || ViewContainer == null)
            CreateUIContext.CreatePrefab(UIContextPrefabPath);
    }

    public static void SetPopupsContainer(RectTransform rect) => PopupContainer = rect;

    public static void SetViewContainer(RectTransform rect) => ViewContainer = rect;
    
    public static NNUIPopup GetPopup(string popupName) => ObjectPooling.Instance.PopPopup(popupName, false);

    public static NNUIPopup GetPopupVisible(string popupName) => PopupContainer.GetChildByName(popupName)?.GetComponent<NNUIPopup>();
}
