using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using System;

[CustomEditor(typeof(CameraTransition))]
public class CameraTransitionEditor : Editor
{
    private const string _enumFile = "RouteName";

    private CameraTransition _cameraTransition;
    private string _pathToEnumFile;
    private string _routeName = "New Route Name";

    private void OnEnable()
    {
        _cameraTransition = (CameraTransition)target;

        _pathToEnumFile = AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(_enumFile)[0]);
    }

    public override void OnInspectorGUI()
    {
        DrawNewClipSelection();
    }

    private void DrawNewClipSelection()
    {
        _routeName = EditorGUILayout.TextField("Name", _routeName);
        DrawAddButton();
    }

    private void DrawAddButton()
    {
        if (GUILayout.Button("Add"))
        {
            AddRoute();
        }
    }

    private void AddRoute()
    {
        if (_routeName == string.Empty)
            return;

        if (!Regex.IsMatch(_routeName, @"^[a-zA-Z][a-zA-Z0-9_]*$"))
            return;

        Array array = Enum.GetValues(typeof(RouteName));
        if (array.Length != 0)
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (_routeName == array.GetValue(i).ToString())
                {
                    Debug.LogError("The path with the same name already exists!");
                    return;
                }
            }
        }

        EnumEditor.WriteToFile(_routeName, _pathToEnumFile);

        _routeName = string.Empty;
    }
}
