using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AnimationData : ScriptableObject
{
    public enum InterpolationFunction { LINEAR, CUBIC_EASE_OUT };

    public Vector2 start_pos;
    public Vector2 end_pos;
    public float duration;
    public InterpolationFunction interpolationFunction;

    public Func<float, float> GetInterpolationFunction()
    {
        switch(interpolationFunction)
        {
            case InterpolationFunction.LINEAR:
                return TweenScaleFunctions.Linear;
            case InterpolationFunction.CUBIC_EASE_OUT:
                return TweenScaleFunctions.CubicEaseOut;
        }
        return null;
    }

    public void GenerateTween(GameObject goRef, string key, Action<ITween<Vector2>> animationCallback, Action<ITween<Vector2>> callback = null)
    {
        var tween = goRef.gameObject.Tween(key, start_pos, end_pos, duration, GetInterpolationFunction(), animationCallback, callback);
        tween.ForceUpdate = true;
    }
}
