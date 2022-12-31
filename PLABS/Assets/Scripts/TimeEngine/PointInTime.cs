using UnityEngine;

public class PointInTime
{
    public Vector3 Velocity;
    public Vector3 Position;
    public Quaternion Rotation;
    
    public PointInTime(Vector3 velocity, Vector3 position, Quaternion rotation)
    {
        Velocity = velocity;
        Position = position;
        Rotation = rotation;
    }
}