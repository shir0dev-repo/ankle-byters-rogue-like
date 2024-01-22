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
    public static Vector3 Get(this VectorEasingMode mode, Vector3 startPosition, Vector3 endPosition, float time)
    {
        Vector3 lerpVector = mode switch
        {
            VectorEasingMode.Linear => Vector3.Lerp(Vector3.zero, endPosition, time),

            VectorEasingMode.EaseInSine => EaseInSine(endPosition, time),
            VectorEasingMode.EaseOutSine => EaseOutSine(endPosition, time),
            VectorEasingMode.EaseInOutSine => EaseInOutSine(endPosition, time),

            VectorEasingMode.EaseInCubic => EaseInCubic(endPosition, time),
            VectorEasingMode.EaseOutCubic => EaseOutCubic(endPosition, time),
            VectorEasingMode.EaseInOutCubic => EaseInOutCubic(endPosition, time),

            VectorEasingMode.EaseInExpo => EaseInExpo(endPosition, time),
            VectorEasingMode.EaseOutExpo => EaseOutExpo(endPosition, time),
            VectorEasingMode.EaseInOutExpo => EaseInOutExpo(endPosition, time),

            VectorEasingMode.EaseInCirc => EaseInCirc(endPosition, time),
            VectorEasingMode.EaseOutCirc => EaseOutCirc(endPosition, time),
            VectorEasingMode.EaseInOutCirc => EaseInOutCirc(endPosition, time),

            VectorEasingMode.EaseInBack => EaseInBack(endPosition, time),
            VectorEasingMode.EaseOutBack => EaseOutBack(endPosition, time),
            VectorEasingMode.EaseInOutBack => EaseInOutBack(endPosition, time),

            _ => Vector3.Lerp(Vector3.zero, endPosition, time)
        };

        return startPosition + lerpVector;
    }

    private static Vector3 EaseInSine(Vector3 endPosition, float time)
    {
        float lerp = 1 - Mathf.Cos((time * Mathf.PI) / 2f);
        return Vector3.Lerp(Vector3.zero, endPosition, lerp);
    }

    private static Vector3 EaseOutSine(Vector3 endPosition, float time)
    {
        float lerp = Mathf.Sin((time * Mathf.PI) / 2f);
        return Vector3.Lerp(Vector3.zero, endPosition, lerp);
    }

    private static Vector3 EaseInOutSine(Vector3 endPosition, float time)
    {
        float lerp = -(Mathf.Cos(time * Mathf.PI) - 1) / 2f;
        return Vector3.Lerp(Vector3.zero, endPosition, lerp);
    }

    private static Vector3 EaseInCubic(Vector3 endPosition, float time)
    {
        float lerp = Mathf.Pow(time, 3);
        return Vector3.Lerp(Vector3.zero, endPosition, lerp);
    }

    private static Vector3 EaseOutCubic(Vector3 endPosition, float time)
    {
        float lerp = 1 - Mathf.Pow(1 - time, 3);
        return Vector3.Lerp(Vector3.zero, endPosition, lerp);
    }

    private static Vector3 EaseInOutCubic(Vector3 endPosition, float time)
    {
        float lerp = time < 0.5f ? 4 * Mathf.Pow(time, 3) : 1.0f - Mathf.Pow(-2.0f * time + 2.0f, 3.0f) / 2.0f;
        return Vector3.Lerp(Vector3.zero, endPosition, lerp);
    }

    private static Vector3 EaseInExpo(Vector3 endPosition, float time)
    {
        float lerp = time == 0 ? 0 : Mathf.Pow(2.0f, 10.0f * time - 10.0f);
        return Vector3.Lerp(Vector3.zero, endPosition, lerp);
    }

    private static Vector3 EaseOutExpo(Vector3 endPosition, float time)
    {
        float lerp = time == 1 ? 1 : 1.0f - Mathf.Pow(2.0f, -10.0f * time);
        return Vector3.Lerp(Vector3.zero, endPosition, lerp);
    }

    private static Vector3 EaseInOutExpo(Vector3 endPosition, float time)
    {
        float lerp = time switch
        {
            >= 0.5f => (2.0f - Mathf.Pow(2.0f, -20.0f * time + 10.0f)) / 2.0f,
            < 0.5f and not 0 => Mathf.Pow(2.0f, 20.0f * time - 10.0f) / 2.0f,
            0 => 0,
            _ => 1,
        };
        return Vector3.Lerp(Vector3.zero, endPosition, lerp);
    }

    private static Vector3 EaseInCirc(Vector3 endPosition, float time)
    {
        float lerp = 1 - Mathf.Sqrt(1 - time * time);
        return Vector3.Lerp(Vector3.zero, endPosition, lerp);
    }

    private static Vector3 EaseOutCirc(Vector3 endPosition, float time)
    {
        float lerp = Mathf.Sqrt(1 - Mathf.Pow(time - 1, 2));
        return Vector3.Lerp(Vector3.zero, endPosition, lerp);
    }

    private static Vector3 EaseInOutCirc(Vector3 endPosition, float time)
    {
        float lerp;
        if (time < 0.5f)
            lerp = (1 - Mathf.Sqrt(1.0f - Mathf.Pow(2.0f * time, 2.0f))) / 2f;
        else
            lerp = (Mathf.Sqrt(1.0f - Mathf.Pow(2.0f * time + 2.0f, 2.0f)) + 1.0f) / 2.0f;
        
        return Vector3.Lerp(Vector3.zero, endPosition, lerp);
    }

    private static Vector3 EaseInBack(Vector3 endPosition, float time)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1.0f;
        float lerp = c3 * Mathf.Pow(time, 3.0f) - c1 * time * time;
        return Vector3.Lerp(Vector3.zero, endPosition, lerp);
    }

    private static Vector3 EaseOutBack(Vector3 endPosition, float time)
    {
        const float c1 = 1.70158f;
        const float c3 = c1 + 1.0f;
        float lerp = 1.0f + c3 * Mathf.Pow(time - 1.0f, 3.0f) + c1 * Mathf.Pow(time - 1, 2.0f);
        return Vector3.Lerp(Vector3.zero, endPosition, lerp);
    }

    private static Vector3 EaseInOutBack(Vector3 endPosition, float time)
    {
        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;
        float lerp;

        if (time < 0.5f)
            lerp = Mathf.Pow(2.0f * time, 2.0f) * ((c2 + 1.0f) * 2 * time - c2) / 2.0f;
        else
            lerp = Mathf.Pow(2.0f * time - 2.0f, 2.0f) * ((c2 + 1.0f) * (time * 2.0f - 2.0f) + c2) + 2.0f / 2.0f;

        return Vector3.Lerp(Vector3.zero, endPosition, lerp);
    }
}
