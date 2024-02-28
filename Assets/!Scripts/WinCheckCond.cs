using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCheckCond : MonoBehaviour
{
    public GameObject boss;
    [SerializeField] GameObject winImage;

    public BossMovement bossMovement;
    public EnemySpawner enemySpawner;

    private void Start()
    {
        winImage.SetActive(false);
    }

    private void Update()
    {
        if (boss == null && enemySpawner.bossSpawned)
        {
            winImage.SetActive(true);
            Debug.Log("The PREFABBBBBBB");
            Debug.Log(boss);
        }
    }
}
