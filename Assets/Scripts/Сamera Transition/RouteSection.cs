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

                routePartSetting.Position = EditorGUILayout.Vector3Field("Position", routePartSetting.Position);
                routePartSetting.Rotation= EditorGUILayout.Vector3Field("Rotation", routePartSetting.Position);
                routePartSetting.MoveDuration = EditorGUILayout.FloatField("MoveDuration", routePartSetting.MoveDuration);

                EditorGUILayout.EndVertical();
            }

            EditorGUILayout.EndVertical();
        }
    }

    private static void DrawAddRoutePathButton(Route route)
    {
        if (GUILayout.Button("Add", GUILayout.Width(45), GUILayout.Height(30)))
        {
            var PartSettings = new PartSettings[route.PartSettings.Length + 1];
            for (int i = 0; i < PartSettings.Length - 1; i++)
            {
                PartSettings[i] = route.PartSettings[i];
            }

            PartSettings[PartSettings.Length - 1] = new PartSettings(PartSettings[PartSettings.Length - 2].Position + new Vector3(3, 0, 0));
            route.PartSettings = PartSettings;
        }
    }
}
