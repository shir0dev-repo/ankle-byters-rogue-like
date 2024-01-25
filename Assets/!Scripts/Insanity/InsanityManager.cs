using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsanityManager : Singleton<InsanityManager>
{
    [SerializeField] private int _currentInsanity = 0;
    [SerializeField] private int _maxInsanity = 100;

    [SerializeField] private int _insanityIncrement;
    [SerializeField] private bool _incrementOverTime = false;

    public Action<int> OnInsanityChanged;

    private void OnEnable()
    {
        TimeTickSystem.OnTick_5 += AddInsanityRecursive;
    }

    private void OnDisable()
    {
        TimeTickSystem.OnTick_5 -= AddInsanityRecursive;
    }

    private void AddInsanityRecursive(object sender, TimeTickSystem.OnTickChangedArgs e)
    {
        if (_incrementOverTime == false) return;

        Debug.Log($"Increasing sanity at tick {e.CurrentTick} by {_insanityIncrement}.");
        AddInsanity(_insanityIncrement);
    }

    public void AddInsanity(int insanityAmount)
    {
        if (_currentInsanity + insanityAmount > _maxInsanity)
            _currentInsanity = _maxInsanity;
        else _currentInsanity += insanityAmount;

        OnInsanityChanged?.Invoke(_currentInsanity);
    }

    // use only for testing, will be removed once fully implemented
    public void SetInsanity(int amount)
    {
        _currentInsanity = amount;
        OnInsanityChanged?.Invoke(_currentInsanity);
    }
}