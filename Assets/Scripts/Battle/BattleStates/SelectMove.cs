using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMove : IBattleState
{
    SelectArrow selectArrowMove;
    public void Init()
    {
        var moveBar = BattleUI.Get<MoveBar>("MoveBar");
        moveBar.gameObject.SetActive(true);

        selectArrowMove = BattleUI.Get<SelectArrow>("SelectArrowMove");
        selectArrowMove.Init();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectArrowMove.MoveCursor(SelectArrow.Direction.RIGHT);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectArrowMove.MoveCursor(SelectArrow.Direction.LEFT);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectArrowMove.MoveCursor(SelectArrow.Direction.UP);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectArrowMove.MoveCursor(SelectArrow.Direction.DOWN);
        }
    }
}
