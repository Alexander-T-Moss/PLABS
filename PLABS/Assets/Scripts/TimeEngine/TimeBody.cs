using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class TimeBody : MonoBehaviour
{
    private TimeController _timeController;
    public bool IsRewinding = false;
    private Rigidbody _rb;
    private Vector3 currentVelocity = new();

    List<PointInTime> pointsInTime;

    void Start()
    {
        _timeController = GameObject.Find("TimeEngine").GetComponent<TimeController>();
        pointsInTime = new();
        _rb = GetComponent<Rigidbody>();
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
            Debug.Log("No more points to rewind back to!");
        }
    }

    void Record()
    {
        if (pointsInTime.Count > Mathf.Round(60f / Time.fixedDeltaTime))
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }
        pointsInTime.Insert(0, new PointInTime(_rb.velocity, transform.position, transform.rotation));

    }

    void StartRewind()
    {
        IsRewinding = true;
        _rb.isKinematic = true;
    }

    void StopRewind()
    {
        IsRewinding = false;
        if(!gameObject.GetComponent<PhysicsParameters>().Kinematic)
            _rb.isKinematic = false;
        _rb.velocity = currentVelocity;
    }
}