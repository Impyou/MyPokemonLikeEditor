using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    public PokemonDef def;
    public HpUX HpBar;
    public HpUX ExpBar;
    private SpriteRenderer spriteRenderer;

    public void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public int[] GetMoveIds()
    {
        return def.moveIDs;
    }

    public void inflictDamage(int damagePower, PokemonDef attacker)
    {
        var damage = ((2 * def.level / 5) + 2) * damagePower * attacker.currentStats.attq / (50 * def.currentStats.def) + 2;
        def.hpCurrent -= damage;
        def.hpCurrent = Mathf.Max(def.hpCurrent, 0);
    }

    public void UpdateHpBar(float newHp)
    {
        HpBar.currentHp = newHp;
        HpBar.UpdateUX();
    }

    public void ResetUX()
    {
        HpBar.totalHp = def.currentStats.hp;
        HpBar.currentHp = def.hpCurrent;
        HpBar.UpdateUX();

        SetExpBarValues(def.expCurrentLevel, def.totalExpToLevelUp, def.currentExp);
    }

    public void UpdateExpBar(float newExpValue)
    {
        if (ExpBar is null)
            return;
        ExpBar.currentHp = newExpValue;
        ExpBar.UpdateUX();
    }

    public void SetExpBarValues(float start, float end, float current)
    {
        if (ExpBar is null)
            return;
        ExpBar.startValue = start;
        ExpBar.totalHp = end;
        UpdateExpBar(current);
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
        Debug.Log(position);
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
        if(ExpBar != null)
            ExpBar.shouldDraw = true;
        HpBar.shouldDraw = true;
    }
}
