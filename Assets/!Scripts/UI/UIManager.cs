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
    public Slider musicSliderPaused;
    public Slider musicSlider;
    public Slider sfxSliderPaused;
    public Slider sfxSlider;

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
        SceneManager.LoadScene("TitleScene");
        MenuUI.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }
}