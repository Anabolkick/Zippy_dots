using System.Collections;
using GooglePlayGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject loseMenu;
    public GameObject loseBlur;
    public GameObject inGameHud;
    public ProjectileSpawner spawner;
    public Material blurMat;

    public int highscore;
    public int score;

    void Awake()
    {
        EventManager.OnScoreChangedEvent.AddListener(MakeHarder);
        EventManager.OnLoseEvent.AddListener(() => StartCoroutine(OpenLoseMenu()));

        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(_ => {});
    }
    public void PauseGame()
    {
        inGameHud.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    public void ResumeGame()
    {
        inGameHud.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }

    private IEnumerator OpenLoseMenu()
    {
        score = ScoreChanges.lastScore;
        highscore = PlayerPrefs.GetInt("Score", 0);

        blurMat.SetFloat("_Size", 0);
        inGameHud.SetActive(false);
        loseBlur.SetActive(true);
        yield return new WaitForSeconds(0.3f);

        float curBlur = 0;
        float finalBlur = 16f;
        float iterCount = 10f;
        float time = 0.55f;
        var addValue = finalBlur / iterCount;
        for (int i = 0; i <= iterCount; i++)
        {
            blurMat.SetFloat("_Size", curBlur);
            curBlur += addValue;
            yield return new WaitForSeconds(time/iterCount);
        }
        loseMenu.SetActive(true);
        spawner.difficult = 1f;
        spawner.spawnDelay = 1.6f;
    }

    private void MakeHarder()
    {
        spawner.difficult += 0.02f;
        spawner.spawnDelay -= 0.03f;
    }
}
