using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UxBar : MonoBehaviour
{
    public Vector2 offset;
    public float maxValue = 100f;
    public float currentValue = 100f;
    public float startValue = 0f;
    public float barHeight;
    public float barWidth;
    public Color color;
    // Start is called before the first frame update

    private Texture2D currentTexture;
    private Texture2D fullBarTexture;

    private Rect currentRect;
    private Rect fullBarRect;

    public Vector2 pos;
    public bool shouldDraw = false;

    public void Start()
    {
        currentRect = new Rect(pos + offset,
                        new Vector2(ComputeCurrentRate() * barWidth / 100,barHeight)
                        );

        fullBarRect = new Rect(pos + offset,
                        new Vector2(barWidth, barHeight)
                        );


        currentTexture = new Texture2D(1, 1);
        currentTexture.wrapMode = TextureWrapMode.Clamp;
        currentTexture.SetPixel(1, 1, color);
        currentTexture.Apply();

        fullBarTexture = new Texture2D(1, 1);
        fullBarTexture.wrapMode = TextureWrapMode.Clamp;
        fullBarTexture.SetPixel(1, 1, Color.grey);
        fullBarTexture.Apply();

    }

    public void OnGUI()
    {
        if (!shouldDraw)
            return;
        if (Event.current.type.Equals(EventType.Repaint))
        {
            Graphics.DrawTexture(fullBarRect, fullBarTexture);
            Graphics.DrawTexture(currentRect, currentTexture);
        }
    }

    public void OnValidate()
    {
        fullBarTexture = new Texture2D(1, 1);
        fullBarTexture.wrapMode = TextureWrapMode.Clamp;
        fullBarTexture.SetPixel(1, 1, Color.grey);
        fullBarTexture.Apply();

        currentTexture = new Texture2D(1, 1);
        currentTexture.wrapMode = TextureWrapMode.Clamp;
        currentTexture.SetPixel(1, 1, color);
        currentTexture.Apply();

        UpdateUX();
    }

    public void UpdateUX()
    {
        currentRect = new Rect(pos + offset,
                        new Vector2(ComputeCurrentRate() * barWidth / 100, barHeight)
                        );

        fullBarRect = new Rect(pos + offset,
                        new Vector2(barWidth, barHeight)
                        );

    }

    public void ChangeCurrent(int currentValue)
    {
        this.currentValue = currentValue;
        
        currentRect = new Rect(pos + offset,
                        new Vector2(ComputeCurrentRate() * barWidth / 100, barHeight)
                        );
    }

    public void ChangeValues(int currentValue, int maxValue)
    {
        this.maxValue = maxValue;
        ChangeCurrent(currentValue);
    }

    public float ComputeCurrentRate()
    {
        return (currentValue - startValue) * 100 / (maxValue - startValue);
    }

    public void FullReset(float start, float end, float current)
    {
        startValue = start;
        maxValue = end;
        currentValue = current;
    }

}
