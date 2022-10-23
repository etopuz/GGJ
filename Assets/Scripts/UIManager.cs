using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject currentMenu;

    [Header("Menus")]
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject howToPlayMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject inGameMenu;
    [SerializeField] private GameObject failMenu;
    [SerializeField] private GameObject winMenu;

    [Header("BossHealth")]
    [SerializeField] private Image bossHealthBar;
    private Boss boss;

    private void Start()
    {
        currentMenu = mainMenu;
        gameManager.OnPauseStateChange += UpdateUIOnPause;
        gameManager.OnExitFromOtherMenus += OpenMainMenu;
        boss = GameObject.FindGameObjectWithTag("boss").GetComponent<Boss>();
    }

    private void Update()
    {
        UpdateBossHealthBar();
        CheckWin();
    }

    private void CheckWin()
    {
        if(gameManager.state == GameState.Win)
        {
            OpenWinMenu();
        }

        else if (gameManager.state == GameState.Failed)
        {
            OpenFailMenu();
        }
    }

    public void OpenCredits()
    {
        ChangeMenu(creditsMenu);
    }

    public void OpenHowToPlayScreen()
    {
        ChangeMenu(howToPlayMenu);
    }

    public void OpenMainMenu()
    {
        ChangeMenu(mainMenu);
        gameManager.ReloadScene();
    }

    public void OpenInGameMenu()
    {
        ChangeMenu(inGameMenu);
    }

    public void OpenPauseMenu()
    {
        ChangeMenu(pauseMenu);
    }

    private void OpenFailMenu()
    {
        ChangeMenu(failMenu);
        gameManager.StopTime();
    }

    private void OpenWinMenu()
    {
        ChangeMenu(winMenu);
        gameManager.StopTime();
    }

    public void UpdateUIOnPause(bool pause)
    {
        if (pause)
        {
            OpenPauseMenu();
        }

        else
        {
            OpenInGameMenu();
        }
    }

    private void ChangeMenu(GameObject newMenu)
    {
        if (currentMenu == null)
        {
            currentMenu = newMenu;
            newMenu.SetActive(true);
        }

        else
        {
            currentMenu.SetActive(false);
            newMenu.SetActive(true);
            currentMenu = newMenu;
        }
    }

    private void UpdateBossHealthBar()
    {
        bossHealthBar.fillAmount = boss.Health / 100;
    }
}