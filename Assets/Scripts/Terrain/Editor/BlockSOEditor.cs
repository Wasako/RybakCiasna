using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlockScriptableObject))]
public class BlockSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorStyles.label.wordWrap = true;
        EditorGUILayout.LabelField("Generation method: 1 - from noise with border, 2 - by chance, 3 - plant. Generation value: if 1 - border, if 2 or 3 - chance");
    }
}
