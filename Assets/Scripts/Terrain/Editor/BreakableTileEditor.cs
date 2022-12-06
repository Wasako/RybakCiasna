using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

[CustomEditor(typeof(BreakableTile))]
public class BreakableTileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty _sprite = serializedObject.FindProperty("m_Sprite");
        SerializedProperty _int = serializedObject.FindProperty("health");
        SerializedProperty _int1 = serializedObject.FindProperty("generationMethod");
        SerializedProperty _float = serializedObject.FindProperty("generationValue");

        _sprite.objectReferenceValue = (Sprite) EditorGUILayout.ObjectField("Sprite", _sprite.objectReferenceValue, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        _int.intValue = EditorGUILayout.IntField("Hardness", _int.intValue);
        _int1.intValue = EditorGUILayout.IntField("Generation method", _int1.intValue);
        _float.floatValue = EditorGUILayout.FloatField("Generation chance", _float.floatValue);
        
        EditorStyles.label.wordWrap = true;
        EditorGUILayout.LabelField("Generation method: 1 - from noise with border, 2 - by chance, 3 - plant");
        
        serializedObject.ApplyModifiedProperties();
    }
}