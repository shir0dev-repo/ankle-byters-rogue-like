using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEditor;


public class StartGame : MonoBehaviour
{
    public static bool SettingsClicked = false;
    public Button startButton;
    public Button SettingsButton;
    public GameObject SettingsUI;
    public GameObject PauseButtonPanel;

    private void Start()
    {
        PauseButtonPanel.SetActive(false);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("RoomImplTest");
        PauseButtonPanel.SetActive(true);
    }

    public void Settings()
    {
        if (SettingsClicked == false)
        {
        SettingsUI.SetActive(true);
        SettingsClicked = true;
        }
        else
        {
            SettingsUI.SetActive(false);
            SettingsClicked = false;
        }
    }
}
