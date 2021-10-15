using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectArrowAction : SelectArrow
{
    public enum Action { FIGHT, BAG, POKEMON_SELECTION, RUN };
    public Action[,] actions = new Action[,] {{Action.FIGHT, Action.POKEMON_SELECTION}, { Action.BAG, Action.RUN}};

    public Action GetAction()
    {
        return actions[cursor.x, cursor.y];
    }


}
