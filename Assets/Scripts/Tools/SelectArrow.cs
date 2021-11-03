using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectArrow : MonoBehaviour
{
    // Start is called before the first frame update
    public int numberActions;
    public Vector2Int cursor;

    public List<Transform> posCursor;
    public enum Direction { RIGHT, LEFT, UP, DOWN };
    public Action<int>[,] callbacks;
    public bool canMoveOnNull = true;

    public SelectArrow() : base()
    {
        cursor = new Vector2Int(0, 0);
    }

    public void Init(Action<int>[,] callbacks)
    {
        this.callbacks = callbacks;
        cursor = new Vector2Int(0, 0);
        transform.position = posCursor[GetActionIndex(cursor)].position;
    }

    public void MoveCursor(Direction direction)
    {
        var newCursor = Vector2Int.zero;
        switch (direction)
        {
            case Direction.RIGHT:
                newCursor = new Vector2Int(Mathf.Min(cursor.x + 1, 1), cursor.y);
                break;
            case Direction.LEFT:
                newCursor = new Vector2Int(Mathf.Max(cursor.x - 1, 0), cursor.y);
                break;
            case Direction.UP:
                newCursor = new Vector2Int(cursor.x, Mathf.Max(cursor.y - 1, 0));
                break;
            case Direction.DOWN:
                newCursor = new Vector2Int(cursor.x, Mathf.Min(cursor.y + 1, 1));
                break;
        }
        if (!IsOutOfRange(newCursor) && (canMoveOnNull || !IsOnNullCallback(newCursor)))
            cursor = newCursor;
        transform.position = posCursor[GetActionIndex(cursor)].position;
    }

    bool IsOnNullCallback(Vector2Int pos) => callbacks[pos.x, pos.y] == null;
    

    bool IsOutOfRange(Vector2Int pos)
    {
        var index = GetActionIndex(pos);
        return index < 0 || index > numberActions - 1;
    }

    public int GetActionIndex(Vector2Int targetCursor)
    {
        return targetCursor.x + targetCursor.y * 2;
    }

    public void Call()
    {
        // TODO fix crash no attack
        callbacks[cursor.x, cursor.y].Invoke(GetActionIndex(cursor));
    }


}
