using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_PauseMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject par_PM;
    [SerializeField] private GameObject par_PMContent;
    [SerializeField] private GameObject par_SettingsContent;
    [SerializeField] private Button btn_ReturnToGame;
    [SerializeField] private Button btn_ShowSettings;
    [SerializeField] private Button btn_ReturnToPM;
    [SerializeField] private Button btn_ReturnToMM;
    [SerializeField] private Button btn_Quit;

    //public but hidden variables
    [HideInInspector] public bool isPaused;

    private void Start()
    {
        btn_ReturnToGame.onClick.AddListener(UnpauseGame);
        btn_ShowSettings.onClick.AddListener(ShowSettings);
        btn_ReturnToPM.onClick.AddListener(ReturnToPauseMenu);
        btn_ReturnToMM.onClick.AddListener(ReturnToMainMenu);
        btn_Quit.onClick.AddListener(Quit);

        UnpauseGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused
            && !par_PM.activeInHierarchy)
        {
            PauseWithUI();
        }
        else if (!isPaused
                 && par_PM.activeInHierarchy)
        {
            UnpauseGame();
        }
    }

    public void UnpauseGame()
    {
        isPaused = false;

        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        par_PM.SetActive(false);
        par_PMContent.SetActive(false);
        par_SettingsContent.SetActive(false);
    }
    public void PauseWithoutUI()
    {
        isPaused = true;

        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void PauseWithUI()
    {
        PauseWithoutUI();

        par_PM.SetActive(true);
        par_PMContent.SetActive(true);
        par_SettingsContent.SetActive(false);
    }

    public void ShowSettings()
    {
        par_PMContent.SetActive(false);
        par_SettingsContent.SetActive(true);
    }
    public void ReturnToPauseMenu()
    {
        par_PMContent.SetActive(true);
        par_SettingsContent.SetActive(false);
    }
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Quit()
    {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }
}