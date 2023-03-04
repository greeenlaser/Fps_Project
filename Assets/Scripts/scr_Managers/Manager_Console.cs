using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager_Console : MonoBehaviour
{
    [Header("UI")]
    public GameObject par_Console;

    //scripts
    private UI_PauseMenu PauseMenuScript;

    private void Awake()
    {
        PauseMenuScript = GetComponent<UI_PauseMenu>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PauseMenuScript.isConsoleOpen = !PauseMenuScript.isConsoleOpen;
        }

        if (PauseMenuScript.isConsoleOpen
            && !par_Console.activeInHierarchy)
        {
            OpenConsole();
        }
        else if (!PauseMenuScript.isConsoleOpen
                 && par_Console.activeInHierarchy)
        {
            CloseConsole();
        }
    }

    private void OpenConsole()
    {
        if (!PauseMenuScript.isPaused)
        {
            PauseMenuScript.PauseWithoutUI();
        }

        PauseMenuScript.isConsoleOpen = true;
        par_Console.SetActive(true);
    }
    public void CloseConsole()
    {
        PauseMenuScript.isConsoleOpen = false;
        par_Console.SetActive(true);

        if (!PauseMenuScript.par_PM.activeInHierarchy)
        {
            PauseMenuScript.UnpauseGame();
        }
    }
}