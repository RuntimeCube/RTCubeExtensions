using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RTCube.Extensions.Tween;

public class Tween<T> : ITween
{
    private T _startValue;
    private T _endValue;
    private float _duration;
    private float _elapsedTime = 0f;
    private Action<T> _onTweenUpdate;

    private EaseType _easeType = EaseType.Linear;

    public Tween(object target, string identifier, T startValue, T endValue, float duration, Action<T> onTweenUpdate)
    {
        Target = target;
        Identifier = identifier;
        _startValue = startValue;
        _endValue = endValue;
        _duration = duration;
        _onTweenUpdate = onTweenUpdate;
        TweenManager.Instance.AddTween(this);
    }

    public object Target { get; private set; }
    public bool IsComplete { get; private set; }
    public bool WasKilled { get; private set; }
    public bool IsPaused { get; private set; }
    public bool IgnoreTimeScale { get; private set; }
    public string Identifier { get; private set; }
    public float DelayTime { get; private set; }
    public Action onComplete { get; set; }

    public void Update()
    {
        if (IsComplete)
        {
            return;
        }

        _elapsedTime += Time.deltaTime;
        float t = Mathf.Clamp01(_elapsedTime / _duration);
        float easedT = Ease(_easeType, t);
        T currentValue;

        currentValue = Interpolate(_startValue, _endValue, easedT);
        _onTweenUpdate?.Invoke(currentValue);

        if (_elapsedTime >= _duration)
        {
            IsComplete = true;
            onComplete?.Invoke();
        }
    }

    public T Interpolate(T start, T end, float t)
    {
        if (start is float startFloat && end is float endFloat)
        {
            return (T)(object)Mathf.LerpUnclamped(startFloat, endFloat, t);
        }
        if (start is Vector2 startVector2 && end is Vector2 endVector2)
        {
            return (T)(object)Vector2.LerpUnclamped(startVector2, endVector2, t);
        }
        if (start is Vector3 startVector3 && end is Vector3 endVector3)
        {
            return (T)(object)Vector3.LerpUnclamped(startVector3, endVector3, t);
        }
        if (start is Vector4 startVector4 && end is Vector4 endVector4)
        {
            return (T)(object)Vector4.LerpUnclamped(startVector4, endVector4, t);
        }
        if (start is Quaternion startQuaternion && end is Quaternion endQuaternion)
        {
            return (T)(object)Quaternion.LerpUnclamped(startQuaternion, endQuaternion, t);
        }
        if (start is Color startColor && end is Color endColor)
        {
            return (T)(object)Color.Lerp(startColor, endColor, t);
        }
        throw new NotImplementedException($"Interpolation for type {typeof(T)} is not implemented");
    }

    public bool IsTargetDestroyed()
    {
        return false;
    }

    public void OnCompleteKill()
    {
        
    }

    public void Pause()
    {
        
    }

    public void Resume()
    {
        
    }

    public void FullKill()
    {
        
    }

    public Tween<T> SetEase(EaseType easeType)
    {
        _easeType = easeType;
        return this;
    }

    #region Ease Calculations
    public static float Linear(float t)
    {
        return t;
    }

    public static float ExpoEaseIn(float t)
    {
        return (t == 0) ? 0 : Mathf.Pow(2, 10 * (t - 1));
    }

    public static float ExpoEaseOut(float t)
    {
        return (t == 1) ? 1 : 1 - Mathf.Pow(2, -10 * t);
    }

    public static float ExpoEaseInOut(float t)
    {
        if (t == 0) return 0;
        if (t == 1) return 1;
        if (t < 0.5) return 0.5f * Mathf.Pow(2, 20 * t - 10);
        return 1 - 0.5f * Mathf.Pow(2, -20 * t + 10);
    }

    public static float ExpoEaseOutIn(float t)
    {
        return (t < 0.5) ? ExpoEaseOut(t * 2) / 2 : (ExpoEaseIn(t * 2 - 1) + 1) / 2;
    }

    public static float CircEaseIn(float t)
    {
        return 1 - Mathf.Sqrt(1 - t * t);
    }

    public static float CircEaseOut(float t)
    {
        return Mathf.Sqrt(1 - (t - 1) * (t - 1));
    }

    public static float CircEaseInOut(float t)
    {
        if (t < 0.5) return CircEaseIn(t * 2) / 2;
        return (CircEaseOut(t * 2 - 1) + 1) / 2;
    }

    public static float CircEaseOutIn(float t)
    {
        return (t < 0.5) ? CircEaseOut(t * 2) / 2 : (CircEaseIn(t * 2 - 1) + 1) / 2;
    }

    public static float QuadEaseIn(float t)
    {
        return t * t;
    }

    public static float QuadEaseOut(float t)
    {
        return t * (2 - t);
    }

    public static float QuadEaseInOut(float t)
    {
        return (t < 0.5) ? QuadEaseIn(t * 2) / 2 : (QuadEaseOut(t * 2 - 1) + 1) / 2;
    }

