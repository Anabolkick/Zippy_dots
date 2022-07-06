using System.Collections;
using GooglePlayGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject PauseMenu;
    public GameObject LoseMenu;
    public GameObject LoseBlur;
    public GameObject InGameHud;
    public ProjectileSpawner Spawner;
    public Material BlurMat;

    public int highscore;
    public int score;

    void Awake()
    {
        EventManager.OnScoreIncreasedEvent.AddListener(MakeHarder);
        EventManager.OnLoseEvent.AddListener(() => StartCoroutine(OpenLoseMenu()));

        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(_ => {});
    }
    public void PauseGame()
    {
        InGameHud.SetActive(false);
        PauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        InGameHud.SetActive(true);
        PauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        StateManager.StartGame();
    }

    public void LoadMainMenu()
    {
        StateManager.InGame = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    private IEnumerator OpenLoseMenu()
    {
        StateManager.InGame = false;
        score = ScoreChange.LastScore;
        highscore = PlayerPrefs.GetInt("Score", 0);

        BlurMat.SetFloat("_Size", 0);
        InGameHud.SetActive(false);
        LoseBlur.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        float curBlur = 0;
        float finalBlur = 16f;
        float iterCount = 10f;
        float time = 0.55f;
        var addValue = finalBlur / iterCount;
        for (int i = 0; i <= iterCount; i++)
        {
            BlurMat.SetFloat("_Size", curBlur);
            curBlur += addValue;
            yield return new WaitForSeconds(time/iterCount);
        }
        LoseMenu.SetActive(true);
        Spawner.Difficult = 1f;
        Spawner.SpawnDelay = 1.6f;
    }

    private void MakeHarder()
    {
        Spawner.Difficult += 0.02f;
        Spawner.SpawnDelay -= 0.03f;
    }
}
