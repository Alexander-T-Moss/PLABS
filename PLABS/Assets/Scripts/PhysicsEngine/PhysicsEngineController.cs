using System.Collections.Generic;
using UnityEngine;

// Load physics classes
using Gravitation;

public class PhysicsEngineController : MonoBehaviour
{
    public List<GameObject> _PhysicsBodies = new();
    private Gravity _gravity = new();   

    private void Awake()
    {
        Time.fixedDeltaTime = 0.01f;
    }


    private void FixedUpdate()
    {
        PhysicsEngineUpdate();
    }

    public void PhysicsEngineUpdate(string tagFilter = null)
    {
        if(tagFilter != null)
            _gravity.SimulateGravity(TagFilter(_PhysicsBodies, tagFilter));
        else
            _gravity.SimulateGravity(_PhysicsBodies);
    }

    public List<GameObject> TagFilter(List<GameObject> _gameObjects, string tagFilter)
    {
        List<GameObject> _tagFilteredObjects = new();
        for(int indexer = 0; indexer < _PhysicsBodies.Count; indexer++)
        {
            if (_PhysicsBodies[indexer].CompareTag(tagFilter))
                _tagFilteredObjects.Add(_PhysicsBodies[indexer]);
        }
        return _tagFilteredObjects;
    }

    public void AddPhysicsBody(GameObject body)
    {
        if(!_PhysicsBodies.Contains(body))
        {
            _PhysicsBodies.Add(body);
            body.GetComponent<PhysicsParameters>().UpdateParameters();
        }
    }

    public void RemovePhysicsBody(GameObject body)
    {
        if(_PhysicsBodies.Contains(body))
            _PhysicsBodies.Remove(body);
    }
}