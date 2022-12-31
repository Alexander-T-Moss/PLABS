using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class TimeBody : MonoBehaviour
{
    public bool IsRewinding = false;
    Rigidbody body;
    private Vector3 currentVelocity = new();

    List<PointInTime> pointsInTime;

    void Start()
    {
        pointsInTime = new();
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            StartRewind();
        }
        else if (Input.GetKeyUp(KeyCode.Backspace))
        {
            StopRewind();
        }
    }

    void FixedUpdate()
    {
        if (IsRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            currentVelocity = pointInTime.Velocity;
            transform.position = pointInTime.Position;
            transform.rotation = pointInTime.Rotation;
            pointsInTime.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(60f / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        pointsInTime.Insert(0, new PointInTime(body.velocity, transform.position, transform.rotation));

    }

    void StartRewind()
    {
        IsRewinding = true;
        body.isKinematic = true;
    }

    void StopRewind()
    {
        IsRewinding = false;
        body.isKinematic = false;
        body.velocity = currentVelocity;
    }
}