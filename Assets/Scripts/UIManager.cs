using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] private GameObject restartMenu;

    private void Start()
    {
        currentMenu = mainMenu;
        gameManager.OnPauseStateChange += UpdateUIOnPause;
        gameManager.OnExitFromOtherMenus += OpenMainMenu;
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

    private void OpenRestartMenu()
    {
        ChangeMenu(restartMenu);
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
}