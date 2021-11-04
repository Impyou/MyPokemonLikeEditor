using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WildPokemonInfo))]
public class EncounterInfoEditor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        /*
        serializedObject.Update();
        ListWithName.Show(serializedObject.FindProperty("pokemonEncounterInfos")); //EditorGUILayout.PropertyField(serializedObject.FindProperty("pokemonEncounterInfos"), true);
        serializedObject.ApplyModifiedProperties();
        */
    }
}
