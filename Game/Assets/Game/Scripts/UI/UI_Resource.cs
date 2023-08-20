using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Resource : MonoBehaviour
{
    public ResourceType resourceType;
    public Button addButton = null;
    public TMP_Text valueTxt = null;

    public bool Active => gameObject.activeInHierarchy;

    private void Start()
    {
        ResourcesManager.Instance.AddUIResource(this);
        OnResourceChanged(ResourcesManager.Instance.Resources[resourceType]);
    }

    public void OnResourceChanged(float newValue)
    {
        valueTxt.text = newValue.ToString();
    }
}
