using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPaused;
    private bool isDead;
    public event Action<bool,bool> OnStateChange;

    private void Start()
    {
        isPaused = true;
        isDead = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || isDead)
        {
            ChangeState();
        }
    }

    private void ChangeState()
    {
        isPaused = !isPaused;
        OnStateChange?.Invoke(isPaused, isDead);
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        ResumeGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        ChangeState();
    }


}
