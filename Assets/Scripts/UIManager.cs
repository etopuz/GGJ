using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject howToPlayMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject restartMenu;

    [SerializeField] private GameManager gameManager;

    private GameObject currentMenu;
    private void Start()
    {
        currentMenu = mainMenu;
        gameManager.OnStateChange += HandleMenus;
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
    }

    public void OpenPauseMenu()
    {
        ChangeMenu(pauseMenu);
    }

    private void OpenRestartMenu()
    {
        ChangeMenu(restartMenu);
    }

    public void HandleMenus(bool pause, bool dead)
    {
        if (dead)
        {
            OpenRestartMenu();
            return;
        }

        currentMenu.SetActive(pause);

        if (!pause)
        {
            currentMenu = pauseMenu;
        }
        
    }

    private void ChangeMenu(GameObject newMenu)
    {
        if(currentMenu == null)
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
