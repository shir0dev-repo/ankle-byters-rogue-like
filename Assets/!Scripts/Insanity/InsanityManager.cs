using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsanityManager : Singleton<InsanityManager>
{
    [SerializeField] private int _currentInsanity = 0;
    [SerializeField] private int _maxInsanity = 100;

    public Action<int> OnInsanityChanged;

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