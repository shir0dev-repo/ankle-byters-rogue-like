using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public static bool IsPaused = false;
    public Button PauseButton;
    public Button ResumeButton;
    public Button TitleScreenButton;
    public GameObject MenuUI;
    public Slider musicSliderPaused;
    public Slider musicSlider;
    public Slider sfxSliderPaused;
    public Slider sfxSlider;
    public GameObject PauseButtonPanel;
    public GameObject GameOverPanel;

    private void Start()
    {
        PlayerManager.OnPlayerDeath += GameOver;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerDeath -= GameOver;
    }

    public void MusicVolumePaused()
    {
        AudioManager.Instance.MusicVoulume(musicSliderPaused.value);
        Debug.Log(musicSliderPaused.value);
    }
    public void MusicVolume()
    {
        AudioManager.Instance.MusicVoulume(musicSlider.value);
        Debug.Log(musicSlider.value);
    }
    public void SfxVolumePaused()
    {
        AudioManager.Instance.SfxVolume(sfxSliderPaused.value);
    }
    public void SfxVolume()
    {
        AudioManager.Instance.SfxVolume(sfxSlider.value);
    }

    public void Resume()
    {
        MenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    public void Pause()
    {
        MenuUI.SetActive(true);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene(0);
        MenuUI.SetActive(false);
        PauseButtonPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }

    private void GameOver()
    {
        Debug.Log("Game ober :(");
        SceneManager.LoadScene(2);
        PauseButtonPanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }
}
