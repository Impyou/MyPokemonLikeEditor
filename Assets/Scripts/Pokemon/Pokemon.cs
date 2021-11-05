using DigitalRuby.Tween;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    public PokemonDef def;
    public UxBar HpBar;
    public UxBar ExpBar;
    private SpriteRenderer spriteRenderer;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public Move[] GetMoves()
    {
        return def.moves;
    }

    public void UpdateHpBar(float newHp)
    {
        HpBar.currentValue = newHp;
        HpBar.UpdateUX();
    }

    public void ResetUX()
    {
        HpBar.maxValue = def.currentStats.hp;
        HpBar.currentValue = def.hpCurrent;
        HpBar.UpdateUX();

        SetExpBarValues(def.expCurrentLevel, def.totalExpToLevelUp, def.currentExp);
    }

    public void UpdateExpBar(float newExpValue)
    {
        if (ExpBar is null)
            return;
        ExpBar.currentValue = newExpValue;
        ExpBar.UpdateUX();
    }

    public void SetExpBarValues(float start, float end, float current)
    {
        if (ExpBar is null)
            return;
        ExpBar.FullReset(start, end, current);
        ExpBar.UpdateUX();
    }

    public void SetAlpha(float alpha)
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
    }

    public int GetHp()
    {
        return def.hpCurrent;
    }

    public int getHpRate()
    {
        return def.hpCurrent * 100 / def.currentStats.hp;
    }

    public bool IsKO()
    {
        return GetHp() == 0;
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    public string GetName()
    {
        return def.name;
    }

    public void SetGUIActive()
    {
        HpBar.UpdateData();

        if (ExpBar != null)
        {
            ExpBar.shouldDraw = true;
            ExpBar.UpdateData();
        }
        HpBar.shouldDraw = true;
        
    }

    public void AnimatePokeSpawn()
    {
        var mat = GetComponent<SpriteRenderer>().material;
        gameObject.Tween("PokeSpawn", 0f, 1.57f, 0.5f, TweenScaleFunctions.Linear, (t) => { mat.SetFloat("_TimeStamp", t.CurrentValue); });
    }

    public void AnimatePokeBack()
    {
        var mat = GetComponent<SpriteRenderer>().material;
        gameObject.Tween("PokeBack", 1.57f, 0f, 0.5f, TweenScaleFunctions.Linear, (t) => { mat.SetFloat("_TimeStamp", t.CurrentValue); });
    }

    public void Heal(int rate)
    {
        def.Heal(rate);
    }

    public void InflictDamage(int damagePower, PokemonDef attacker)
    {
        def.InflictDamage(damagePower, attacker);
    }

    public void ApplyModifier(StatModifier modifier)
    {
        def.ApplyModifier(modifier);
    }
}
