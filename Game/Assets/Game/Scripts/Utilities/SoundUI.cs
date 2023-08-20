using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;


public class SoundUI : MonoBehaviour, IPointerDownHandler, IPointerClickHandler
{
    public enum ClickEvent { ClickDown, ClickUp }

    [Space]
    public ClickEvent playOn = ClickEvent.ClickDown;
    public AssetReference soundAsset = null;
    public float delayTime = 0f;
    [Range(0, 1)]
    public float volumeScale = 1;
    

    private AudioClip _audio = null;

    private void Awake()
    {
        if (_audio == null)
            Addressables.LoadAssetAsync<AudioClip>(soundAsset).Completed += (handle) => { _audio = handle.Result; };
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(PlaySound(ClickEvent.ClickDown));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        StartCoroutine(PlaySound(ClickEvent.ClickUp));
    }

    private IEnumerator PlaySound(ClickEvent playOn)
    {
        if (this.playOn == playOn)
        {
            yield return new WaitForSeconds(delayTime);
            SoundManager.Instance.PlayOnShot(_audio, volumeScale);
        }
    }
}
