using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace NN.Utilities
{
    [DefaultExecutionOrder(-100)]
    public class ObjectPooling : MonoBehaviour
    {
        public static ObjectPooling Instance = null;

        private readonly string POPUP_PATH = "Assets/AddressableAssets/Prefabs/UI/Popup/{0}.prefab";

        private List<NNUIPopup> popups = new List<NNUIPopup>();

        private Transform PopupsContainer = null;
        

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(Instance);
            NNUIManager.CreateContainer();
        }

        public void PushPopup(NNUIPopup popup, bool hide) => PushToPool(popup, popups, hide, PopupsContainer);

        public NNUIPopup PopPopup(string popupName, bool show) => PopFromPool(popupName, popups, string.Format(POPUP_PATH, popupName), show, NNUIManager.PopupContainer);


        private void PushToPool<T>(T obj, List<T> collection, bool hide, Transform parent) where T : IPool
        {
            collection.Add(obj);

            obj.SetParent(parent);

            if (hide)
                obj.Hide();
        }

        private T PopFromPool<T>(string objName, List<T> collection, string path, bool show, Transform newParent) where T : IPool
        {
            T obj = collection.Find(o => objName.Equals(o.Name));

            if (obj == null)
                obj = Addressables.InstantiateAsync(path).WaitForCompletion().GetComponent<T>();
            else
                collection.Remove(obj);

            obj.SetParent(newParent);

            if (show)
                obj.Show();

            return obj;
        }
    }
}

