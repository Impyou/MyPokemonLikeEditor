using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    public PokemonDef def;
    public HpUX HpBar;

    public int[] GetMoveIds()
    {
        return def.moveIDs;
    }

    public void inflictDamage(int damagePower)
    {
        def.hpCurrent -= damagePower;
        def.hpCurrent = Mathf.Max(def.hpCurrent, 0);
    }

    public void UpdateHpBar(int newHp)
    {
        HpBar.currentHp = newHp;
        HpBar.UpdateUX();
    }

    public void ResetHpBar()
    {
        HpBar.totalHp = def.hpMax;
        HpBar.currentHp = def.hpCurrent;
        HpBar.UpdateUX();
    }

    public int GetHp()
    {
        return def.hpCurrent;
    }

    public int getHpRate()
    {
        return def.hpCurrent * 100 / def.hpMax;
    }

    public bool IsKO()
    {
        return GetHp() == 0;
    }
}
