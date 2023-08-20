using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class IToggleGroup<T, T1> : MonoBehaviour where T : IToggle<T1> where T1 : class
{
    public T Selected => items.Find(c => c.Selected);
    public bool AnyOn => Selected != null;

    protected List<T> items = new List<T>();


    protected virtual void Awake()
    {
        items = GetComponentsInChildren<T>().ToList();
    }

    public virtual void Initialized()
    {

    }
}
