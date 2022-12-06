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
        // SerializedProperty _coll = serializedObject.FindProperty("m_ColliderType");

        _sprite.objectReferenceValue = (Sprite) EditorGUILayout.ObjectField("Sprite", _sprite.objectReferenceValue, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        _int.intValue = EditorGUILayout.IntField("Hardness", _int.intValue);
        
        serializedObject.ApplyModifiedProperties();

    }
}