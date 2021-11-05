using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TypeChart))]
public class TypeChartEditor: Editor
{
    public override void OnInspectorGUI()
    {
        var typeChart = (TypeChart)target;
        int typeNumber = Enum.GetNames(typeof(PokemonType)).Length;

        var verticalStyle = new GUIStyle();
        var horizontalStyle = new GUIStyle();
        int typeSpace = 50;
        int startIndent = 50;
        var typeNameWidth = (EditorGUIUtility.currentViewWidth - typeSpace - startIndent) / typeNumber;

        var superEffectiveGUIStyle = new GUIStyle(GUI.skin.button);
        var notVeryEffectiveGUIStyle = new GUIStyle(GUI.skin.button);
        var notEffectiveGUIStyle = new GUIStyle(GUI.skin.button);
        var effectiveGUIStyle = new GUIStyle(GUI.skin.button);

        EditorGUILayout.BeginHorizontal(horizontalStyle);
        GUILayout.Space(typeSpace);
        for (int i = 0; i < typeNumber; i++)
        {
            EditorGUILayout.LabelField(((PokemonType)i).ToString(), GUILayout.Width(typeNameWidth));
        }
        EditorGUILayout.EndHorizontal();

        for (int i =0; i < typeNumber;i++)
        {
            EditorGUILayout.BeginHorizontal(verticalStyle);
            EditorGUILayout.LabelField(((PokemonType)i).ToString(), GUILayout.Width(typeSpace));
            for (int j = 0; j < typeNumber;j++)
            {
                var effectiveLevel = typeChart.GetMultiplier((PokemonType)i, (PokemonType)j);
                GUIStyle effectiveButtonStyle = effectiveGUIStyle;
                string text = "1/1";
                GUI.backgroundColor = Color.grey;
                switch ((EffectiveLevel)effectiveLevel)
                {
                    case EffectiveLevel.NotEffective:
                        effectiveButtonStyle = notEffectiveGUIStyle;
                        GUI.backgroundColor = Color.black;
                        text = "0";
                        break;
                    case EffectiveLevel.NotVeryEffective:
                        effectiveButtonStyle = notVeryEffectiveGUIStyle;
                        GUI.backgroundColor = Color.red;
                        text = "1/2";
                        break;
                    case EffectiveLevel.SuperEffective:
                        effectiveButtonStyle = superEffectiveGUIStyle;
                        GUI.backgroundColor = Color.green;
                        text = "2";
                        break;
                }
                if (GUILayout.Button(text, effectiveButtonStyle, GUILayout.Width(typeNameWidth)))
                {
                    typeChart.RollValue((PokemonType)i, (PokemonType)j);
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        if (GUILayout.Button("Reset"))
        {
            typeChart.Reset();
        }
    }
}
