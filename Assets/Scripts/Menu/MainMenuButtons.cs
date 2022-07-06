using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public Locale EnglishLocale;
    public Locale UkrainianLocale;
    void Awake()
    {
        SetLanguage();
    }
    public void StartGame()
    {
        StateManager.StartGame();
    }

    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.Galysh.ZippyDots");
    }

    public void ChangeLanguage()
    {
        if (LocalizationSettings.SelectedLocale.name == "English")
        {
            PlayerPrefs.SetString("Language", "Ukrainian");
            LocalizationSettings.SelectedLocale = UkrainianLocale;
        }
        else if (LocalizationSettings.SelectedLocale.name == "Ukrainian")
        {
            PlayerPrefs.SetString("Language", "English");
            LocalizationSettings.SelectedLocale = EnglishLocale;
        }
    }

    private void SetLanguage()
    {
        if (!PlayerPrefs.HasKey("Language"))
        {
            if (Application.systemLanguage == SystemLanguage.Russian || Application.systemLanguage == SystemLanguage.Ukrainian)
            {
                PlayerPrefs.SetString("Language", "Ukrainian");
            }
            else
            {
                PlayerPrefs.SetString("Language", "English");
            }
        }

        if(PlayerPrefs.GetString("Language") == "Ukrainian")
        {
            LocalizationSettings.SelectedLocale = UkrainianLocale;
        }
        else if (PlayerPrefs.GetString("Language") == "English")
        {
            LocalizationSettings.SelectedLocale = EnglishLocale;
        }
    }

}
