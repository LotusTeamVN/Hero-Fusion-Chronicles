using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SoundBackground : MonoBehaviour
{
    public AssetReference soundAsset = null;
    [Range(0, 1)]
    public float volume = 1f;

    private void Awake()
    {
        Addressables.LoadAssetAsync<AudioClip>(soundAsset).Completed += (handle) => 
        {
            SoundManager.Instance.SetBackgroundSound(handle.Result, volume);
        };
    }
}
