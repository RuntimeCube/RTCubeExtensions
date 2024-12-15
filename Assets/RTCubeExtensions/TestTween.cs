using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTween : MonoBehaviour
{
    private Vector3 _startScale;
    private void Awake() {
        _startScale = transform.localScale;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // TweenManager.TweenSpriteAlpha(gameObject, 1f, 0f, 0.75f);
            TweenManager.TweenScale(gameObject, _startScale, Vector3.one * 1.25f, 0.75f).SetEase(EaseType.ElasticEaseOut);
        }
    }
}
