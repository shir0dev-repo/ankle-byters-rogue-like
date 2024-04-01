using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainController : PersistentSingleton<MainController>
{
    public UIManager UIManager { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        UIManager = GetComponentInChildren<UIManager>();
    }
    void Start()
    {

    }

    void Update()
    {

    }
}
