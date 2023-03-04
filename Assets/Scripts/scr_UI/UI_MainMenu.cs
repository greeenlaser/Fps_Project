using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class UI_MainMenu : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject par_MMContent;
    [SerializeField] private GameObject par_SettingsContent;
    [SerializeField] private Button btn_Play;
    [SerializeField] private Button btn_ShowSettings;
    [SerializeField] private Button btn_ReturnToMM;
    [SerializeField] private Button btn_Quit;

    private void Awake()
    {
        btn_Play.onClick.AddListener(Play);
        btn_ShowSettings.onClick.AddListener(ShowSettings);
        btn_ReturnToMM.onClick.AddListener(ShowMainMenu);
        btn_Quit.onClick.AddListener(Quit);

        ShowMainMenu();
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void ShowMainMenu()
    {
        par_MMContent.SetActive(true);
        par_SettingsContent.SetActive(false);
    }
    public void ShowSettings()
    {
        par_MMContent.SetActive(false);
        par_SettingsContent.SetActive(true);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif

#if UNITY_STANDALONE
        Application.Quit();
#endif
    }
}