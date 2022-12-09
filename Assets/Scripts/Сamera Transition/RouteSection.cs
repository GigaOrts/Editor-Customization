using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class RouteSection
{
    public static void Draw(List<Route> routes)
    {
        for (int routeNumber = 0; routeNumber < routes.Count; routeNumber++)
        {
            string routeName = routes[routeNumber].Name.ToString();

            EditorGUILayout.BeginVertical(GUI.skin.box);
            EditorGUILayout.LabelField(routeName);

            for (int i = 0; i < routes[routeNumber].PartSettings.Length; i++)
            {
                PartSettings routePartSetting = routes[routeNumber].PartSettings[i];

                EditorGUILayout.BeginVertical(GUI.skin.window);
                EditorGUILayout.LabelField($"Point {i + 1}");
                DrawDeletePathButton(routes[routeNumber], i);

                routePartSetting.Position = EditorGUILayout.Vector3Field("Position", routePartSetting.Position);
                routePartSetting.Rotation= EditorGUILayout.Vector3Field("Rotation", routePartSetting.Rotation);
                routePartSetting.MoveDuration = EditorGUILayout.FloatField("MoveDuration", routePartSetting.MoveDuration);

                EditorGUILayout.EndVertical();
            }

            DrawAddRoutePathButton(routes[routeNumber]);
            EditorGUILayout.EndVertical();
        }
    }

    private static void DrawAddRoutePathButton(Route route)
    {
        if (GUILayout.Button("Add", GUILayout.Width(45), GUILayout.Height(30)))
        {
            PartSettings[] partSettings = new PartSettings[route.PartSettings.Length + 1];
            for (int i = 0; i < partSettings.Length - 1; i++)
            {
                partSettings[i] = route.PartSettings[i];
            }

            partSettings[partSettings.Length - 1] = new PartSettings(partSettings[partSettings.Length - 2].Position + new Vector3(3f, 0f, 0f));
            route.PartSettings = partSettings;
        }
    }
    
    private static void DrawDeletePathButton(Route route, int index)
    {
        if (GUILayout.Button("-", GUILayout.Width(17), GUILayout.Height(17)))
        {
            PartSettings[] parts = new PartSettings[route.PartSettings.Length - 1];

            for (int i = 0; i < index; i++)
            {
                parts[i] = route.PartSettings[i];
            }

            for (int i = index + 1; i < parts.Length + 1; i++)
            {
                parts[i - 1] = route.PartSettings[i];
            }

            route.PartSettings = parts;
        }
    }
}
