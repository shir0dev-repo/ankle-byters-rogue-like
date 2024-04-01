using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _titleCanvas;
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private GameObject _inGameSettingsObject;
    [SerializeField] private GameObject _titleSettingsObject;
    [SerializeField] private GameObject _gameOverCanvas;

    public void SetUI(bool title, bool settings, bool gameOver)
    {
        DisplayTitleMenu(title);
        DisplaySettingsMenu(settings);
        DisplayGameOverMenu(gameOver);
    }

    public void DisplayTitleMenu(bool toggle)
    {
        _titleCanvas.SetActive(toggle);
    }

    public void DisplaySettingsMenu(bool currentlyPaused)
    {
        bool inGame = GameManager.Instance.InGame;
        _inGameSettingsObject.SetActive(inGame);
        _titleSettingsObject.SetActive(!inGame);

        _settingsCanvas.SetActive(currentlyPaused);
    }

    public void DisplayGameOverMenu(bool toggle)
    {
        _gameOverCanvas.SetActive(toggle);
    }
}
