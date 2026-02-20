using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Music")]
    public AudioClip[] bgmTracks;
    public AudioClip winMusic;

    [Header("SFX")]
    public AudioClip deathSound;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        // Create AudioSources automatically
        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        bgmSource.loop = true;
        bgmSource.playOnAwake = false;
        bgmSource.spatialBlend = 0f;

        sfxSource.loop = false;
        sfxSource.playOnAwake = false;
        sfxSource.spatialBlend = 0f;
    }

    void Start()
    {
        PlayRandomBGM();
    }

    public void PlayRandomBGM()
    {
        if (bgmTracks.Length == 0) return;

        int index = Random.Range(0, bgmTracks.Length);
        bgmSource.clip = bgmTracks[index];
        bgmSource.Play();
    }

    public void PlayWinMusic()
    {
        bgmSource.Stop();
        bgmSource.clip = winMusic;
        bgmSource.loop = false;
        bgmSource.Play();
    }

    public void PlayDeathSound()
    {
        if (deathSound != null)
            sfxSource.PlayOneShot(deathSound, 1f);
    }
}