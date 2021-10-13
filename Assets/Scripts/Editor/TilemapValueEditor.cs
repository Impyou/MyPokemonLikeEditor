using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TilemapValue))]
public class TilemapValueEditor : Editor
{
    public float sizeBlock = 0.5f;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Generate Labels"))
        {
            GenerateLabels();
        }
    }

    public void GenerateLabels()
    {
        var tileMapValue = (TilemapValue)target;
        if(tileMapValue.tileTypes == null)
        {
            tileMapValue.tileTypes = new TilemapValue.TileType[tileMapValue.width, tileMapValue.height];
        }
    }

    private void OnSceneGUI()
    {
        ShowLabel();
    }

    void ShowLabel()
    {
        var tileMapValue = (TilemapValue)target;

        if(tileMapValue.tileTypes == null)
            return;

        for (int i = 0; i < tileMapValue.tileTypes.GetLength(0); i++)
        {
            for (int j = 0; j < tileMapValue.tileTypes.GetLength(1); j++)
            {
                Debug.Log(tileMapValue.tileTypes[i, j]);
                Handles.Label(new Vector3(i * sizeBlock, j * sizeBlock, 0f), tileMapValue.tileTypes[i, j].ToString());
            }
        }
    }
}
