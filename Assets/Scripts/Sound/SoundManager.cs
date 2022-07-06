using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public static SoundModes SoundMode { get; private set; }

    private AudioSource _soundsSource;
    private AudioSource _musicSource;

    [SerializeField] private AudioClip _pointGainedClip;
    [SerializeField] private AudioClip _pointLoseClip;
    [SerializeField] private AudioClip _loseClip;
    [SerializeField] private AudioClip _changeDirClip;
 


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        _musicSource = gameObject.GetComponentsInChildren<AudioSource>()[1];
        _soundsSource = gameObject.GetComponent<AudioSource>();
        EventManager.OnScoreIncreasedEvent.AddListener(PlayPointGainedSound);
        EventManager.OnLoseEvent.AddListener(PlayLoseSound);
        SoundMode = (SoundModes)PlayerPrefs.GetInt("SoundMode", 0);
        SetSoundMode();
    }

    private void PlayPointGainedSound()
    {
        _soundsSource.PlayOneShot(_pointGainedClip);
    }

    public void PlayPointLoseSound()
    {
        _soundsSource.PlayOneShot(_pointLoseClip);
    }

    private void PlayLoseSound()
    {
        _soundsSource.PlayOneShot(_loseClip);
    }

    public void PlayChangeDirSound()
    {
        _soundsSource.PlayOneShot(_changeDirClip);
    }

    public void ChangeSoundMode()
    {
        if (SoundMode == SoundModes.Unmute)
        {
            SoundMode = SoundModes.MuteMusic;
            PlayerPrefs.SetInt("SoundMode", 0);
        }
        else if (SoundMode == SoundModes.MuteMusic)
        {
            SoundMode = SoundModes.MuteAll;
            PlayerPrefs.SetInt("SoundMode", 1);
        }
        else
        {
            SoundMode = SoundModes.Unmute;
            PlayerPrefs.SetInt("SoundMode", 2);
        }
        PlayerPrefs.Save();
        SetSoundMode();
    }

    public void SetSoundMode()
    {
        if (SoundMode == SoundModes.MuteMusic)
        {
            _soundsSource.enabled = true;
            _musicSource.enabled = false;
        }
        else if (SoundMode == SoundModes.MuteAll)
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

    public enum SoundModes
    {
        MuteMusic,
        MuteAll,
        Unmute
    }

}