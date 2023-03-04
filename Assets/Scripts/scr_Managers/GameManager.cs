using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //scripts
    private UI_PauseMenu PauseMenuScript;
    private Manager_Console ConsoleScript;

    private void Awake()
    {
        PauseMenuScript = GetComponent<UI_PauseMenu>();
        ConsoleScript = GetComponent<Manager_Console>();
    }

    private void Start()
    {
        PauseMenuScript.UnpauseGame();
        //ConsoleScript.CloseConsole();
        ConsoleScript.par_Console.SetActive(false);
    }
}