using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsanityTimer : MonoBehaviour
{
    [SerializeField] public float maxTime = 5.0f;
    [SerializeField] GameObject player;
    public GameObject youDiedText;
    public Image timerImage;

    float timeRemaining;

    void Start()
    {
        timeRemaining = 0f;
    }

    void Update()
    {
        if (timeRemaining < maxTime)
        {
            timeRemaining = timeRemaining + Time.deltaTime;
            timerImage.fillAmount = timeRemaining / maxTime;
        }
        else
        {
            youDiedText.SetActive(true);
            Destroy(player);
        }
    }
}