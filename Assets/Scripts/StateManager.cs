using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;
    public static bool InGame;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InGame = false;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

    }


    public static void StartGame()
    {
        SceneManager.LoadScene("Game");
        InGame = true;
    }
}
