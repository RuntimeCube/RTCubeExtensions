using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RTCube.Extensions;
using RTCube.Extensions.Tween;

public class TweenManager : MonoSingleton<TweenManager>
{
    private Dictionary<string, ITween> _activeTweens = new Dictionary<string, ITween>();

    private void Update()
    {
        foreach (var tween in _activeTweens.Values)
        {
            tween.Update();
        }
    }

    public void AddTween<T>(Tween<T> tween)
    {
        _activeTweens[tween.Identifier] = tween;
    }

    #region static helper methods
    public static Tween<float> TweenSpriteAlpha(GameObject gameObject, float startAlpha, float endAlpha, float duration)
    {
        var spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            throw new Exception("GameObject does not have a SpriteRenderer component");
        }
        string identifier = $"{spriteRenderer.GetInstanceID()}_Alpha";
        
        Tween<float> tween = new Tween<float>(gameObject, identifier, startAlpha, endAlpha, duration, value => 
        {
            Color color = spriteRenderer.color;
            color.a = value;
            spriteRenderer.color = color;
        });

        return tween;
    }

    public static Tween<Vector3> TweenScale(GameObject gameObject, Vector3 startScale, Vector3 endScale, float duration)
    {
        string identifier = $"{gameObject.transform.GetInstanceID()}_Scale";
        Transform transform = gameObject.transform;
        Tween<Vector3> tween = new Tween<Vector3>(gameObject, identifier, startScale, endScale, duration, value => 
        {
            transform.localScale = value;
        });

        return tween;
    }
    #endregion
}