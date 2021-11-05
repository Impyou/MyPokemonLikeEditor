using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class UxBar : MonoBehaviour
{
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

    public bool shouldDraw = false;
    Vector2 pos;

    public void Start()
    {
        UpdateData();
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

    public Vector2 GetScreenPos()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        screenPos.y = Screen.height - screenPos.y;
        return screenPos ;
    }

    public void UpdateData()
    {
        pos = GetScreenPos();
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
        currentRect = new Rect(pos,
                        new Vector2(ComputeCurrentRate() * barWidth / 100, barHeight)
                        );

        fullBarRect = new Rect(pos,
                        new Vector2(barWidth, barHeight)
                        );

    }

    public void ChangeCurrent(int currentValue)
    {
        this.currentValue = currentValue;
        
        currentRect = new Rect(pos,
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
