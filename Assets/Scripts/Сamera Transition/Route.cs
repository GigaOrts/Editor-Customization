using UnityEngine;

[System.Serializable]
public class Route
{
    [HideInInspector] public string ID;
    [HideInInspector] public RouteName Name;
    [HideInInspector] public RoutePartSettings[] RoutePartSettings;
}
