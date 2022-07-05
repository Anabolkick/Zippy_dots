using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.PropertyVariants.TrackedProperties;
using UnityEngine.Localization.Tables;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public Sprite[] Mode_sprites;
    public LocalizeStringEvent Sounds_on;
    public LocalizeStringEvent Sounds_off;
    public LocalizeStringEvent Music_off;

    void Start()
    {
        ChangeSoundImage();
        EventManager.OnSoundModeChangedEvent.AddListener(ChangeSoundImage);
    }
    public void ChangeMode()
    {
        EventManager.OnSoundModeChangedEvent.Invoke();
    }

    private void ChangeSoundImage()
    {
        if (SoundManager.Sound_mode == SoundManager.SoundMode.Unmute)
        {
            gameObject.GetComponent<Image>().sprite = Mode_sprites[0];
            Sounds_off.enabled = false;
            Music_off.enabled = false;
            Sounds_on.enabled = true;
        }
        else if (SoundManager.Sound_mode == SoundManager.SoundMode.MuteMusic)
        {
            gameObject.GetComponent<Image>().sprite = Mode_sprites[1];
            Sounds_on.enabled = false;
            Sounds_off.enabled = false;
            Music_off.enabled = true;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = Mode_sprites[2];
            Music_off.enabled = false;
            Sounds_on.enabled = false;
            Sounds_off.enabled = true;
        }
    }
}
