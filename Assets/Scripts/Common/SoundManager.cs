using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource animalRunningSource;

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip[] soundEffects;

    void Awake()
    {
        // Singleton setup
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        PlayMusic(backgroundMusic);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (clip == null) return;
        musicSource.clip = clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFXByIndex(int index)
    {
        if (index < 0 || index >= soundEffects.Length) return;
        PlaySFX(soundEffects[index]);
    }
    public void PlayingRunningSound()
    {
        if (animalRunningSource.isPlaying) return; 
        StartCoroutine(FadeIn(animalRunningSource, 3));

    }
    public void StopRunningSound()
    {
        StartCoroutine(FadeOut(animalRunningSource, 3));

    }
    public IEnumerator FadeIn(AudioSource audioSource, float duration)
    {
        audioSource.volume = 0f;
        audioSource.Play();
        
        float targetVolume = 1f;
        float startTime = Time.time;

        while (audioSource.volume < targetVolume)
        {
            audioSource.volume = Mathf.Clamp01((Time.time - startTime) / duration);
            yield return null;
        }
    }
    public IEnumerator FadeOut(AudioSource audioSource, float duration)
    {
        float startVolume = audioSource.volume;
        float startTime = Time.time;

        while (audioSource.volume > 0f)
        {
            audioSource.volume = Mathf.Clamp01(startVolume - ((Time.time - startTime) / duration));
            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume; // Reset volume if needed
    }
    public AudioClip myClip;
    public GameObject audioPrefab; // A prefab with an AudioSource component

    public void PlaySoundAtPosition(Vector3 position)
    {
        GameObject soundObject = Instantiate(audioPrefab, position, Quaternion.identity);
        AudioSource source = soundObject.GetComponent<AudioSource>();
        source.clip = myClip;
        source.Play();
        Destroy(soundObject, myClip.length); // Clean up after playback
    }


}
