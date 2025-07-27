using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField]private AudioSource bgmSource;
    [SerializeField]private AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField]private AudioClip bgmClip;
    [SerializeField]private AudioClip clickClip;
    [SerializeField]private AudioClip winClip;
    [SerializeField]private AudioClip loseClip;
    [SerializeField]private AudioClip moveBox;
    [SerializeField]private AudioClip bottleFilled;
    [SerializeField]private AudioClip clearBox;
    [SerializeField]private AudioClip shake;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayBGM();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM()
    {
        if (bgmSource != null && bgmClip != null)
        {
            bgmSource.clip = bgmClip;
            bgmSource.loop = true;
            bgmSource.Play();
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayClickSound()
    {
        PlaySFX(clickClip);
    }

    public void PlayWinSound()
    {
        PlaySFX(winClip);
    }

    public void PlayLoseSound()
    {
        PlaySFX(loseClip);
    }

    public void PlayMoveBoxSound()
    {
        PlaySFX(moveBox);
    }

    public void PlayBottleFilledSound()
    {
        PlaySFX(bottleFilled);
    }

    public void PlayClearBoxSound()
    {
        PlaySFX(clearBox);
    }

    public void PlayShakeSound()
    {
        PlaySFX(shake);
    }
}
