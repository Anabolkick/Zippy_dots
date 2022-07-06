using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;
using static SoundManager;

public class SoundButton : MonoBehaviour
{
    public Sprite[] ModeSprites;
    public LocalizeStringEvent SoundsOn;
    public LocalizeStringEvent SoundsOff;
    public LocalizeStringEvent MusicOff;

    void Start()
    {
        Instance.SetSoundMode();
        ChangeSoundImage();
    }
    public void ChangeMode()
    {
        Instance.ChangeSoundMode();
        ChangeSoundImage();
    }

    private void ChangeSoundImage()
    {
        if (SoundMode == SoundModes.Unmute)
        {
            gameObject.GetComponent<Image>().sprite = ModeSprites[0];
            SoundsOff.enabled = false;
            MusicOff.enabled = false;
            SoundsOn.enabled = true;
        }
        else if (SoundMode == SoundModes.MuteMusic)
        {
            gameObject.GetComponent<Image>().sprite = ModeSprites[1];
            SoundsOn.enabled = false;
            SoundsOff.enabled = false;
            MusicOff.enabled = true;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = ModeSprites[2];
            MusicOff.enabled = false;
            SoundsOn.enabled = false;
            SoundsOff.enabled = true;
        }
    }
}