    public static float QuadEaseOutIn(float t)
    {
        return (t < 0.5) ? QuadEaseOut(t * 2) / 2 : (QuadEaseIn(t * 2 - 1) + 1) / 2;
    }

    public static float SineEaseIn(float t)
    {
        return 1 - Mathf.Cos(t * Mathf.PI / 2);
    }

    public static float SineEaseOut(float t)
    {
        return Mathf.Sin(t * Mathf.PI / 2);
    }

    public static float SineEaseInOut(float t)
    {
        return (t < 0.5) ? SineEaseIn(t * 2) / 2 : (SineEaseOut(t * 2 - 1) + 1) / 2;
    }

    public static float SineEaseOutIn(float t)
    {
        return (t < 0.5) ? SineEaseOut(t * 2) / 2 : (SineEaseIn(t * 2 - 1) + 1) / 2;
    }

    public static float CubicEaseIn(float t)
    {
        return t * t * t;
    }

    public static float CubicEaseOut(float t)
    {
        return 1 - Mathf.Pow(1 - t, 3);
    }

    public static float CubicEaseInOut(float t)
    {
        return (t < 0.5) ? CubicEaseIn(t * 2) / 2 : (CubicEaseOut(t * 2 - 1) + 1) / 2;
    }

    public static float CubicEaseOutIn(float t)
    {
        return (t < 0.5) ? CubicEaseOut(t * 2) / 2 : (CubicEaseIn(t * 2 - 1) + 1) / 2;
    }
    
    public static float QuartEaseIn(float t)
    {
        return t * t * t * t;
    }

    public static float QuartEaseOut(float t)
    {
        return 1 - Mathf.Pow(1 - t, 4);
    }

    public static float QuartEaseInOut(float t)
    {
        return (t < 0.5) ? QuartEaseIn(t * 2) / 2 : (QuartEaseOut(t * 2 - 1) + 1) / 2;
    }

    public static float QuartEaseOutIn(float t)
    {
        return (t < 0.5) ? QuartEaseOut(t * 2) / 2 : (QuartEaseIn(t * 2 - 1) + 1) / 2;
    }

    public static float QuintEaseIn(float t)
    {
        return t * t * t * t * t;
    }

    public static float QuintEaseOut(float t)
    {
        return 1 - Mathf.Pow(1 - t, 5);
    }

    public static float QuintEaseInOut(float t)
    {
        return (t < 0.5) ? QuintEaseIn(t * 2) / 2 : (QuintEaseOut(t * 2 - 1) + 1) / 2;
    }

    public static float QuintEaseOutIn(float t)
    {
        return (t < 0.5) ? QuintEaseOut(t * 2) / 2 : (QuintEaseIn(t * 2 - 1) + 1) / 2;
    }

    public static float ElasticEaseIn(float t)
    {
        return t == 0 ? 0 : (t == 1 ? 1 : -Mathf.Pow(2, 10 * (t - 1)) * Mathf.Sin((t * 10 - 10.75f) * ((2 * Mathf.PI) / 3)));
    }

    public static float ElasticEaseOut(float t)
    {
        return t == 0 ? 0 : (t == 1 ? 1 : Mathf.Pow(2, -10 * t) * Mathf.Sin((t * 10 - 0.75f) * ((2 * Mathf.PI) / 3)) + 1);
    }

    public static float ElasticEaseInOut(float t)
    {
        if (t == 0) return 0;
        if (t == 1) return 1;
        return (t < 0.5) ? ElasticEaseIn(t * 2) / 2 : (ElasticEaseOut(t * 2 - 1) + 1) / 2;
    }

    public static float ElasticEaseOutIn(float t)
    {
        return (t < 0.5) ? ElasticEaseOut(t * 2) / 2 : (ElasticEaseIn(t * 2 - 1) + 1) / 2;
    }

    public static float BounceEaseIn(float t)
    {
        return 1 - BounceEaseOut(1 - t);
    }

    public static float BounceEaseOut(float t)
    {
        if (t < 1 / 2.75f)
        {
            return 7.5625f * t * t;
        }
        else if (t < 2 / 2.75f)
        {
            return 7.5625f * (t -= 1.5f / 2.75f) * t + 0.75f;
        }
        else if (t < 2.5 / 2.75f)
        {
            return 7.5625f * (t -= 2.25f / 2.75f) * t + 0.9375f;
        }
        else
        {
            return 7.5625f * (t -= 2.625f / 2.75f) * t + 0.984375f;
        }
    }

    public static float BounceEaseInOut(float t)
    {
        return (t < 0.5) ? BounceEaseIn(t * 2) / 2 : (BounceEaseOut(t * 2 - 1) + 1) / 2;
    }

    public static float BounceEaseOutIn(float t)
    {
        return (t < 0.5) ? BounceEaseOut(t * 2) / 2 : (BounceEaseIn(t * 2 - 1) + 1) / 2;
    }

