using UnityEngine;

public interface IPool
{
    string Name { get; }

    void Show();

    void Hide();

    void SetParent(Transform parent);
}
