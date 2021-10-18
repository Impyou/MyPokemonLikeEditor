using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HpUX : MonoBehaviour
{
    public int totalHp = 100;
    public int currentHp = 100;
    public float barHeight;
    public float barWidth;
    // Start is called before the first frame update

    private Texture2D hpTexture;
    private Texture2D fullHpTexture;

    private Rect hpRect;
    private Rect fullHpRect;

    public Vector2 pos;

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
        hpTexture.SetPixel(1, 1, Color.white);
        hpTexture.Apply();

        fullHpTexture = new Texture2D(1, 1);
        fullHpTexture.wrapMode = TextureWrapMode.Clamp;
        fullHpTexture.SetPixel(1, 1, Color.grey);
        fullHpTexture.Apply();

    }

    public void OnGUI()
    {
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
        var hpRate = ComputeHpRate();
        
        hpRect = new Rect(pos,
                        new Vector2(ComputeHpRate() * barWidth / 100, barHeight)
                        );
    }

    public int ComputeHpRate()
    {
        return currentHp * 100 / totalHp;
    }

}
