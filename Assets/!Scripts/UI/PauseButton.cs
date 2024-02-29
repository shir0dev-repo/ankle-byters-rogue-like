using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{

    public GameObject PasueButton;

    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "TitleScene")
        {
            PasueButton.SetActive(false);
        }
        else if (currentScene.name == "GameScene")
        {
            PasueButton.SetActive(true);
        }
    }
}
