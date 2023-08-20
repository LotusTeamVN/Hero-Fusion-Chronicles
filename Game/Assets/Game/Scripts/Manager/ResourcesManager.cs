using System.Collections.Generic;
using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public static ResourcesManager Instance = null;


    public Dictionary<ResourceType, float> Resources
    { 
        get
        {
            if (!ES3Utis.HasKey(GameConstants.resourcesKey))
                ES3Utis.SetKey(GameConstants.resourcesKey, ConfigDataHelper.GetDefaultResource());
            return ES3Utis.GetKey<Dictionary<ResourceType, float>>(GameConstants.resourcesKey);
        }
    }

    public float Gold
    {
        get => Resources[ResourceType.Gold];
        set => OnResourceChanged(ResourceType.Gold, Mathf.Clamp(value, 0, 9999));
    }

    public float Gem
    {
        get => Resources[ResourceType.Gem];
        set => OnResourceChanged(ResourceType.Gem, Mathf.Clamp(value, 0, 9999));
    }


    private Dictionary<ResourceType, List<UI_Resource>> uiResources = new Dictionary<ResourceType, List<UI_Resource>>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(Instance);
    }

    public void AddUIResource(UI_Resource uiResource)
    {
        if (!uiResources.ContainsKey(uiResource.resourceType))
            uiResources.Add(uiResource.resourceType, new List<UI_Resource>() { uiResource });
        else
            uiResources[uiResource.resourceType].Add(uiResource);
    }

    public void RemoveUIResource(UI_Resource uiResource)
    {
        if (uiResources.ContainsKey(uiResource.resourceType))
            uiResources[uiResource.resourceType].Remove(uiResource);
    }

    public List<UI_Resource> GetUIResource(ResourceType type) 
    {
        if (uiResources == null || uiResources.Count <= 0 || !uiResources.ContainsKey(type))
            return null;

        return uiResources[type];
    }

    public void OnResourceChanged(ResourceType resourceType, float newValue)
    {
        Dictionary<ResourceType, float> resources = new Dictionary<ResourceType, float>(Resources);
        resources[resourceType] = newValue;

        List<UI_Resource> uis = GetUIResource(resourceType);

        if (uis != null)
        {
            foreach (var ui in uis)
            {
                if (ui.Active)
                    ui.OnResourceChanged(newValue);
            }
        }

        ES3Utis.SetKey(GameConstants.resourcesKey, resources);
    }
}
