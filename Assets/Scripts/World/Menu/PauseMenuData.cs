using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuData : MonoBehaviour
{
    public GameObject topMenu, botMenu;
    public AnimationData topMenuAnimationData, botMenuAnimationData;

    public GameObject[] menuItems;
    public int menuWidth;

    public int cursor = 0;

    public void Start()
    {
        StopMenuShader();
    }

    public void Move(Direction direction)
    {
        int newCursor = 0;
        switch(direction)
        {
            case Direction.DOWN:
                newCursor = cursor + menuWidth;
                break;
            case Direction.UP:
                newCursor = cursor - menuWidth;
                break;
            case Direction.LEFT:
                newCursor = cursor - 1;
                break;
            case Direction.RIGHT:
                newCursor = cursor + 1;
                break;
        }
        if(!isOut(newCursor))
        {
            StopMenuShader();
            cursor = newCursor;
            StartMenuShader();
        }
    }

    public void Close()
    {
        StopMenuShader();
    }

    public bool isOut(int cursor)
    {
        return cursor < 0 || cursor > menuItems.Length - 1; 
    }

    public void StopMenuShader()
    {
        var target = menuItems[cursor];
        var menuMaterial = target.GetComponent<SpriteRenderer>().material;
        menuMaterial.SetInt("_Active", 0);
    }

    public void StartMenuShader()
    {
        var target = menuItems[cursor];
        var menuMaterial = target.GetComponent<SpriteRenderer>().material;
        menuMaterial.SetInt("_Active", 1);
    }

    public void InitMenu()
    {
        cursor = 0;
        StartMenuShader();
    }

    public int GetIndex()
    {
        return cursor;
    }
}
