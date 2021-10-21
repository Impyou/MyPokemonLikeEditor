using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PokemonDefBase
{
    public enum ExpFamily { SLOW, MIDDLE, FAST}
    public PokemonStats v;
    public float expMultiplier;
    public ExpFamily expFamily;
    
    public int GetNeededExpLevel(int level)
    {
        switch(expFamily)
        {
            case ExpFamily.SLOW:
                return ExpComputer.Slow(level);
            case ExpFamily.MIDDLE:
                return ExpComputer.Middle(level);
            case ExpFamily.FAST:
                return ExpComputer.Fast(level);
        }
        return 0;
    }

    public class ExpComputer
    {
        public static int Pow(int x, int p)
        {
            var result = x;
            for(int i = 1; i < p; i++)
            {
                result *= x;
            }
            return result;
        }
        public static int Slow(int level)
        {
            return 6 * Pow(level, 3) / 5 - 15 * Pow(level, 2) + 100 * level - 140;
        }

        public static int Middle(int level)
        {
            return Pow(level, 3);
        }
        public static int Fast(int level)
        {
            return 4 * Pow(level, 3) / 5;
        }
    }
        

    public PokemonDefBase()
    {}
}
