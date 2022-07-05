using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundMode Sound_mode { get; private set; }
    private AudioSource _soundsSource;
    private AudioSource _musicSource;
    public AudioClip pointGainedClip;
    public AudioClip loseClip;    
    public AudioClip changeDirClip;

    public static SoundManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        _musicSource = gameObject.GetComponentsInChildren<AudioSource>()[1];
        _soundsSource = gameObject.GetComponent<AudioSource>();
        EventManager.OnScoreChangedEvent.AddListener(PlayPointGainedSound);
        EventManager.OnLoseEvent.AddListener(PlayLoseSound);
        EventManager.OnSoundModeChangedEvent.AddListener(ChangeSoundMode);
        EventManager.OnDirectionChangedEvent.AddListener(PlayChangeDirSound);
        Sound_mode = (SoundMode)PlayerPrefs.GetInt("SoundMode", 0);
        ActiveSoundMode();
    }

    private void PlayPointGainedSound()
    {
        _soundsSource.PlayOneShot(pointGainedClip);
    }

    private void PlayLoseSound()
    {
        _soundsSource.PlayOneShot(loseClip);
    }

    private void PlayChangeDirSound()
    {
        _soundsSource.PlayOneShot(changeDirClip);
    }

    private void ChangeSoundMode()
    {
        if (Sound_mode == SoundMode.Unmute)
        {
            Sound_mode = SoundMode.MuteMusic;
            PlayerPrefs.SetInt("SoundMode", 0);
        }
        else if (Sound_mode == SoundMode.MuteMusic)
        {
            Sound_mode = SoundMode.MuteAll;
            PlayerPrefs.SetInt("SoundMode", 1);
        }
        else
        {
            Sound_mode = SoundMode.Unmute;
            PlayerPrefs.SetInt("SoundMode", 2);
        }
        PlayerPrefs.Save();
        ActiveSoundMode();
    }

    private void ActiveSoundMode()
    {
        if (Sound_mode == SoundMode.MuteMusic)
        {
            _soundsSource.enabled = true;
            _musicSource.enabled = false;
        }
        else if (Sound_mode == SoundMode.MuteAll)
        {
            _musicSource.enabled = false;
            _soundsSource.enabled = false;
        }
        else
        {
            _soundsSource.enabled = true;
            _musicSource.enabled = true;
        }
    }

    public enum SoundMode
    {
        MuteMusic,
        MuteAll,
        Unmute
    }

}