using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TextMeshSortingLayer))]
public class TextMeshSortingLayerEditor : Editor
{
    public int sortingLayer;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var txtSorting = (TextMeshSortingLayer)target;
        sortingLayer = txtSorting.GetComponent<MeshRenderer>().sortingOrder;

        var newSortingLayer = EditorGUILayout.IntField("SortingLayer", sortingLayer);

        if(newSortingLayer != sortingLayer)
            txtSorting.GetComponent<MeshRenderer>().sortingOrder = newSortingLayer;
    }
}
