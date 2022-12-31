using System.Collections.Generic;
using UnityEngine;

namespace Gravitation
{
    public class Gravity
    {
        private float SphereOfInfluence = 10000000f;
        private const float gravitationalConstant = 1f;

        public void SimulateGravity(List<GameObject> _gameObjects)
        {
            for(int indexerY = 0; indexerY < _gameObjects.Count; indexerY++)
            {
                for(int indexerX = 0; indexerX < _gameObjects.Count; indexerX++)
                {
                    if (_gameObjects[indexerY] != _gameObjects[indexerX] || _gameObjects[indexerY].CompareTag(_gameObjects[indexerX].tag))
                        AddGravityForce(_gameObjects[indexerY], _gameObjects[indexerX]);
                }
            }
        }

        private void AddGravityForce(GameObject _gameObject, GameObject _attractedGameObject)
        {
            Rigidbody _body = _gameObject.GetComponent<Rigidbody>();
            Rigidbody _attractedBody = _attractedGameObject.GetComponent<Rigidbody>();

            Vector3 direction = _body.position - _attractedBody.position;
            float seperationDistance = direction.magnitude;

            float forceMagnitude = 0;

            if(seperationDistance != 0 && seperationDistance <= SphereOfInfluence)
                // NEWTONS GRAVITY EQUATION
                forceMagnitude = _body.mass * _attractedBody.mass / Mathf.Pow(seperationDistance, 2) * gravitationalConstant;

            Vector3 force = direction.normalized * forceMagnitude;

            //Debug.Log("Gravity: Adding force " + force.ToString() + " to " + _attractedGameObject.name + " from " + _gameObject.name);
            _attractedBody.AddForce(force, ForceMode.Force);
        }
    }
}