    public static float BackEaseIn(float t)
    {
        return t * t * (2.70158f * t - 1.70158f);
    }

    public static float BackEaseOut(float t)
    {
        return (t -= 1) * t * (2.70158f * t + 1.70158f) + 1;
    }

    public static float BackEaseInOut(float t)
    {
        return (t < 0.5) ? BackEaseIn(t * 2) / 2 : (BackEaseOut(t * 2 - 1) + 1) / 2;
    }

    public static float BackEaseOutIn(float t)
    {
        return (t < 0.5) ? BackEaseOut(t * 2) / 2 : (BackEaseIn(t * 2 - 1) + 1) / 2;
    }
    #endregion 

    #region Ease enum and Helper
    public static float Ease(EaseType easeType, float t)
    {
        return easeType switch
        {
            EaseType.Linear => Linear(t),
            EaseType.ExpoEaseIn => ExpoEaseIn(t),
            EaseType.ExpoEaseOut => ExpoEaseOut(t),
            EaseType.ExpoEaseInOut => ExpoEaseInOut(t),
            EaseType.ExpoEaseOutIn => ExpoEaseOutIn(t),
            EaseType.CircEaseIn => CircEaseIn(t),
            EaseType.CircEaseOut => CircEaseOut(t),
            EaseType.CircEaseInOut => CircEaseInOut(t),
            EaseType.CircEaseOutIn => CircEaseOutIn(t),
            EaseType.QuadEaseIn => QuadEaseIn(t),
            EaseType.QuadEaseOut => QuadEaseOut(t),
            EaseType.QuadEaseInOut => QuadEaseInOut(t),
            EaseType.QuadEaseOutIn => QuadEaseOutIn(t),
            EaseType.SineEaseIn => SineEaseIn(t),
            EaseType.SineEaseOut => SineEaseOut(t),
            EaseType.SineEaseInOut => SineEaseInOut(t),
            EaseType.SineEaseOutIn => SineEaseOutIn(t),
            EaseType.CubicEaseIn => CubicEaseIn(t),
            EaseType.CubicEaseOut => CubicEaseOut(t),
            EaseType.CubicEaseInOut => CubicEaseInOut(t),
            EaseType.CubicEaseOutIn => CubicEaseOutIn(t),
            EaseType.QuartEaseIn => QuartEaseIn(t),
            EaseType.QuartEaseOut => QuartEaseOut(t),
            EaseType.QuartEaseInOut => QuartEaseInOut(t),
            EaseType.QuartEaseOutIn => QuartEaseOutIn(t),
            EaseType.QuintEaseIn => QuintEaseIn(t),
            EaseType.QuintEaseOut => QuintEaseOut(t),
            EaseType.QuintEaseInOut => QuintEaseInOut(t),
            EaseType.QuintEaseOutIn => QuintEaseOutIn(t),
            EaseType.ElasticEaseIn => ElasticEaseIn(t),
            EaseType.ElasticEaseOut => ElasticEaseOut(t),
            EaseType.ElasticEaseInOut => ElasticEaseInOut(t),
            EaseType.ElasticEaseOutIn => ElasticEaseOutIn(t),
            EaseType.BounceEaseIn => BounceEaseIn(t),
            EaseType.BounceEaseOut => BounceEaseOut(t),
            EaseType.BounceEaseInOut => BounceEaseInOut(t),
            EaseType.BounceEaseOutIn => BounceEaseOutIn(t),
            EaseType.BackEaseIn => BackEaseIn(t),
            EaseType.BackEaseOut => BackEaseOut(t),
            EaseType.BackEaseInOut => BackEaseInOut(t),
            EaseType.BackEaseOutIn => BackEaseOutIn(t),
        };
    }
    #endregion
}


public enum EaseType
{
    Linear,
    ExpoEaseIn,
    ExpoEaseOut,
    ExpoEaseInOut,
    ExpoEaseOutIn,
    CircEaseIn,
    CircEaseOut,
    CircEaseInOut,
    CircEaseOutIn,
    QuadEaseIn,
    QuadEaseOut,
    QuadEaseInOut,
    QuadEaseOutIn,
    SineEaseIn,
    SineEaseOut,
    SineEaseInOut,
    SineEaseOutIn,
    CubicEaseIn,
    CubicEaseOut,
    CubicEaseInOut,
    CubicEaseOutIn,
    QuartEaseIn,
    QuartEaseOut,
    QuartEaseInOut,
    QuartEaseOutIn,
    QuintEaseIn,
    QuintEaseOut,
    QuintEaseInOut,
    QuintEaseOutIn,
    ElasticEaseIn,
    ElasticEaseOut,
    ElasticEaseInOut,
    ElasticEaseOutIn,
    BounceEaseIn,
    BounceEaseOut,
    BounceEaseInOut,
    BounceEaseOutIn,
    BackEaseIn,
    BackEaseOut,
    BackEaseInOut,
    BackEaseOutIn,
}