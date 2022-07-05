using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    public Locale englishLocale;
    public Locale ukrainianLocale;
    void Awake()
    {
        SetLanguage();
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");
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
            LocalizationSettings.SelectedLocale = ukrainianLocale;
        }
        else if (LocalizationSettings.SelectedLocale.name == "Ukrainian")
        {
            PlayerPrefs.SetString("Language", "English");
            LocalizationSettings.SelectedLocale = englishLocale;
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
            LocalizationSettings.SelectedLocale = ukrainianLocale;
        }
        else if (PlayerPrefs.GetString("Language") == "English")
        {
            LocalizationSettings.SelectedLocale = englishLocale;
        }
    }

}
