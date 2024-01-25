using System;
using UnityEngine;

public class TimeTickSystem
{
    public const float TICK_TIMER_MAX = 0.2f;
    private int _tick;
    
    public class OnTickArgs : EventArgs
    {
        public int CurrentTick;
    }

    public static event EventHandler<OnTickArgs> OnTick; // every 10ms realtime (physics update)
    public static event EventHandler<OnTickArgs> OnTick_5; // every second realtime
    public static event EventHandler<OnTickArgs> OnTick_25; // every 5 seconds realtime

    public int GetTick() => _tick;

    public void IncrementTick()
    {
        _tick++;

        OnTickArgs args = new() { CurrentTick = _tick };

        OnTick?.Invoke(this, args);
        if (_tick % 5 == 0) OnTick_5?.Invoke(this, args);
        if (_tick % 25 == 0) OnTick_25?.Invoke(this, args);
    }
}

public class TimeTickManager : Singleton<TimeTickManager>
{
    private float _tickTimer;
    TimeTickSystem _system;

    public int GetTick() => _system == null ? 0 : _system.GetTick();

    public void Create()
    {
        _system ??= new TimeTickSystem();
    }

    private void Update()
    {
        if (_system == null) return;

        _tickTimer += Time.deltaTime;

        if (_tickTimer >= TimeTickSystem.TICK_TIMER_MAX)
        {
            _tickTimer -= TimeTickSystem.TICK_TIMER_MAX;
            _system.IncrementTick();
        }
    }
}
