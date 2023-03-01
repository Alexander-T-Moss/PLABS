using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TrajectoryLines;

public class PredictionEngine : MonoBehaviour
{
    // Prediction scene components
    private Scene _predictionScene;
    private PhysicsScene _predictionPhysicsScene;

    // Lists of GameObjects PredictionEngine manages
    private List<GameObject> RealGameObjects;
    private List<GameObject> GhostGameObjects;

    // Tracjectory Lines
    private List<TrajectoryLine> TrajectoryLines;
    public GameObject TrajectoryLinePrefab;

    // Link to Game Engines
    private PhysicsEngine _physicsEngine;

    // Runs when GameObject this script is attached to is made active
    private void Awake()
    {
        // Create predictionScene and predicitonPhysicsScene
        CreateSceneParameters sceneParameters = new(LocalPhysicsMode.Physics3D);
        _predictionScene = SceneManager.CreateScene("predictionScene", sceneParameters);
        _predictionPhysicsScene = _predictionScene.GetPhysicsScene();

        // Initialize TrajectoryLines and GhostGameObjects list
        TrajectoryLines = new();
        GhostGameObjects = new();
    }

    // Runs before first frame update is called
    private void Start()
    {
        _physicsEngine = GameObject.Find("PhysicsEngine").GetComponent<PhysicsEngine>();
    }

    // Loads all gameObjects with tag PhysicsBody into predictionScene
    private void LoadGhostObjects()
    {
        // Loads GameObjects in PhysicsEngines PhysicsBody list
        RealGameObjects = _physicsEngine.GetPhysicBodies();

        // Remove old trajectory lines
        ClearTrajectoryPredictions();

        // Add clones and new trajectory lines
        for(int indexer = 0; indexer < RealGameObjects.Count; indexer++)
        {
            GhostGameObjects.Add(CreateClone(RealGameObjects[indexer]));

            // Only create trajectory lines for game objects that are not kinematic
            if(!RealGameObjects[indexer].GetComponent<Rigidbody>().isKinematic)
                TrajectoryLines.Add(Instantiate(TrajectoryLinePrefab).GetComponent<TrajectoryLine>());
            else
                TrajectoryLines.Add(null);
        }
    }

    // Removes all GameObjects in predictionScene
    private void UnloadGhostObjects()
    {
        for(int indexer = 0; indexer < GhostGameObjects.Count; indexer++)
        {
            Destroy(GhostGameObjects[indexer]);
        }
        GhostGameObjects = new();
        RealGameObjects = new();
    }

    // Creates clone of a GameObject and spawns it in a scene
    private GameObject CreateClone(GameObject _gameObjectToBeCloned)
    {
        // Create _clonedGameObject GameObject
        GameObject _clonedGameObject = Instantiate(_gameObjectToBeCloned);
        SceneManager.MoveGameObjectToScene(_clonedGameObject, _predictionScene);

        // Modify _clonedGameObject's GameObject parameters
        _clonedGameObject.tag = "Clone";
        _clonedGameObject.GetComponent<MeshRenderer>().enabled = false;
        _clonedGameObject.GetComponent<Rigidbody>().velocity = _gameObjectToBeCloned.GetComponent<Rigidbody>().velocity;
        //_clonedGameObject.GetComponent<PhysicsParameters>().UpdateParameters();

        return _clonedGameObject;
    }

    // Creates trajectory line GameObject
    private GameObject CreateTrajectoryLine(string _objectName)
    {
        // Create _trajectoryLine GameObject
        GameObject _trajectoryLine = new(_objectName);
        _trajectoryLine.AddComponent<LineRenderer>();

            
        // Modify _trajectoryLine's GameObject parameters
        LineRenderer _trajectoryLineRenderer = _trajectoryLine.GetComponent<LineRenderer>();
        _trajectoryLineRenderer.startWidth = 0.05f;
        _trajectoryLineRenderer.endWidth = 0.05f;

        return _trajectoryLine;
    }

    private void ClearTrajectoryPredictions()
    {
        for(int indexer = 0; indexer < TrajectoryLines.Count; indexer++)
        {
            Destroy(TrajectoryLines[indexer].GetGameObject());
        }
        TrajectoryLines = new();
    }

    public void PredictTrajectories(int _steps = 3000)
    {
        LoadGhostObjects();

        // Set verticy count for TrajectoryLines
        for(int indexer = 0; indexer < TrajectoryLines.Count; indexer++)
        {
            if (TrajectoryLines[indexer] != null)
                TrajectoryLines[indexer].SetVertices(_steps);
        }

        for(int _step = 0; _step < _steps; _step++)
        {
            _physicsEngine.PhysicsEngineUpdate(GhostGameObjects);
            _predictionPhysicsScene.Simulate(Time.fixedDeltaTime);
        
            for(int indexer = 0; indexer < GhostGameObjects.Count; indexer++)
            {
                if (!GhostGameObjects[indexer].GetComponent<Rigidbody>().isKinematic)
                    TrajectoryLines[indexer].SetVertex(_step, GhostGameObjects[indexer].GetComponent<Rigidbody>().position);
            }
        }

        UnloadGhostObjects();
    }
}