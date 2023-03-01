using System.Collections.Generic;
using UnityEngine;

using Gravitation;

public class PhysicsEngine : MonoBehaviour
{
    public List<GameObject> _PhysicsBodies = new();
    private Gravity _gravity = new();

    private void FixedUpdate()
    {
        PhysicsEngineUpdate(_PhysicsBodies);
    }

    public void PhysicsEngineUpdate(List<GameObject> gameObjects)
    {
        _gravity.SimulateGravity(gameObjects);
    }

    public List<GameObject> GetPhysicBodies()
    {
        return _PhysicsBodies;
    }

    public void AddPhysicsBody(GameObject body)
    {
        if(!_PhysicsBodies.Contains(body))
        {
            _PhysicsBodies.Add(body);
            //body.GetComponent<PhysicsParameters>().UpdateParameters();
        }
        else
            Debug.Log("You're trying to add a physics body to the PhysicsEngine that is already here!");
    }

    public void RemovePhysicsBody(GameObject body)
    {
        if(_PhysicsBodies.Contains(body))
            _PhysicsBodies.Remove(body);
        else
            Debug.Log("You are trying to remove a physics body from the PhysicsEngine that isn't here");
    }
}