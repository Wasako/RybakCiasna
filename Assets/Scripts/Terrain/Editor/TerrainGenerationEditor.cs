using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TerrainGeneration))]
public class TerrainGenerationEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // all above is necessory for a custom inspector window

        // shows standard fields of base class
        base.OnInspectorGUI();

        // target is where functions are taken from, i think
        TerrainGeneration generator = target as TerrainGeneration;

        // draws a space :)
        EditorGUILayout.Space();

        // draws a button with a label, which executes the code in {} when clicked
        if (GUILayout.Button("Generate terrain w/ new seed"))
        {
            generator.ButtonNewTerrain();
        }

        if (GUILayout.Button("Generate terrain w/ same seed"))
        {
            generator.ButtonSameTerrain();
        }

        if (GUILayout.Button("Clear terrain"))
        {
            generator.ButtonClearTerrain();
        }

        // draws a label in which word wrap is on
        EditorStyles.label.wordWrap = true;
        EditorGUILayout.LabelField("Press I to generate with new seed, O with same seed, P to clear.");
        
    }
}
