using DG.Tweening;
using UnityEngine;

public static class GameUtilities
{
    public static RectTransform GetChildByName(this RectTransform parent, string name)
    {
        int childCount = parent.childCount;
        for (int i = 0; i < childCount; i++)
        {
            RectTransform rect = parent.GetChild(i) as RectTransform;
            if (rect.gameObject.name.Replace("(Clone)", "").Equals(name))
                return rect;
        }
        return null;
    }

    public static T TryGetComponent<T>(this MonoBehaviour mono, ref T component)
    {
        if (component == null)
            component = mono.GetComponent<T>();
        return component;
    }

    public static T TryGetComponentInChildren<T>(this MonoBehaviour mono, ref T component)
    {
        if (component == null)
            component = mono.GetComponentInChildren<T>();
        return component;
    }

    public static T[] TryGetComponentsInChildren<T>(this MonoBehaviour mono, ref T[] components)
    {
        if (components == null)
            components = mono.GetComponentsInChildren<T>();
        return components;
    }


    public static void Swap<T>(ref T value1, ref T value2)
    {
        T temp = value1;
        value1 = value2;
        value2 = temp;
    }

    public static void CreateContainer(string name, Transform parent, ref Transform container)
    {
        GameObject obj = new GameObject(name);
        container = obj.transform;
        container.SetParent(parent);
    }

    public static void CreateContainer(this MonoBehaviour parent, string name, ref Transform container)
    {
        GameObject obj = new GameObject(name);
        container = obj.transform;
        container.SetParent(parent.transform);
    }

    public static int GetRandomValue(int min, int max)
    {
        int value = Random.Range(min, max);
        return value;
    }

    public static float GetRandomValue(float min, float max)
    {
        float value = Random.Range(min, max);
        return value;
    }


    public static void Stop(this Tween tween)
    {
        if (tween != null)
            tween.Kill();
    }

    public static void Active(this CanvasGroup canvasGr)
    {
        canvasGr.interactable = canvasGr.blocksRaycasts = true;
        canvasGr.alpha = 1;
    }

    public static void DeActive(this CanvasGroup canvasGr)
    {
        canvasGr.interactable = canvasGr.blocksRaycasts = false;
        canvasGr.alpha = 0;
    }

    public static bool OnRange(this float value, float min, float max)
    {
        return value >= min && value <= max;
    }

    public static bool GetRandomOnRange(float min, float max)
    {
        float randomValue = GetRandomValue(0, 10001) / 100f;
        return randomValue.OnRange(min, max);
    }

}
