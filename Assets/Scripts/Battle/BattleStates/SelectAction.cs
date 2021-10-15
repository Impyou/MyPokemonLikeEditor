using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAction : IBattleState
{
    public SelectArrowAction selectArrow;

    public SelectAction()
    {
        
    }

    public void Init()
    {
        selectArrow = BattleUI.Get<SelectArrowAction>("SelectArrow");
        selectArrow.Init();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectArrow.MoveCursor(SelectArrow.Direction.RIGHT);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectArrow.MoveCursor(SelectArrow.Direction.LEFT);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectArrow.MoveCursor(SelectArrow.Direction.UP);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectArrow.MoveCursor(SelectArrow.Direction.DOWN);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            var action = selectArrow.GetAction();
            if (action == SelectArrowAction.Action.FIGHT)
            {
                BattleStateMachine.__instance__.SetState(new SelectMove());
            }
        }
    }
}
