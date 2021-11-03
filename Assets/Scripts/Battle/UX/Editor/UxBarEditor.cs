using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UxBar)), CanEditMultipleObjects]
public class UxBarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var uxBar = (UxBar)target;
        uxBar.UpdateData();
    }
}
