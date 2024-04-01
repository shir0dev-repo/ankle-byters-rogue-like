using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainControllerPrefab;
    private void Awake()
    {
        if (MainController.Instance == null)
            Instantiate(_mainControllerPrefab);

        Destroy(gameObject);
    }
}
