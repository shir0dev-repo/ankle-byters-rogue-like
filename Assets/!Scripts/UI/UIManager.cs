using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static bool IsPaused = false;
    public Button PauseButton;
    public Button ResumeButton;
    public Button TitleScreenButton;
    public GameObject MenuUI;
    public Slider musicSlider;
    public Slider sfxSlider;

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVoulume(musicSlider.value);
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
        SceneManager.LoadScene("Title");
        MenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }
}
