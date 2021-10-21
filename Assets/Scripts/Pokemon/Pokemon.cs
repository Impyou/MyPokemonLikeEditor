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

    public void inflictDamage(int damagePower, PokemonDef attacker)
    {
        var damage = ((2 * def.level / 5) + 2) * damagePower * attacker.currentStats.attq / (50 * def.currentStats.def) + 2;
        def.hpCurrent -= damage;
        def.hpCurrent = Mathf.Max(def.hpCurrent, 0);
    }

    public void UpdateHpBar(int newHp)
    {
        HpBar.currentHp = newHp;
        HpBar.UpdateUX();
    }

    public void ResetHpBar()
    {
        HpBar.totalHp = def.currentStats.hp;
        HpBar.currentHp = def.hpCurrent;
        HpBar.UpdateUX();
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
}
