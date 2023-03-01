using UnityEngine;

namespace TrajectoryLines
{
    public class TrajectoryLine : MonoBehaviour
    {
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.startWidth = 0.05f;
            _lineRenderer.endWidth = 0.05f;
            _lineRenderer.startColor = Color.white;
            _lineRenderer.endColor = Color.white;
        }

        public void SetVertices(int verticies)
        {
            _lineRenderer.positionCount = verticies;
        }

        public void SetVertex(int index, Vector3 position)
        {
            _lineRenderer.SetPosition(index, position);
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}