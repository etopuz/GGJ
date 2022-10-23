using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameState state;
    public event Action<bool> OnPauseStateChange;
    public event Action OnExitFromOtherMenus;

    private void Start()
    {
        state = GameState.NotStarted;
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPausePressed();
        }
    }

    private void OnPausePressed()
    {
        if(state == GameState.NotStarted)
        {
            OnExitFromOtherMenus?.Invoke();
        }

        else if(state == GameState.Playing)
        {
            state = GameState.Paused;
            OnPauseStateChange?.Invoke(state == GameState.Paused);
        }

        else if (state == GameState.Paused)
        {
            state = GameState.Playing;
            OnPauseStateChange?.Invoke(state == GameState.Paused);
        }
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        state = GameState.Playing;
        OnPauseStateChange?.Invoke(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        state = GameState.Paused;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        state = GameState.Playing;
        OnPauseStateChange?.Invoke(state == GameState.Paused);
    }

    public void RestartGame()
    {
        ReloadScene();
        StartGame();
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}

[System.Serializable]
public enum GameState
{
    NotStarted,
    Playing,
    Paused,
    Failed,
    Succes
}
