using System.Collections.Generic;
using UnityEngine;

public struct PlayerProgress
{
    public Vector3 Position;
    public Quaternion Rotation;
    public IEnumerable<Vector3> DestinationPoints;
}