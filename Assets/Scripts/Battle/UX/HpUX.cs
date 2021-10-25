using DigitalRuby.Tween;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HpUX : MonoBehaviour
{
    public float totalHp = 100f;
    public float currentHp = 100f;
    public float startValue = 0f;
    public float barHeight;
    public float barWidth;
    public Color color;
    // Start is called before the first frame update

    private Texture2D hpTexture;
    private Texture2D fullHpTexture;

    private Rect hpRect;
    private Rect fullHpRect;

    public Vector2 pos;
    public bool shouldDraw = false;

    public void Start()
    {
        hpRect = new Rect(pos,
                        new Vector2(ComputeHpRate() * barWidth / 100,barHeight)
                        );

        fullHpRect = new Rect(pos,
                        new Vector2(barWidth, barHeight)
                        );

        hpTexture = new Texture2D(1, 1);
        hpTexture.wrapMode = TextureWrapMode.Clamp;
        hpTexture.SetPixel(1, 1, color);
        hpTexture.Apply();

        fullHpTexture = new Texture2D(1, 1);
        fullHpTexture.wrapMode = TextureWrapMode.Clamp;
        fullHpTexture.SetPixel(1, 1, Color.grey);
        fullHpTexture.Apply();

    }

    public void OnGUI()
    {
        if (!shouldDraw)
            return;
        if (Event.current.type.Equals(EventType.Repaint))
        {
            Graphics.DrawTexture(fullHpRect, fullHpTexture);
            Graphics.DrawTexture(hpRect, hpTexture);
        }
    }

    public void OnValidate()
    {
        UpdateUX();
    }

    public void UpdateUX()
    {
        hpRect = new Rect(pos,
                        new Vector2(ComputeHpRate() * barWidth / 100, barHeight)
                        );

        fullHpRect = new Rect(pos,
                        new Vector2(barWidth, barHeight)
                        );
    }

    public void ChangeHp(int newHp)
    {
        currentHp = newHp;
        
        hpRect = new Rect(pos,
                        new Vector2(ComputeHpRate() * barWidth / 100, barHeight)
                        );
    }

    public void ChangeHp(int newHp, int maxHp)
    {
        totalHp = maxHp;
        ChangeHp(newHp);
    }

    public float ComputeHpRate()
    {
        return (currentHp - startValue) * 100 / (totalHp - startValue);
    }

    public void AnimateHp()
    {
        Action<ITween<float>> hpAnim = (t) => { };
    }

}
