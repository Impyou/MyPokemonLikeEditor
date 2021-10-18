using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TilemapValue))]
public class TilemapValueEditor : Editor
{
    public float sizeBlock = 0.5f;
    int tmp_width, tmp_height;
    TilemapValue.TileType newType = TilemapValue.TileType.WALL;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        tmp_width = EditorGUILayout.IntField("Width", tmp_width);
        tmp_height = EditorGUILayout.IntField("Height", tmp_height);

        if(GUILayout.Button("Generate Labels"))
        {
            GenerateLabels();
        }
    }

    public void GenerateLabels()
    {
        var tileMapValue = (TilemapValue)target;

        tileMapValue.width = tmp_width;
        tileMapValue.height = tmp_height;

        tileMapValue.tileTypes = new TilemapValue.TileType[tileMapValue.width * tileMapValue.height];
    }

    private void OnSceneGUI()
    {
        ShowLabel();
        
        /* TODO : Fix issue y position */
        Vector3 mousePosition = Event.current.mousePosition;
        mousePosition.y = Camera.current.pixelHeight - mousePosition.y; //Hard fix
        mousePosition = Camera.current.ScreenToWorldPoint(mousePosition);

        int x, y;
        x = (int)(mousePosition.x / sizeBlock);
        y = (int)((mousePosition.y + sizeBlock) / sizeBlock);

        var e = Event.current;
        if(e.type == EventType.MouseDown && e.button == 0)
        {
            Debug.Log("Click");
            var tileMapValue = (TilemapValue)target;
            tileMapValue.SetTile(x, y, newType);
        }

        
    }

    void ShowLabel()
    {
        var tileMapValue = (TilemapValue)target;

        if(tileMapValue.tileTypes == null)
            return;

        for (int i = 0; i < tileMapValue.width; i++)
        {
            for (int j = 0; j < tileMapValue.height; j++)
            {
                Handles.Label(new Vector3(i * sizeBlock, j * sizeBlock, 0f), tileMapValue.GetTile(i, j).ToString());
            }
        }
    }

}
