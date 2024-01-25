using UnityEngine;

public enum VectorEasingMode
{
    Linear,

    EaseInSine,
    EaseOutSine,
    EaseInOutSine,

    EaseInCubic,
    EaseOutCubic,
    EaseInOutCubic,

    EaseInExpo,
    EaseOutExpo,
    EaseInOutExpo,

    EaseInCirc,
    EaseOutCirc,
    EaseInOutCirc,

    EaseInBack,
    EaseOutBack,
    EaseInOutBack
}

public static class VectorEasing
{
    /// <summary>
    /// Evaluates a position based on a zero-to-one value.
    /// </summary>
    /// <param name="mode">The easing function to use.</param>
    /// <param name="startPosition">The initial position when t=0.</param>
    /// <param name="endPosition">The final position when t=1.</param>
    /// <param name="time">The point at which the easing function should be evaluated, clamped between zero and one.</param>
    /// <returns>A point between the start and end positions, interpolated by the easing mode, at a value of time.</returns>
    public static Vector3 Evaluate(this VectorEasingMode mode, Vector3 startPosition, Vector3 endPosition, float time)
    {
        time = Mathf.Clamp01(time);

        float lerp = mode switch
        {
            VectorEasingMode.Linear => time,

            VectorEasingMode.EaseInSine => EaseInSine(time),
            VectorEasingMode.EaseOutSine => EaseOutSine(time),
            VectorEasingMode.EaseInOutSine => EaseInOutSine(time),

            VectorEasingMode.EaseInCubic => EaseInCubic(time),
            VectorEasingMode.EaseOutCubic => EaseOutCubic(time),
            VectorEasingMode.EaseInOutCubic => EaseInOutCubic(time),

            VectorEasingMode.EaseInExpo => EaseInExpo(time),
            VectorEasingMode.EaseOutExpo => EaseOutExpo(time),
            VectorEasingMode.EaseInOutExpo => EaseInOutExpo(time),

            VectorEasingMode.EaseInCirc => EaseInCirc(time),
            VectorEasingMode.EaseOutCirc => EaseOutCirc(time),
            VectorEasingMode.EaseInOutCirc => EaseInOutCirc(time),

            VectorEasingMode.EaseInBack => EaseInBack(time),
            VectorEasingMode.EaseOutBack => EaseOutBack(time),
            VectorEasingMode.EaseInOutBack => EaseInOutBack(time),

            _ => time
        };

        return startPosition + Vector3.Lerp(Vector3.zero, endPosition, lerp);
    }
    private static float EaseInSine(float time)
    {
        return 1 - Mathf.Cos((time * Mathf.PI) / 2f);
        
    }
    private static float EaseOutSine(float time)
    {
        return Mathf.Sin((time * Mathf.PI) / 2f);
        
    }
    private static float EaseInOutSine(float time)
    {
        return -(Mathf.Cos(time * Mathf.PI) - 1) / 2f;
        
    }
    private static float EaseInCubic(float time)
    {
        return Mathf.Pow(time, 3);
        
    }
    private static float EaseOutCubic(float time)
    {
        return 1 - Mathf.Pow(1 - time, 3);
        
    }
    private static float EaseInOutCubic(float time)
    {
        return time < 0.5f ? 4 * Mathf.Pow(time, 3) : 1.0f - Mathf.Pow(-2.0f * time + 2.0f, 3.0f) / 2.0f;
        
    }
    private static float EaseInExpo(float time)
    {
        return time == 0 ? 0 : Mathf.Pow(2.0f, 10.0f * time - 10.0f);
        
    }
    private static float EaseOutExpo(float time)
    {
        return time == 1 ? 1 : 1.0f - Mathf.Pow(2.0f, -10.0f * time);
        
    }
    private static float EaseInOutExpo(float time)
    {
        return time switch
        {
            >= 0.5f => (2.0f - Mathf.Pow(2.0f, -20.0f * time + 10.0f)) / 2.0f,
            < 0.5f and not 0 => Mathf.Pow(2.0f, 20.0f * time - 10.0f) / 2.0f,
            0 => 0,
            _ => 1,
        };
        
    }
    private static float EaseInCirc(float time)
    {
        return 1 - Mathf.Sqrt(1 - (time * time));
        
    }
    private static float EaseOutCirc(float time)
    {
        return Mathf.Sqrt(1 - Mathf.Pow(time - 1, 2));
        
    }
    private static float EaseInOutCirc(float time)
    {
        float lerp;
        if (time < 0.5f)
            lerp = (1 - Mathf.Sqrt(1.0f - Mathf.Pow(2.0f * time, 2.0f))) / 2f;
        else
            lerp = (Mathf.Sqrt(1.0f - Mathf.Pow(2.0f * time + 2.0f, 2.0f)) + 1.0f) / 2.0f;

        return lerp;
    }
    private static float EaseInBack(float time)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1.0f;
        return c3 * Mathf.Pow(time, 3.0f) - c1 * time * time;
        
    }
    private static float EaseOutBack(float time)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1.0f;
        return 1.0f + c3 * Mathf.Pow(time - 1.0f, 3.0f) + c1 * Mathf.Pow(time - 1, 2.0f);
    }
    private static float EaseInOutBack(float time)
    {
        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;
        float lerp;

        if (time < 0.5f)
            lerp = Mathf.Pow(2.0f * time, 2.0f) * ((c2 + 1.0f) * 2 * time - c2) / 2.0f;
        else
            lerp = Mathf.Pow(2.0f * time - 2.0f, 2.0f) * ((c2 + 1.0f) * (time * 2.0f - 2.0f) + c2) + 2.0f / 2.0f;

        return lerp;
    }
}
