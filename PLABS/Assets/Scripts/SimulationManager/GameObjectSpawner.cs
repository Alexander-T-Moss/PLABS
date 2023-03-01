using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSpawner : MonoBehaviour
{
    private PhysicsEngine _physicsEngine;
    public GameObject PhysicsBodyPrefab;

    private void Start()
    {
        _physicsEngine = GameObject.Find("PhysicsEngine").GetComponent<PhysicsEngine>();
    }

    public GameObject MakeNewPhysicsBody(string name = "TestBody")
    {
        GameObject _newPhysicsBody = Instantiate(PhysicsBodyPrefab);
        _newPhysicsBody.transform.position = RandomPositionVector();
        _newPhysicsBody.GetComponent<Rigidbody>().velocity = RandomVelocityVector();
        _newPhysicsBody.name = name;
        _physicsEngine.AddPhysicsBody(_newPhysicsBody);
        _newPhysicsBody.GetComponent<PhysicsParameters>().InitializePhysicsBody();
        return _newPhysicsBody;
    }

    private Vector3 RandomPositionVector()
    {
        return Random.insideUnitSphere * Random.Range(-20, 20); 
    }

    private Vector3 RandomVelocityVector()
    {
        return Random.insideUnitSphere * Random.Range(-4, 4); 
    }
}