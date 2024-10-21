using System;
using UnityEngine;

public struct PlayerProgress
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3[] DestinationPoints;

    public static PlayerProgress Empty => new() {DestinationPoints = Array.Empty<Vector3>()};
}