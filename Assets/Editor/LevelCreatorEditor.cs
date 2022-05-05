using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(LevelCreator))]
[CanEditMultipleObjects]
public class LevelCreatorEditor : Editor
{
    private SerializedProperty blockGO;
    private SerializedProperty rootTransform;
    private SerializedProperty gridHeight;
    private SerializedProperty gridWidth;
    private SerializedProperty gridOffset;
    private SerializedProperty noiseHeight;
    private SerializedProperty objectsGO;
    private SerializedProperty mapBorder;
    

    LevelCreator levelCreator;



    private void OnEnable()
    {
        levelCreator = (LevelCreator) target;
        blockGO = serializedObject.FindProperty("blockGO");
        rootTransform = serializedObject.FindProperty("root");
        gridHeight = serializedObject.FindProperty("GridHeight");
        gridWidth = serializedObject.FindProperty("GridWidth");
        gridOffset = serializedObject.FindProperty("GridOffset");
        noiseHeight = serializedObject.FindProperty("NoiseHeight");
        objectsGO = serializedObject.FindProperty("objectsGO");
        mapBorder = serializedObject.FindProperty("MapBorder");
       
    }


    public override void OnInspectorGUI()
    {
        serializedObject.UpdateIfRequiredOrScript();

        EditorGUILayout.PropertyField(blockGO);
        EditorGUILayout.PropertyField(rootTransform);
        EditorGUILayout.PropertyField(gridWidth);
        EditorGUILayout.PropertyField(gridHeight);
        EditorGUILayout.PropertyField(gridOffset);
        EditorGUILayout.PropertyField(noiseHeight);
        EditorGUILayout.PropertyField(objectsGO);
        EditorGUILayout.PropertyField(mapBorder);
      

        EditorGUILayout.Space();

        var style = new GUIStyle(GUI.skin.button);
        style.normal.textColor= Color.green;

        var style2 = new GUIStyle(GUI.skin.button);
        style2.normal.textColor = Color.red;

        if (GUILayout.Button("Generate Level", style))
        {
            
            levelCreator.GenerateLevel();
            MarkSceneDirty();
        }

        if (GUILayout.Button("Destroy Level",style2))
        {
            levelCreator.DestroyCurrentLevel();
            MarkSceneDirty();
        }
        serializedObject.ApplyModifiedProperties();
        
    }

    private void MarkSceneDirty()
    {
        EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
    }
}
