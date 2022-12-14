using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;
using System;
using System.Linq;

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
        base.DrawDefaultInspector();

        _cameraTransition.Routes = RefreshRoutes(_cameraTransition.Routes);

        RouteSection.Draw(_cameraTransition.Routes);

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
        Refresh();

        _routeName = string.Empty;
    }

    private void RemoveRoute(Route route)
    {
        if (!EnumEditor.TryRemoveFromFile(route.Name.ToString(), _pathToEnumFile))
            return;

        Refresh();
    }

    private void Refresh()
    {
        Debug.Log("WAIT");
        string relativePath = _pathToEnumFile.Substring(_pathToEnumFile.IndexOf("Assets"));
        AssetDatabase.ImportAsset(relativePath);
    }

    private List<Route> RefreshRoutes(List<Route> oldRoutes)
    {
        int countRoute = Enum.GetNames(typeof(RouteName)).Length;
        List<Route> routes = new List<Route>(countRoute);

        for (int i = 0; i < countRoute; i++)
        {
            RouteName routeName = (RouteName)i;
            Route route = TryRestoreRoute(oldRoutes, routeName.ToString());

            if(route == null)
            {
                route = CreateNewRoute(routeName);
            }

            routes.Add(route);
        }

        return routes;
    }

    private Route TryRestoreRoute(List<Route> oldRoutes, string name)
    {
        return oldRoutes.FirstOrDefault(o => o.Name.ToString() == name);
    }

    private Route CreateNewRoute(RouteName routeName)
    {
        Route route = new Route
        {
            Name = routeName,
            PartSettings = new PartSettings[1]
            {
                new PartSettings(Vector3.zero)
            }
        };

        return route;
    }
}
