using TMPro;
using UnityEngine;

public class ScoreChanges : MonoBehaviour
{

    public static int lastScore;
    private static TMP_Text _scoreText;
    private static int _score;

    void Awake()
    {
        _scoreText = gameObject.GetComponent<TMP_Text>();
        EventManager.OnLoseEvent.AddListener(ResetScore);
    }

    public static void ScoreChanged()
    {
        _score++;
        EventManager.OnScoreChangedEvent.Invoke();
        _scoreText.text = _score.ToString();
    }

    private void ResetScore()
    {
        var highscore = PlayerPrefs.GetInt("Score", 0);
        if (highscore < _score)
        {
            highscore = _score;
            PlayerPrefs.SetInt("Score", highscore);
            Social.ReportScore(highscore, GPS.leaderboard_leaderboard, _ => {});
        }
        lastScore = _score;
        _score = 0;
        _scoreText.text = _score.ToString();
    }

}
