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
        BreakableTile tile = target as BreakableTile;

        tile.sprite = (Sprite) EditorGUILayout.ObjectField("Sprite", tile.sprite, typeof(Sprite), false, GUILayout.Height(EditorGUIUtility.singleLineHeight));
        tile.baseHealth = EditorGUILayout.IntField("Hardness", tile.baseHealth);
        tile.colliderType = (Tile.ColliderType) EditorGUILayout.EnumPopup("Collider Type", Tile.ColliderType.Grid);

    }
}

