using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTickSystem : PersistentSingleton<TimeTickSystem>
{
    public class OnTickChangedArgs : EventArgs
    {
        public int CurrentTick;
    }

    private const float _TICK_TIMER_MAX = 0.2f;

    private int _tick;
    private float _tickTimer;

    public static event EventHandler<OnTickChangedArgs> OnTick; // every 10ms realtime (physics update)
    public static event EventHandler<OnTickChangedArgs> OnTick_5; // every second realtime
    public static event EventHandler<OnTickChangedArgs> OnTick_25; // every 5 seconds realtime


    protected override void Awake()
    {
        base.Awake();
        _tick = 0;
    }

    private void Update()
    {
        HandleTick();
    }
    public int GetTick() => _tick;

    private void HandleTick() 
    {
        _tickTimer += Time.deltaTime;
        if (_tickTimer >= _TICK_TIMER_MAX)
        {
            _tickTimer -= _TICK_TIMER_MAX;
            _tick++;
            
            OnTickChangedArgs args = new() { CurrentTick = _tick };

            OnTick?.Invoke(this, args);
            if (_tick % 5 == 0) OnTick_5?.Invoke(this, args);
            if (_tick % 25 == 0) OnTick_25?.Invoke(this, args);
        }
    }
}
