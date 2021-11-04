using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ListWithName
{
    public static void Show(SerializedProperty list)
    {
        EditorGUILayout.LabelField(list.name);
        if (list.isExpanded)
        {
            EditorGUI.indentLevel += 2;
            for (int i = 0; i < list.arraySize; i++)
            {
                //EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
            }
            EditorGUI.indentLevel -= 2;
        }
    }
}
