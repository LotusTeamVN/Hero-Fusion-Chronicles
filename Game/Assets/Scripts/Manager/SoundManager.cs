using Runtime.Definition;
using UnityEngine;
using UnityEngine.AddressableAssets;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    private AudioSource audioSource = null;
    public AudioSource AudioSource => this.TryGetComponent(ref audioSource);

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(Instance);
    }

    public void SetBackgroundSound(AudioClip audio, float volume = 1)
    {
        AudioSource.clip = audio;
        AudioSource.volume = volume;
        AudioSource.Stop();
        AudioSource.Play();
    }

    public void PlayOnShot(AudioClip auido, float volume = 1) => AudioSource.PlayOneShot(auido, volume);

    public void PlayOnShot(string soundName, float volume = 1)
    {
        Addressables.LoadAssetAsync<AudioClip>(string.Format(GameConstants.soundPath, soundName)).Completed += (handle) =>
        {
            AudioSource.PlayOneShot(handle.Result, volume);
        };
    }
}

