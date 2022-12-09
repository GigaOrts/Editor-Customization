using UnityEngine;

[System.Serializable]
public class PartSettings
{
    public Vector3 Position;
    public Vector3 Rotation;
    public float MoveDuration;

    public PartSettings(Vector3 position)
    {
        Position = position;
    }
}
